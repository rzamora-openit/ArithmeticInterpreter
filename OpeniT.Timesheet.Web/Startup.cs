namespace OpeniT.Timesheet.Web
{
	using System;
	using AutoMapper;
	using Hangfire;
	using Helpers;
	using Microsoft.AspNetCore.Authentication.Cookies;
	using Microsoft.AspNetCore.Authentication.OpenIdConnect;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Rewrite;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Logging;
	using Models;
	using Newtonsoft.Json;
    public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", false, true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
				.AddEnvironmentVariables();
			this.Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// Add framework services.
			services.AddSingleton(this.Configuration);
			services.AddDbContext<DataContext>();
            services.AddTransient<AzureHelper>();
            services.AddScoped<IDataRepository, DataRepository>();
			services.AddScoped<IDataPerformance, DataPerformance>();
			services.AddScoped<IAuthorizationHandler, UserAuthorizationHandler>();
			services.AddLogging();

			services.Configure<CookiePolicyOptions>(options =>
			{
                // This lambda determines whether user consent for non-essential cookies 
                // is needed for a given request.
                options.CheckConsentNeeded = context => true;

				// This lambda determines whether user consent for non-essential cookies 
				// is needed for a given request.
				options.CheckConsentNeeded = context => true;
				//options.MinimumSameSitePolicy = SameSiteMode.None;
				options.MinimumSameSitePolicy = (SameSiteMode)(-1);
				options.OnAppendCookie = cookieContext =>
				{
					if (cookieContext.CookieOptions.SameSite == SameSiteMode.None) cookieContext.CookieOptions.SameSite = (SameSiteMode)(-1);
				};
				options.OnDeleteCookie = cookieContext =>
				{
					if (cookieContext.CookieOptions.SameSite == SameSiteMode.None) cookieContext.CookieOptions.SameSite = (SameSiteMode)(-1);
				};
			});

			services.AddAuthentication(
					options =>
					{
						options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
						options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
					})
				.AddCookie(
					options =>
					{
						options.LoginPath = new PathString("/Login");
						options.AccessDeniedPath = new PathString("/Home/AccessDenied");
					})
				.AddOpenIdConnect(
					options =>
					{
						options.Authority = this.Configuration["Microsoft:Authority"];
						options.ClientId = this.Configuration["Microsoft:ClientId"];
						options.SignedOutCallbackPath = new PathString("/Login");
					});

			services.AddAuthorization(
				options =>
				{
					options.AddPolicy("PersistedUser",
						policy => policy.Requirements.Add(new UserAuthorizationRequirement()));
				});

			services.AddDistributedMemoryCache();
			services.AddSession(
				options =>
				{
					options.IdleTimeout = TimeSpan.FromSeconds(10);
					options.Cookie.HttpOnly = true;
				});

			services.AddHttpContextAccessor();

			services.AddApplicationInsightsTelemetry(this.Configuration);
			services.AddAutoMapper();
			services.AddMvc()
				.AddJsonOptions(
					options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

			services.AddHangfire(x => x.UseSqlServerStorage(this.Configuration["ConnectionStrings:DataConnection"]));
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSignalR();

            services.Configure<FormOptions>(options =>
            {
                options.MemoryBufferThreshold = int.MaxValue;
                options.ValueCountLimit = 1024; //default 1024
                options.ValueLengthLimit = int.MaxValue; //not recommended value
                options.MultipartBodyLengthLimit = int.MaxValue; //not recommended value
                options.BufferBodyLengthLimit = int.MaxValue;
            });
        }

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(
			IApplicationBuilder app,
			IHostingEnvironment env,
			ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseRewriter(new RewriteOptions().AddRedirectToHttps());
				app.UseHsts();
			}

			app.UseCookiePolicy();
			app.UseAuthentication();
			app.UseHangfireServer();

			app.UseSession();
			app.UseStaticFiles();
			app.UseMvc(
				routes =>
				{
					routes.MapRoute("areaRoute", "{area:exists}/{controller=Admin}/{action=Index}/{id?}");
					routes.MapRoute("default", "{controller=Statistics}/{action=Index}/{id?}");
				});			

		}
	}
}