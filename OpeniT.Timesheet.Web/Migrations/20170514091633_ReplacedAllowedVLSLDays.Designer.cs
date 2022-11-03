using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using OpeniT.Timesheet.Web.Models;

namespace OpeniT.Timesheet.Web.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20170514091633_ReplacedAllowedVLSLDays")]
    partial class ReplacedAllowedVLSLDays
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.EmploymentCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("Description");

                    b.HasKey("Id");

                    b.ToTable("EmploymentCodes");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.PinnedRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<double>("Hours");

                    b.Property<string>("Location");

                    b.Property<string>("Owner");

                    b.Property<int?>("SubProcessId");

                    b.Property<string>("SubTask");

                    b.Property<string>("Task");

                    b.HasKey("Id");

                    b.HasIndex("SubProcessId");

                    b.ToTable("PinnedRecords");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.Process", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("Pid");

                    b.Property<string>("TaskUri");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("Processes");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.Record", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<double?>("EpochStart");

                    b.Property<double>("Hours");

                    b.Property<bool>("IsFromExcel");

                    b.Property<string>("Location");

                    b.Property<string>("Owner");

                    b.Property<int?>("SubProcessId");

                    b.Property<string>("SubTask");

                    b.Property<string>("Task");

                    b.HasKey("Id");

                    b.HasIndex("SubProcessId");

                    b.HasIndex("Owner", "Date");

                    b.ToTable("Records");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.SubProcess", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int?>("ProcessId");

                    b.Property<int>("SPid");

                    b.HasKey("Id");

                    b.HasIndex("ProcessId");

                    b.ToTable("SubProcesses");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<double>("DailyHoursRequired");

                    b.Property<string>("DisplayName");

                    b.Property<string>("Email");

                    b.Property<int?>("EmploymentCodeId");

                    b.Property<double>("ExcessHours");

                    b.Property<double>("MonthlyHoursRequired");

                    b.Property<int?>("UserLocationId");

                    b.Property<int?>("UserTypeId");

                    b.HasKey("Id");

                    b.HasIndex("Email");

                    b.HasIndex("EmploymentCodeId");

                    b.HasIndex("UserLocationId");

                    b.HasIndex("UserTypeId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.UserLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("AllowedSLDays");

                    b.Property<double>("AllowedVLDays");

                    b.Property<string>("Code");

                    b.Property<double>("DailyHours");

                    b.Property<double>("MonthlyHours");

                    b.Property<string>("Name");

                    b.Property<string>("RequiredHours");

                    b.HasKey("Id");

                    b.ToTable("UserLocations");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.UserType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Status");

                    b.HasKey("Id");

                    b.ToTable("UserTypes");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.PinnedRecord", b =>
                {
                    b.HasOne("OpeniT.Timesheet.Web.Models.SubProcess", "SubProcess")
                        .WithMany()
                        .HasForeignKey("SubProcessId");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.Record", b =>
                {
                    b.HasOne("OpeniT.Timesheet.Web.Models.SubProcess", "SubProcess")
                        .WithMany("Records")
                        .HasForeignKey("SubProcessId");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.SubProcess", b =>
                {
                    b.HasOne("OpeniT.Timesheet.Web.Models.Process", "Process")
                        .WithMany("SubProcesses")
                        .HasForeignKey("ProcessId");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.User", b =>
                {
                    b.HasOne("OpeniT.Timesheet.Web.Models.EmploymentCode", "EmploymentCode")
                        .WithMany()
                        .HasForeignKey("EmploymentCodeId");

                    b.HasOne("OpeniT.Timesheet.Web.Models.UserLocation", "UserLocation")
                        .WithMany()
                        .HasForeignKey("UserLocationId");

                    b.HasOne("OpeniT.Timesheet.Web.Models.UserType", "UserType")
                        .WithMany()
                        .HasForeignKey("UserTypeId");
                });
        }
    }
}
