namespace OpeniT.Timesheet.Web.Models
{
	using System.ComponentModel.DataAnnotations.Schema;

	public class UserThumbnail
	{
		public int Id { get; set; }

		public int UserId { get; set; }

		public string ContentType { get; set; }

		public byte[] Content { get; set; }

		public virtual User User { get; set; }
	}
}