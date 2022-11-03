﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OpeniT.Timesheet.Web.Models;

namespace OpeniT.Timesheet.Web.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20181108233421_AddedProcessUrl")]
    partial class AddedProcessUrl
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.DeviationActivity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment");

                    b.Property<DateTimeOffset>("Date");

                    b.Property<int?>("DeviationReportId");

                    b.Property<string>("Type");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("DeviationReportId");

                    b.HasIndex("UserId");

                    b.ToTable("DeviationActivities");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.DeviationReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("Date");

                    b.Property<string>("Description");

                    b.Property<int?>("ProcessId");

                    b.Property<string>("ProcessUrl");

                    b.Property<int?>("ReportedById");

                    b.Property<bool>("SeenByOwner");

                    b.Property<bool>("SeenBySubOwner");

                    b.Property<string>("Status");

                    b.Property<int?>("SubProcessId");

                    b.Property<string>("Subject");

                    b.Property<int?>("UpdatedById");

                    b.HasKey("Id");

                    b.HasIndex("ProcessId");

                    b.HasIndex("ReportedById");

                    b.HasIndex("SubProcessId");

                    b.HasIndex("UpdatedById");

                    b.ToTable("DeviationReports");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.EmploymentCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<string>("Description");

                    b.HasKey("Id");

                    b.ToTable("EmploymentCodes");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AllDay");

                    b.Property<DateTime?>("End");

                    b.Property<DateTime>("Start");

                    b.Property<string>("Title");

                    b.Property<string>("Type");

                    b.Property<int?>("UserLocationId");

                    b.HasKey("Id");

                    b.HasIndex("UserLocationId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.KeyResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTimeOffset?>("LastUpdate");

                    b.Property<int?>("ObjectiveId");

                    b.Property<double>("Progress");

                    b.HasKey("Id");

                    b.HasIndex("ObjectiveId");

                    b.ToTable("KeyResults");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.MidshiftSummary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count");

                    b.Property<DateTime>("Month");

                    b.Property<DateTimeOffset?>("SubmittedOn");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("MidshiftSummaries");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.Objective", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTimeOffset?>("LastUpdate");

                    b.Property<int?>("ParentId");

                    b.Property<int?>("ProcessId");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.HasIndex("ProcessId");

                    b.HasIndex("UserId");

                    b.ToTable("Objectives");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.PinnedRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<double>("Hours");

                    b.Property<string>("Location");

                    b.Property<int>("OrderBy");

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
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DeputyOwnerId");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<int?>("OwnerId");

                    b.Property<int>("Pid");

                    b.Property<string>("TaskUri");

                    b.Property<string>("TaskUriPrefix");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("DeputyOwnerId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Processes");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("EndDate");

                    b.Property<bool>("IsDone");

                    b.Property<string>("Package");

                    b.Property<int>("ProjectId");

                    b.Property<string>("ProjectName");

                    b.Property<string>("Region");

                    b.Property<DateTime?>("StartDate");

                    b.Property<string>("Status");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.PublishedEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AffectedRows");

                    b.Property<DateTimeOffset?>("EndedAt");

                    b.Property<int>("HangfireJobId");

                    b.Property<int?>("PublishedById");

                    b.Property<DateTimeOffset>("StartedAt");

                    b.Property<string>("Status");

                    b.Property<string>("Title");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("PublishedById");

                    b.ToTable("PublishedEvents");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.Record", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<double?>("EpochStart");

                    b.Property<int>("HangfireJobId");

                    b.Property<double>("Hours");

                    b.Property<int?>("InsertedById");

                    b.Property<bool>("IsFromExcel");

                    b.Property<bool>("IsLocked");

                    b.Property<string>("Location");

                    b.Property<string>("Owner");

                    b.Property<int?>("SubProcessId");

                    b.Property<string>("SubTask");

                    b.Property<string>("Task");

                    b.HasKey("Id");

                    b.HasIndex("InsertedById");

                    b.HasIndex("SubProcessId");

                    b.HasIndex("Owner", "Date");

                    b.ToTable("Records");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.RecordSummary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Difference");

                    b.Property<double>("ExcessHours");

                    b.Property<double>("Hours");

                    b.Property<DateTime>("Month");

                    b.Property<double>("RequiredHours");

                    b.Property<double>("SalaryDeduction");

                    b.Property<DateTimeOffset?>("SubmittedOn");

                    b.Property<double>("TotalHours");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RecordSummaries");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.Request", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ApprovedById");

                    b.Property<DateTime?>("ApprovedDate");

                    b.Property<DateTime>("Date");

                    b.Property<DateTime?>("EndDate");

                    b.Property<double>("Hours");

                    b.Property<int?>("RequestorId");

                    b.Property<DateTime?>("StartDate");

                    b.Property<string>("Status");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("ApprovedById");

                    b.HasIndex("RequestorId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.SubProcess", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DeputyOwnerId");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<int?>("OwnerId");

                    b.Property<int?>("ProcessId");

                    b.Property<int>("SPid");

                    b.HasKey("Id");

                    b.HasIndex("DeputyOwnerId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("ProcessId");

                    b.ToTable("SubProcesses");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.TaskGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("CreatedAt");

                    b.Property<string>("CreatedBy");

                    b.Property<bool>("IsDefault");

                    b.Property<string>("Name");

                    b.Property<int?>("ProcessId");

                    b.HasKey("Id");

                    b.HasIndex("ProcessId");

                    b.ToTable("TaskGroups");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.TimeLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<double?>("Duration");

                    b.Property<string>("Owner");

                    b.Property<TimeSpan?>("TimeIn");

                    b.Property<TimeSpan?>("TimeOut");

                    b.HasKey("Id");

                    b.ToTable("TimeLogs");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.TimeLogSummary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Month");

                    b.Property<double>("NightDiffTotal");

                    b.Property<DateTimeOffset?>("SubmittedOn");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("TimeLogSummaries");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("CreatedAt");

                    b.Property<string>("Department");

                    b.Property<string>("DisplayName");

                    b.Property<string>("Email");

                    b.Property<int?>("EmploymentCodeId");

                    b.Property<bool>("IsImportAllowed");

                    b.Property<string>("JobTitle");

                    b.Property<DateTimeOffset?>("LastLogin");

                    b.Property<DateTimeOffset?>("LastUpdate");

                    b.Property<int?>("LineManagerId");

                    b.Property<DateTime?>("RegularizationDate");

                    b.Property<DateTimeOffset?>("UpdatedAt");

                    b.Property<int?>("UserLocationId");

                    b.Property<int?>("UserTypeId");

                    b.HasKey("Id");

                    b.HasIndex("Email");

                    b.HasIndex("EmploymentCodeId");

                    b.HasIndex("LineManagerId");

                    b.HasIndex("UserLocationId");

                    b.HasIndex("UserTypeId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.UserContract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("AllowedSLDays");

                    b.Property<double>("AllowedVLDays");

                    b.Property<double>("DailyHoursRequired");

                    b.Property<double>("MonthlyHoursRequired");

                    b.Property<int?>("UserId");

                    b.Property<DateTime?>("ValidFrom");

                    b.Property<DateTime?>("ValidUntil");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserContracts");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.UserExcessHours", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Hours");

                    b.Property<int>("UserId");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserExcessHours");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.UserExcessVL", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Hours");

                    b.Property<int>("UserId");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserExcessVL");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.UserLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("AllowedSLDays");

                    b.Property<double>("AllowedTODays");

                    b.Property<double>("AllowedVLDays");

                    b.Property<string>("ClassName");

                    b.Property<string>("Code");

                    b.Property<double>("DailyHours");

                    b.Property<bool>("IsDeleted");

                    b.Property<double>("MonthlyHours");

                    b.Property<string>("Name");

                    b.Property<string>("RequiredHours");

                    b.HasKey("Id");

                    b.ToTable("UserLocations");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.UserThumbnail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("Content");

                    b.Property<string>("ContentType");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserThumbnails");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.UserType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Status");

                    b.HasKey("Id");

                    b.ToTable("UserTypes");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.DeviationActivity", b =>
                {
                    b.HasOne("OpeniT.Timesheet.Web.Models.DeviationReport", "DeviationReport")
                        .WithMany("DeviationActivities")
                        .HasForeignKey("DeviationReportId");

                    b.HasOne("OpeniT.Timesheet.Web.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.DeviationReport", b =>
                {
                    b.HasOne("OpeniT.Timesheet.Web.Models.Process", "Process")
                        .WithMany()
                        .HasForeignKey("ProcessId");

                    b.HasOne("OpeniT.Timesheet.Web.Models.User", "ReportedBy")
                        .WithMany()
                        .HasForeignKey("ReportedById");

                    b.HasOne("OpeniT.Timesheet.Web.Models.SubProcess", "SubProcess")
                        .WithMany()
                        .HasForeignKey("SubProcessId");

                    b.HasOne("OpeniT.Timesheet.Web.Models.User", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.Event", b =>
                {
                    b.HasOne("OpeniT.Timesheet.Web.Models.UserLocation", "UserLocation")
                        .WithMany()
                        .HasForeignKey("UserLocationId");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.KeyResult", b =>
                {
                    b.HasOne("OpeniT.Timesheet.Web.Models.Objective", "Objective")
                        .WithMany("KeyResults")
                        .HasForeignKey("ObjectiveId");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.MidshiftSummary", b =>
                {
                    b.HasOne("OpeniT.Timesheet.Web.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.Objective", b =>
                {
                    b.HasOne("OpeniT.Timesheet.Web.Models.Objective", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.HasOne("OpeniT.Timesheet.Web.Models.Process", "Process")
                        .WithMany()
                        .HasForeignKey("ProcessId");

                    b.HasOne("OpeniT.Timesheet.Web.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.PinnedRecord", b =>
                {
                    b.HasOne("OpeniT.Timesheet.Web.Models.SubProcess", "SubProcess")
                        .WithMany()
                        .HasForeignKey("SubProcessId");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.Process", b =>
                {
                    b.HasOne("OpeniT.Timesheet.Web.Models.User", "DeputyOwner")
                        .WithMany()
                        .HasForeignKey("DeputyOwnerId");

                    b.HasOne("OpeniT.Timesheet.Web.Models.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.PublishedEvent", b =>
                {
                    b.HasOne("OpeniT.Timesheet.Web.Models.User", "PublishedBy")
                        .WithMany()
                        .HasForeignKey("PublishedById");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.Record", b =>
                {
                    b.HasOne("OpeniT.Timesheet.Web.Models.User", "InsertedBy")
                        .WithMany()
                        .HasForeignKey("InsertedById");

                    b.HasOne("OpeniT.Timesheet.Web.Models.SubProcess", "SubProcess")
                        .WithMany("Records")
                        .HasForeignKey("SubProcessId");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.RecordSummary", b =>
                {
                    b.HasOne("OpeniT.Timesheet.Web.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.Request", b =>
                {
                    b.HasOne("OpeniT.Timesheet.Web.Models.User", "ApprovedBy")
                        .WithMany()
                        .HasForeignKey("ApprovedById");

                    b.HasOne("OpeniT.Timesheet.Web.Models.User", "Requestor")
                        .WithMany()
                        .HasForeignKey("RequestorId");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.SubProcess", b =>
                {
                    b.HasOne("OpeniT.Timesheet.Web.Models.User", "DeputyOwner")
                        .WithMany()
                        .HasForeignKey("DeputyOwnerId");

                    b.HasOne("OpeniT.Timesheet.Web.Models.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");

                    b.HasOne("OpeniT.Timesheet.Web.Models.Process", "Process")
                        .WithMany("SubProcesses")
                        .HasForeignKey("ProcessId");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.TaskGroup", b =>
                {
                    b.HasOne("OpeniT.Timesheet.Web.Models.Process", "Process")
                        .WithMany("TaskGroups")
                        .HasForeignKey("ProcessId");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.TimeLogSummary", b =>
                {
                    b.HasOne("OpeniT.Timesheet.Web.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.User", b =>
                {
                    b.HasOne("OpeniT.Timesheet.Web.Models.EmploymentCode", "EmploymentCode")
                        .WithMany()
                        .HasForeignKey("EmploymentCodeId");

                    b.HasOne("OpeniT.Timesheet.Web.Models.User", "LineManager")
                        .WithMany()
                        .HasForeignKey("LineManagerId");

                    b.HasOne("OpeniT.Timesheet.Web.Models.UserLocation", "UserLocation")
                        .WithMany()
                        .HasForeignKey("UserLocationId");

                    b.HasOne("OpeniT.Timesheet.Web.Models.UserType", "UserType")
                        .WithMany()
                        .HasForeignKey("UserTypeId");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.UserContract", b =>
                {
                    b.HasOne("OpeniT.Timesheet.Web.Models.User", "User")
                        .WithMany("UserContracts")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.UserExcessHours", b =>
                {
                    b.HasOne("OpeniT.Timesheet.Web.Models.User")
                        .WithMany("ExcessHours")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.UserExcessVL", b =>
                {
                    b.HasOne("OpeniT.Timesheet.Web.Models.User")
                        .WithMany("ExcessVL")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.UserThumbnail", b =>
                {
                    b.HasOne("OpeniT.Timesheet.Web.Models.User", "User")
                        .WithOne("Thumbnail")
                        .HasForeignKey("OpeniT.Timesheet.Web.Models.UserThumbnail", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
