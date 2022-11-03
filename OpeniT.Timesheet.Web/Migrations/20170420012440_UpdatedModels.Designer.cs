using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using OpeniT.Timesheet.Web.Models;

namespace OpeniT.Timesheet.Web.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20170420012440_UpdatedModels")]
    partial class UpdatedModels
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.Process", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("Pid");

                    b.HasKey("Id");

                    b.ToTable("Processes");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.Record", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<double>("Hours");

                    b.Property<string>("Location");

                    b.Property<string>("Owner");

                    b.Property<int?>("SubProcessId");

                    b.Property<string>("SubTask");

                    b.Property<string>("Task");

                    b.HasKey("Id");

                    b.HasIndex("SubProcessId");

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

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.Record", b =>
                {
                    b.HasOne("OpeniT.Timesheet.Web.Models.SubProcess", "SubProcess")
                        .WithMany()
                        .HasForeignKey("SubProcessId");
                });

            modelBuilder.Entity("OpeniT.Timesheet.Web.Models.SubProcess", b =>
                {
                    b.HasOne("OpeniT.Timesheet.Web.Models.Process")
                        .WithMany("SubProcesses")
                        .HasForeignKey("ProcessId");
                });
        }
    }
}
