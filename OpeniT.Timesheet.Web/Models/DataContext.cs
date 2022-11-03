using Microsoft.EntityFrameworkCore.Internal;

namespace OpeniT.Timesheet.Web.Models
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Configuration;

	public class DataContext : DbContext
	{
		private readonly IConfigurationRoot config;

		public DataContext(IConfigurationRoot config, DbContextOptions options)
			: base(options)
		{
			this.config = config;
		}

        public DbSet<EmploymentCode> EmploymentCodes { get; set; }
		public DbSet<Process> Processes { get; set; }
		public DbSet<SubProcess> SubProcesses { get; set; }
		public DbSet<Record> Records { get; set; }
		public DbSet<RecordSummary> RecordSummaries { get; set; }
		public DbSet<Request> Requests { get; set; }
		public DbSet<UserContract> UserContracts { get; set; }
		public DbSet<UserExcessHours> UserExcessHours { get; set; }
		public DbSet<UserExcessVL> UserExcessVL { get; set; }
		public DbSet<UserLocation> UserLocations { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<UserThumbnail> UserThumbnails { get; set; }
		public DbSet<UserType> UserTypes { get; set; }
		public DbSet<TaskGroup> TaskGroups { get; set; }
		public DbSet<SiteValues> SiteValues { get; set; }
		public DbSet<Role> Roles { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			optionsBuilder.UseSqlServer(this.config["ConnectionStrings:DataConnection"]);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Record>().HasIndex(r => new { r.Owner, r.Date });
			modelBuilder.Entity<User>().HasIndex(u => u.Email);
			modelBuilder.Entity<User>().HasOne(u => u.Thumbnail).WithOne(t => t.User)
				.HasForeignKey<UserThumbnail>(t => t.UserId);
		}
	}
}