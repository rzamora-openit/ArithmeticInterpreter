namespace OpeniT.Timesheet.Web.Models
{
	public class UserExcessHours
	{
		public int Id { get; set; }

		public int UserId { get; set; }

		public int Year { get; set; }

		public double Hours { get; set; }
	}
}