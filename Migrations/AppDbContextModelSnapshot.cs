﻿// <auto-generated />
using System;
using Hospital.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

namespace Hospital.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.HasSequence("SEQ_PATIENT_ID")
                .StartsAt(1000000L);

            modelBuilder.Entity("Hospital.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID")
                        .UseIdentityColumn();

                    b.Property<string>("Building")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)")
                        .HasColumnName("BUILDING");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)")
                        .HasColumnName("NAME");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("NVARCHAR2(11)")
                        .HasColumnName("PHONE");

                    b.HasKey("Id");

                    b.ToTable("DEPARTMENTS");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Building = "1号楼",
                            Name = "内科",
                            Phone = "11111111111"
                        },
                        new
                        {
                            Id = 2,
                            Building = "1号楼",
                            Name = "儿科",
                            Phone = "22222222222"
                        },
                        new
                        {
                            Id = 3,
                            Building = "1号楼",
                            Name = "皮肤科",
                            Phone = "33333333333"
                        },
                        new
                        {
                            Id = 4,
                            Building = "1号楼",
                            Name = "急诊科",
                            Phone = "44444444444"
                        },
                        new
                        {
                            Id = 5,
                            Building = "2号楼",
                            Name = "神经科",
                            Phone = "55555555555"
                        },
                        new
                        {
                            Id = 6,
                            Building = "2号楼",
                            Name = "中医科",
                            Phone = "66666666666"
                        },
                        new
                        {
                            Id = 7,
                            Building = "2号楼",
                            Name = "外科",
                            Phone = "77777777777"
                        },
                        new
                        {
                            Id = 8,
                            Building = "2号楼",
                            Name = "眼科",
                            Phone = "88888888888"
                        },
                        new
                        {
                            Id = 9,
                            Building = "3号楼",
                            Name = "口腔科",
                            Phone = "99999999999"
                        },
                        new
                        {
                            Id = 10,
                            Building = "3号楼",
                            Name = "妇科",
                            Phone = "00000000000"
                        });
                });

            modelBuilder.Entity("Hospital.Models.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID")
                        .UseHiLo("SEQ_PATIENT_ID");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("TIMESTAMP(7)")
                        .HasColumnName("BIRTHDAY");

                    b.Property<int>("Gender")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("GENDER");

                    b.Property<string>("GlobalId")
                        .IsRequired()
                        .HasMaxLength(18)
                        .HasColumnType("NVARCHAR2(18)")
                        .HasColumnName("GLOBAL_ID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("NAME");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("NVARCHAR2(15)")
                        .HasColumnName("PASSWORD");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("NVARCHAR2(11)")
                        .HasColumnName("PHONE");

                    b.Property<string>("WardId")
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("WARD_ID");

                    b.HasKey("Id");

                    b.ToTable("PATIENTS");
                });

            modelBuilder.Entity("Hospital.Models.Registration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("RAW(16)")
                        .HasColumnName("ID");

                    b.Property<int>("PatientId")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("PATIENT_ID");

                    b.Property<string>("StaffId")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(20)")
                        .HasColumnName("STAFF_ID");

                    b.Property<DateTime>("Time")
                        .HasColumnType("TIMESTAMP(7)")
                        .HasColumnName("TIME");

                    b.Property<decimal>("fee")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("FEE");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.HasIndex("StaffId");

                    b.ToTable("REGISTRATIONS");
                });

            modelBuilder.Entity("Hospital.Models.Staff", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)")
                        .HasColumnName("ID");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("NVARCHAR2(80)")
                        .HasColumnName("ADDRESS");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("TIMESTAMP(7)")
                        .HasColumnName("BIRTHDAY");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("DEPARTMENT_ID");

                    b.Property<int>("Gender")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("GENDER");

                    b.Property<string>("GlobalId")
                        .IsRequired()
                        .HasMaxLength(18)
                        .HasColumnType("NVARCHAR2(18)")
                        .HasColumnName("GLOBAL_ID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("NAME");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("NVARCHAR2(15)")
                        .HasColumnName("PASSWORD");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("NVARCHAR2(11)")
                        .HasColumnName("PHONE");

                    b.Property<int>("Role")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ROLE");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("STAFF");
                });

            modelBuilder.Entity("Hospital.Models.Ward", b =>
                {
                    b.Property<string>("WardId")
                        .HasMaxLength(4)
                        .HasColumnType("NVARCHAR2(4)")
                        .HasColumnName("WARD_ID");

                    b.Property<string>("Building")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)")
                        .HasColumnName("BUILDING");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("DEPARTMENT_ID");

                    b.Property<int>("EndNum")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ENDNUM");

                    b.Property<int>("StartNum")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("STARTNUM");

                    b.Property<int>("Type")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("TYPE");

                    b.HasKey("WardId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("WARDS");
                });

            modelBuilder.Entity("Hospital.Models.Registration", b =>
                {
                    b.HasOne("Hospital.Models.Patient", "Patient")
                        .WithMany("Registrations")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hospital.Models.Staff", "Staff")
                        .WithMany("Registrations")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("Hospital.Models.Staff", b =>
                {
                    b.HasOne("Hospital.Models.Department", "Department")
                        .WithMany("Staff")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Hospital.Models.Ward", b =>
                {
                    b.HasOne("Hospital.Models.Department", "Department")
                        .WithMany("Wards")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Hospital.Models.Department", b =>
                {
                    b.Navigation("Staff");

                    b.Navigation("Wards");
                });

            modelBuilder.Entity("Hospital.Models.Patient", b =>
                {
                    b.Navigation("Registrations");
                });

            modelBuilder.Entity("Hospital.Models.Staff", b =>
                {
                    b.Navigation("Registrations");
                });
#pragma warning restore 612, 618
        }
    }
}
