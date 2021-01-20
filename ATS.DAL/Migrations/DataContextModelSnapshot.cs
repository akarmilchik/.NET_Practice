﻿// <auto-generated />
using System;
using ATS.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ATS.DAL.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("ATS.DAL.ModelsEntities.Billing.ClientEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("ATS.DAL.ModelsEntities.Billing.ContractEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Client_Id")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ContractCloseDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ContractStartDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("TariffPlan_Id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Terminal_Id")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("ATS.DAL.ModelsEntities.Billing.SecondMinuteTariffPlanEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("MinuteCost")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TariffPlans");
                });

            modelBuilder.Entity("ATS.DAL.ModelsEntities.CallDetailsEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Cost")
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("DurationTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Source")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartedTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Target")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CallsDetails");
                });

            modelBuilder.Entity("ATS.DAL.ModelsEntities.PortEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("PortState")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Ports");
                });

            modelBuilder.Entity("ATS.DAL.ModelsEntities.RequestEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("SourcePhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<int>("State")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("ATS.DAL.ModelsEntities.RespondEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Request_Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SourcePhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<int>("State")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Responds");
                });

            modelBuilder.Entity("ATS.DAL.ModelsEntities.StationEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Stations");
                });

            modelBuilder.Entity("ATS.DAL.ModelsEntities.TerminalEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsOnline")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<int>("ProvidedPort_Id")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Terminals");
                });

            modelBuilder.Entity("ATS.DAL.ModelsEntities.OutgoingRequestEntity", b =>
                {
                    b.HasBaseType("ATS.DAL.ModelsEntities.RequestEntity");

                    b.Property<string>("TargetPhoneNumber")
                        .HasColumnType("TEXT");

                    b.ToTable("OutgoingRequests");
                });

            modelBuilder.Entity("ATS.DAL.ModelsEntities.OutgoingRequestEntity", b =>
                {
                    b.HasOne("ATS.DAL.ModelsEntities.RequestEntity", null)
                        .WithOne()
                        .HasForeignKey("ATS.DAL.ModelsEntities.OutgoingRequestEntity", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
