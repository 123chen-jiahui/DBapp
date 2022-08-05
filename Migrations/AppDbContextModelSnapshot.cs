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

            modelBuilder.HasSequence("SEQ_LINEITEM_ID");

            modelBuilder.HasSequence("SEQ_PATIENT_ID")
                .StartsAt(1000000L);

            modelBuilder.HasSequence("SEQ_STAFF_ID")
                .StartsAt(2000000L);

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

            modelBuilder.Entity("Hospital.Models.LineItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID")
                        .UseHiLo("SEQ_LINEITEM_ID");

                    b.Property<string>("MedicineId")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(9)")
                        .HasColumnName("MEDICINE_ID");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("RAW(16)")
                        .HasColumnName("ORDER_ID");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("PRICE");

                    b.Property<Guid?>("ShoppingCartId")
                        .HasColumnType("RAW(16)")
                        .HasColumnName("SHOPPINGCART_ID");

                    b.HasKey("Id");

                    b.HasIndex("MedicineId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ShoppingCartId");

                    b.ToTable("LINEITEM");
                });

            modelBuilder.Entity("Hospital.Models.MedicalEquipment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("RAW(16)")
                        .HasColumnName("ID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("NAME");

                    b.Property<string>("Producer")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("PRODUCER");

                    b.Property<string>("RoomId")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(450)")
                        .HasColumnName("ROOM_ID");

                    b.Property<DateTime?>("StartUseTime")
                        .HasColumnType("TIMESTAMP(7)")
                        .HasColumnName("START_USE_TIME");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("MEDICAL_EQUIPMENTS");
                });

            modelBuilder.Entity("Hospital.Models.Medicine", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(9)
                        .HasColumnType("NVARCHAR2(9)")
                        .HasColumnName("ID");

                    b.Property<string>("Indications")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("NVARCHAR2(40)")
                        .HasColumnName("INDICATIONS");

                    b.Property<int>("Inventory")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("INVENTORY");

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("MANUFACTURER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("NVARCHAR2(40)")
                        .HasColumnName("NAME");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("PRICE");

                    b.HasKey("Id");

                    b.ToTable("MEDICINE");

                    b.HasData(
                        new
                        {
                            Id = "H19994016",
                            Indications = "有炎症的患者",
                            Inventory = 500,
                            Manufacturer = "昆明贝克诺顿制药有限公司",
                            Name = "阿莫西林克拉维酸钾片",
                            Price = 20.00m
                        },
                        new
                        {
                            Id = "Z20040063",
                            Indications = "用于治疗流行性感冒属热毒袭肺症",
                            Inventory = 200,
                            Manufacturer = "石家庄以岭药业股份有限公司",
                            Name = "连花清瘟胶囊",
                            Price = 15.00m
                        },
                        new
                        {
                            Id = "Z50020615",
                            Indications = "用于外感风热所致的咳嗽",
                            Inventory = 350,
                            Manufacturer = "太极集团重庆涪陵制药厂有限公司",
                            Name = "急支糖浆",
                            Price = 25.00m
                        });
                });

            modelBuilder.Entity("Hospital.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("RAW(16)")
                        .HasColumnName("ID");

                    b.Property<DateTime>("CreateDateUTC")
                        .HasColumnType("TIMESTAMP(7)")
                        .HasColumnName("CREATE_DATE_UTC");

                    b.Property<int>("PatientId")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("PATIENT_ID");

                    b.Property<int>("State")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("STATE");

                    b.Property<string>("TransactionMetadata")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("TRANSACTION_METADATA");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("ORDERS");
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

            modelBuilder.Entity("Hospital.Models.PurchaseList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("RAW(16)")
                        .HasColumnName("ID");

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("COST");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TIMESTAMP(7)")
                        .HasColumnName("DATE");

                    b.Property<int>("StaffId")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("STAFF_ID");

                    b.HasKey("Id");

                    b.HasIndex("StaffId");

                    b.ToTable("PURCHASE_LISTS");
                });

            modelBuilder.Entity("Hospital.Models.PurchaseListItem", b =>
                {
                    b.Property<Guid>("PurchaseListId")
                        .HasColumnType("RAW(16)")
                        .HasColumnName("PURCHASE_LIST_ID");

                    b.Property<string>("Description")
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("DESCRIPTION");

                    b.Property<string>("ItemId")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("ITEM_ID");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("ITEM_NAME");

                    b.Property<int>("Number")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("NUMBER");

                    b.Property<decimal>("Univalent")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("UNIVALENT");

                    b.HasKey("PurchaseListId");

                    b.ToTable("PURCHASE_LIST_ITEMS");
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

                    b.Property<int>("StaffId")
                        .HasColumnType("NUMBER(10)")
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

            modelBuilder.Entity("Hospital.Models.Room", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("NVARCHAR2(450)")
                        .HasColumnName("ID");

                    b.Property<string>("Building")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR2(20)")
                        .HasColumnName("BUILDING");

                    b.Property<int>("RoomType")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ROOMTYPE");

                    b.HasKey("Id");

                    b.ToTable("ROOMS");
                });

            modelBuilder.Entity("Hospital.Models.ShoppingCart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("RAW(16)")
                        .HasColumnName("ID");

                    b.Property<int>("PatientId")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("PATIENT_ID");

                    b.HasKey("Id");

                    b.HasIndex("PatientId")
                        .IsUnique();

                    b.ToTable("SHOPPINGCART");
                });

            modelBuilder.Entity("Hospital.Models.Staff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID")
                        .UseHiLo("SEQ_STAFF_ID");

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

            modelBuilder.Entity("Hospital.Models.LineItem", b =>
                {
                    b.HasOne("Hospital.Models.Medicine", "Medicine")
                        .WithMany()
                        .HasForeignKey("MedicineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hospital.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId");

                    b.HasOne("Hospital.Models.ShoppingCart", "ShoppingCart")
                        .WithMany("ShoppingCartItems")
                        .HasForeignKey("ShoppingCartId");

                    b.Navigation("Medicine");

                    b.Navigation("Order");

                    b.Navigation("ShoppingCart");
                });

            modelBuilder.Entity("Hospital.Models.MedicalEquipment", b =>
                {
                    b.HasOne("Hospital.Models.Room", "Room")
                        .WithMany("MedicalEquipments")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");
                });

            modelBuilder.Entity("Hospital.Models.Order", b =>
                {
                    b.HasOne("Hospital.Models.Patient", "Patient")
                        .WithMany("Orders")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Hospital.Models.PurchaseList", b =>
                {
                    b.HasOne("Hospital.Models.Staff", "Staff")
                        .WithMany("PurchaseLists")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("Hospital.Models.PurchaseListItem", b =>
                {
                    b.HasOne("Hospital.Models.PurchaseList", "PurchaseList")
                        .WithMany("PurchaseListItems")
                        .HasForeignKey("PurchaseListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PurchaseList");
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

            modelBuilder.Entity("Hospital.Models.ShoppingCart", b =>
                {
                    b.HasOne("Hospital.Models.Patient", "Patient")
                        .WithOne("ShoppingCart")
                        .HasForeignKey("Hospital.Models.ShoppingCart", "PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
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

            modelBuilder.Entity("Hospital.Models.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("Hospital.Models.Patient", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Registrations");

                    b.Navigation("ShoppingCart");
                });

            modelBuilder.Entity("Hospital.Models.PurchaseList", b =>
                {
                    b.Navigation("PurchaseListItems");
                });

            modelBuilder.Entity("Hospital.Models.Room", b =>
                {
                    b.Navigation("MedicalEquipments");
                });

            modelBuilder.Entity("Hospital.Models.ShoppingCart", b =>
                {
                    b.Navigation("ShoppingCartItems");
                });

            modelBuilder.Entity("Hospital.Models.Staff", b =>
                {
                    b.Navigation("PurchaseLists");

                    b.Navigation("Registrations");
                });
#pragma warning restore 612, 618
        }
    }
}
