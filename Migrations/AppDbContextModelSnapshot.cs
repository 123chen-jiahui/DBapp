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
                    b.Property<string>("Name")
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)")
                        .HasColumnName("NAME");

                    b.Property<string>("Building")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)")
                        .HasColumnName("BUILDING");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("NVARCHAR2(11)")
                        .HasColumnName("PHONE");

                    b.HasKey("Name");

                    b.ToTable("DEPARTMENTS");

                    b.HasData(
                        new
                        {
                            Name = "内科",
                            Building = "1号楼",
                            Phone = "11111111111"
                        },
                        new
                        {
                            Name = "儿科",
                            Building = "1号楼",
                            Phone = "22222222222"
                        },
                        new
                        {
                            Name = "皮肤科",
                            Building = "1号楼",
                            Phone = "33333333333"
                        },
                        new
                        {
                            Name = "急诊科",
                            Building = "1号楼",
                            Phone = "44444444444"
                        },
                        new
                        {
                            Name = "神经科",
                            Building = "2号楼",
                            Phone = "55555555555"
                        },
                        new
                        {
                            Name = "中医科",
                            Building = "2号楼",
                            Phone = "66666666666"
                        },
                        new
                        {
                            Name = "外科",
                            Building = "2号楼",
                            Phone = "77777777777"
                        },
                        new
                        {
                            Name = "眼科",
                            Building = "2号楼",
                            Phone = "88888888888"
                        },
                        new
                        {
                            Name = "口腔科",
                            Building = "3号楼",
                            Phone = "99999999999"
                        },
                        new
                        {
                            Name = "妇科",
                            Building = "3号楼",
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

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(20)")
                        .HasColumnName("DEPARTMENT_NAME");

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

                    b.HasIndex("DepartmentName");

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

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(20)")
                        .HasColumnName("DEPARTMENT_NAME");

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

                    b.HasIndex("DepartmentName");

                    b.ToTable("WARDS");
                });

            modelBuilder.Entity("Hospital.Models.Staff", b =>
                {
                    b.HasOne("Hospital.Models.Department", "Department")
                        .WithMany("Staff")
                        .HasForeignKey("DepartmentName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Hospital.Models.Ward", b =>
                {
                    b.HasOne("Hospital.Models.Department", "Department")
                        .WithMany("Wards")
                        .HasForeignKey("DepartmentName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Hospital.Models.Department", b =>
                {
                    b.Navigation("Staff");

                    b.Navigation("Wards");
                });
#pragma warning restore 612, 618
        }
    }
}
