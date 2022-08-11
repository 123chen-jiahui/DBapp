﻿// <auto-generated />
using System;
using Hospital.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

namespace Hospital.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220811115608_AddCapacity")]
    partial class AddCapacity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

            modelBuilder.HasSequence("SEQ_TIMESLOT_ID");

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

                    b.Property<int?>("TimeSlotId")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("TimeSlotId");

                    b.ToTable("STAFF");

                    b.HasData(
                        new
                        {
                            Id = 2000021,
                            Address = "陕西省咸阳市永寿县",
                            Birthday = new DateTime(1988, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentId = 1,
                            Gender = 0,
                            GlobalId = "665850236429837525",
                            Name = "向紫槐",
                            Password = "RW7fGxEV34d",
                            Phone = "93993366799",
                            Role = 0
                        },
                        new
                        {
                            Id = 2000022,
                            Address = "湖北省十堰市勋县",
                            Birthday = new DateTime(1987, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentId = 2,
                            Gender = 1,
                            GlobalId = "304894851618147779",
                            Name = "罗亚梅",
                            Password = "adhk7__dKX",
                            Phone = "91167597036",
                            Role = 1
                        },
                        new
                        {
                            Id = 2000023,
                            Address = "江苏省连云港市赣榆县",
                            Birthday = new DateTime(1986, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentId = 3,
                            Gender = 0,
                            GlobalId = "986261509125943643",
                            Name = "陆田然",
                            Password = "(7vpTeu!b",
                            Phone = "94341610852",
                            Role = 2
                        },
                        new
                        {
                            Id = 2000024,
                            Address = "湖北省黄冈市罗田县",
                            Birthday = new DateTime(1981, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentId = 4,
                            Gender = 1,
                            GlobalId = "962041149765665058",
                            Name = "邹萧玉",
                            Password = "w*^RviPyFC",
                            Phone = "66558976140",
                            Role = 1
                        },
                        new
                        {
                            Id = 2000025,
                            Address = "陕西省延安市延长县",
                            Birthday = new DateTime(1977, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentId = 5,
                            Gender = 0,
                            GlobalId = "333867359137450567",
                            Name = "姚密如",
                            Password = "fXjpNbQU",
                            Phone = "26006740883",
                            Role = 2
                        },
                        new
                        {
                            Id = 2000026,
                            Address = "四川省乐山市犍为县",
                            Birthday = new DateTime(1989, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentId = 6,
                            Gender = 1,
                            GlobalId = "858384696885017237",
                            Name = "双娴淑",
                            Password = "erCnaFOM%p",
                            Phone = "91711269117",
                            Role = 1
                        },
                        new
                        {
                            Id = 2000027,
                            Address = "陕西省汉中市留坝县",
                            Birthday = new DateTime(1978, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentId = 7,
                            Gender = 0,
                            GlobalId = "481091333354292218",
                            Name = "顾佩兰",
                            Password = "c9XISFcIVhvW",
                            Phone = "11119062912",
                            Role = 1
                        },
                        new
                        {
                            Id = 2000028,
                            Address = "山东省济宁市金乡县",
                            Birthday = new DateTime(1978, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentId = 8,
                            Gender = 1,
                            GlobalId = "714094264311323885",
                            Name = "武灵枫",
                            Password = "zYbL1q~sR",
                            Phone = "75857654588",
                            Role = 2
                        },
                        new
                        {
                            Id = 2000029,
                            Address = "湖南省长沙市宁乡县",
                            Birthday = new DateTime(1985, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentId = 9,
                            Gender = 0,
                            GlobalId = "254709473335247146",
                            Name = "蒲依心",
                            Password = "STI3x~jOQW",
                            Phone = "64642789641",
                            Role = 0
                        },
                        new
                        {
                            Id = 2000030,
                            Address = "山东省临沂市平邑县",
                            Birthday = new DateTime(1988, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentId = 10,
                            Gender = 1,
                            GlobalId = "888034902337818811",
                            Name = "印香卉",
                            Password = "6nH4n(MQr",
                            Phone = "86209733191",
                            Role = 1
                        },
                        new
                        {
                            Id = 2000031,
                            Address = "河北省邢台市柏乡县",
                            Birthday = new DateTime(1977, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentId = 1,
                            Gender = 0,
                            GlobalId = "380234543475013804",
                            Name = "王紫菱",
                            Password = "e_NkkZ9xe^sk",
                            Phone = "56644803281",
                            Role = 1
                        },
                        new
                        {
                            Id = 2000032,
                            Address = "江苏省淮阴市邻水县",
                            Birthday = new DateTime(1977, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentId = 2,
                            Gender = 1,
                            GlobalId = "931807544686001441",
                            Name = "万忆山",
                            Password = "84H0OLa%BQ2!",
                            Phone = "25428049805",
                            Role = 1
                        },
                        new
                        {
                            Id = 2000033,
                            Address = "安徽省安庆市岳西县",
                            Birthday = new DateTime(1979, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentId = 3,
                            Gender = 0,
                            GlobalId = "679203722177881099",
                            Name = "邹晶辉",
                            Password = "v_yHFYIjn3",
                            Phone = "26772684495",
                            Role = 1
                        },
                        new
                        {
                            Id = 2000034,
                            Address = "浙江省杭州市淳安县",
                            Birthday = new DateTime(1968, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentId = 4,
                            Gender = 1,
                            GlobalId = "715974750548300271",
                            Name = "祖琴芬",
                            Password = "Oa&r(cWm",
                            Phone = "45363277674",
                            Role = 1
                        },
                        new
                        {
                            Id = 2000035,
                            Address = "贵州省贵阳市修文县",
                            Birthday = new DateTime(1958, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentId = 5,
                            Gender = 0,
                            GlobalId = "215564788365062520",
                            Name = "乌暄莹",
                            Password = "w8QKacFdtfY3",
                            Phone = "16246675485",
                            Role = 2
                        },
                        new
                        {
                            Id = 2000036,
                            Address = "广东省韶关市翁源县",
                            Birthday = new DateTime(1982, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentId = 6,
                            Gender = 1,
                            GlobalId = "225994230321642894",
                            Name = "邹南春",
                            Password = "vSw1M0ICnE~8u",
                            Phone = "80527942982",
                            Role = 1
                        });
                });

            modelBuilder.Entity("Hospital.Models.Staff_TimeSlot", b =>
                {
                    b.Property<int>("StaffId")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("STAFF_ID");

                    b.Property<int>("Day")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("DAY");

                    b.Property<int>("Capacity")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("CAPACITY");

                    b.Property<int>("TimeSlotId")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("TIMESLOT_ID");

                    b.HasKey("StaffId", "Day");

                    b.HasIndex("TimeSlotId");

                    b.ToTable("STAFF_TIMESLOT");
                });

            modelBuilder.Entity("Hospital.Models.TimeSlot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID")
                        .UseHiLo("SEQ_TIMESLOT_ID");

                    b.Property<int>("EndTime")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("END_TIME");

                    b.Property<int>("StartTime")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("START_TIME");

                    b.HasKey("Id");

                    b.ToTable("TIME_SLOTS");
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

                    b.HasOne("Hospital.Models.TimeSlot", null)
                        .WithMany("Staff")
                        .HasForeignKey("TimeSlotId");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Hospital.Models.Staff_TimeSlot", b =>
                {
                    b.HasOne("Hospital.Models.Staff", "Staff")
                        .WithMany()
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hospital.Models.TimeSlot", "TimeSlot")
                        .WithMany()
                        .HasForeignKey("TimeSlotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Staff");

                    b.Navigation("TimeSlot");
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

            modelBuilder.Entity("Hospital.Models.TimeSlot", b =>
                {
                    b.Navigation("Staff");
                });
#pragma warning restore 612, 618
        }
    }
}
