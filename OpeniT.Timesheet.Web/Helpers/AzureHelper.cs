namespace OpeniT.Timesheet.Web.Helpers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net.Http;
	using System.Security.Authentication;
	using System.Text;
	using System.Threading.Tasks;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.Logging;
	using Microsoft.IdentityModel.Clients.ActiveDirectory;

	using Models;

	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;
	using Newtonsoft.Json.Serialization;

	public class AzureHelper
	{
		private readonly IConfigurationRoot config;

		private readonly ILogger<AzureHelper> logger;

		private readonly DataContext dataContext;

		public AzureHelper(IConfigurationRoot config, ILogger<AzureHelper> logger, DataContext dataContext)
		{
			this.config = config;
			this.logger = logger;
			this.dataContext = dataContext;
		}

		public async Task<AzureProfile> GetAsync(string owner, string query)
		{
			AzureProfile profile = null;
			var apiVersion = this.config["Microsoft:GraphApiVersion"];
			var tenantName = this.config["Microsoft:TenantName"];
			var authString = this.config["Microsoft:Authority"];
			var clientId = this.config["Microsoft:ClientId"];
			var clientSecret = this.config["Microsoft:ClientSecret"];
			var graphUri = this.config["Microsoft:GraphUri"];

			try
			{
				var clientCredential = new ClientCredential(clientId, clientSecret);
				var authenticationContext = new AuthenticationContext(authString, false);
				var authenticationResult = await authenticationContext.AcquireTokenAsync(graphUri, clientCredential);
				var token = authenticationResult.AccessToken;

				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(graphUri);
					client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

					var uri = $"{apiVersion}/{tenantName}/users/{owner}{query}";

					var result = await client.GetAsync(uri);
					if (!result.IsSuccessStatusCode) throw new Exception($"{result.Content.ReadAsStringAsync().Result}");

					var content = await result.Content.ReadAsStringAsync();
					profile = JsonConvert.DeserializeObject<AzureProfile>(content);

					if (string.IsNullOrEmpty(query))
					{
						var thumbnailUri = $"{apiVersion}/{tenantName}/users/{owner}/photo/$value";
						result = await client.GetAsync(thumbnailUri);
						if (result != null && result.IsSuccessStatusCode)
						{
							var thumbnail = new UserThumbnail();
							var contentThumbnail = await result.Content.ReadAsByteArrayAsync();
							thumbnail.ContentType = result.Content.Headers.ContentType.MediaType;
							thumbnail.Content = contentThumbnail;

							if (profile != null) profile.Thumbnail = thumbnail;
						}
					}
				}
			}
			catch (AuthenticationException ex)
			{
				this.logger.LogCritical($"Acquiring a token failed with the following error: {ex.Message}");
				if (ex.InnerException != null) this.logger.LogError($"Error detail: {ex.InnerException.Message}");
			}
			catch (Exception ex)
			{
				this.logger.LogError($"Error getting user information: {ex}");
			}

			return profile;
		}

		public async Task<AzureProfile> GetGroupsAsync(string email, string query)
		{
			AzureProfile profile = null;
			var apiVersion = this.config["Microsoft:GraphApiVersion"];
			var tenantName = this.config["Microsoft:TenantName"];
			var authString = this.config["Microsoft:Authority"];
			var clientId = this.config["Microsoft:ClientId"];
			var clientSecret = this.config["Microsoft:ClientSecret"];
			var graphUri = this.config["Microsoft:GraphUri"];

			try
			{
				var clientCredential = new ClientCredential(clientId, clientSecret);
				var authenticationContext = new AuthenticationContext(authString, false);
				var authenticationResult = await authenticationContext.AcquireTokenAsync(graphUri, clientCredential);
				var token = authenticationResult.AccessToken;

				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(graphUri);
					client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

					var uri = $"{apiVersion}/{tenantName}/groups?filter=Equals(email,'{email}'){query}";

					var result = await client.GetAsync(uri);
					if (!result.IsSuccessStatusCode) throw new Exception($"{result.Content.ReadAsStringAsync().Result}");

					var content = await result.Content.ReadAsStringAsync();
					profile = JsonConvert.DeserializeObject<AzureProfile>(content);


					var jObject = JObject.Parse(content);
					var jArray = jObject.Value<JArray>("value");
					var groups_ = jArray.ToObject<List<AzureProfile>>();
					if (groups_.Any())
					{
						return groups_[0];
					}
				}
			}
			catch (AuthenticationException ex)
			{
				this.logger.LogCritical($"Acquiring a token failed with the following error: {ex.Message}");
				if (ex.InnerException != null) this.logger.LogError($"Error detail: {ex.InnerException.Message}");
			}
			catch (Exception ex)
			{
				this.logger.LogError($"Error getting user information: {ex}");
			}

			return profile;
		}

		public async Task<bool> IsKnownInactiveEmail(string email)
		{
			if (string.IsNullOrEmpty(email)) return false;

			try
			{
				var azureProfile = await GetAsync(email, "?$select=accountEnabled");
				if (azureProfile == null)
				{
					//not known
					return false;
				}

				return !azureProfile.AccountEnabled;
			}
			catch (Exception ex)
			{
				this.logger.LogError($"Error getting user information: {ex}");
				//not known
				return false;
			}
		}

		public async Task<List<AzureProfile>> GetUsersAsync()
		{
			var profiles = new List<AzureProfile>();
			var apiVersion = this.config["Microsoft:GraphApiVersion"];
			var tenantName = this.config["Microsoft:TenantName"];
			var authString = this.config["Microsoft:Authority"];
			var clientId = this.config["Microsoft:ClientId"];
			var clientSecret = this.config["Microsoft:ClientSecret"];
			var graphUri = this.config["Microsoft:GraphUri"];

			try
			{
				var clientCredential = new ClientCredential(clientId, clientSecret);
				var authenticationContext = new AuthenticationContext(authString, false);
				var authenticationResult = await authenticationContext.AcquireTokenAsync(graphUri, clientCredential);
				var token = authenticationResult.AccessToken;

				string nextLink = null;

				do
				{
					string content = string.Empty;
					using (var client = new HttpClient())
					{
						client.BaseAddress = new Uri(graphUri);
						client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

						var uri = nextLink ?? $"{apiVersion}/{tenantName}/users?$expand=manager($levels=1;$select=companyName,displayName,department,givenName,jobTitle,physicalDeliveryOfficeName,surname,userPrincipalName,id)&$select=companyName,displayName,department,givenName,jobTitle,physicalDeliveryOfficeName,surname,userPrincipalName,id";

						var result = await client.GetAsync(uri);
						if (!result.IsSuccessStatusCode) throw new Exception($"{result.Content.ReadAsStringAsync().Result}");

						content = await result.Content.ReadAsStringAsync();

						var jArray = JObject.Parse(content).Value<JArray>("value");
						profiles.AddRange(jArray.ToObject<List<AzureProfile>>());
					}

					// microsoft graph only allows 100 users per query
					// this allows retrieval of all users
					try
					{
						nextLink = JObject.Parse(content)?.Value<string>("@odata.nextLink");
					}
					catch
					{
						nextLink = null;
					}
				} while (!string.IsNullOrWhiteSpace(nextLink));
			}
			catch (AuthenticationException ex)
			{
				this.logger.LogCritical($"Acquiring a token failed with the following error: {ex.Message}");
				if (ex.InnerException != null) this.logger.LogError($"Error detail: {ex.InnerException.Message}");
			}
			catch (Exception ex)
			{
				this.logger.LogError($"Error getting users information: {ex}");
			}

			return profiles;
		}
	}
}