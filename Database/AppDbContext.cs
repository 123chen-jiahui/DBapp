using Hospital.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Hospital.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {


        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Ward> Wards { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<MedicalEquipment> MedicalEquipments { get; set; }
        public DbSet<Medicine> Medicine { get; set; }
        public DbSet<PurchaseList> PurchaseLists { get; set; }
        public DbSet<PurchaseListItem> PurchaseListItems { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<LineItem> LineItems { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence("SEQ_PATIENT_ID")
                .StartsAt(1000000)
                .IncrementsBy(1);
            // modelBuilder.HasDefaultSchema("C##TEST");
            modelBuilder.HasSequence("SEQ_STAFF_ID")
                .StartsAt(2000000)
                .IncrementsBy(1);
            modelBuilder.HasSequence("SEQ_LINEITEM_ID")
                .StartsAt(1)
                .IncrementsBy(1);
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.Property(o => o.Id)
                .ValueGeneratedOnAdd()
                .UseHiLo("SEQ_PATIENT_ID");
            });
            modelBuilder.Entity<Staff>(entity =>
            {
                entity.Property(o => o.Id)
                .ValueGeneratedOnAdd()
                .UseHiLo("SEQ_STAFF_ID");
            });
            modelBuilder.Entity<LineItem>(entity =>
            {
                entity.Property(o => o.Id)
                .ValueGeneratedOnAdd()
                .UseHiLo("SEQ_LINEITEM_ID");
            });
            // 添加科室数据
            // 读取json数据
            var departmentJsonData = File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"/Database/DepartmentMockData.json"); // 字符串前加@表示这是一个C#的string，不过我们还要获得当前项目的文件夹地址，需要用到C#的反射机制
            // 由json对象转换为数据类型，需要Newtonsoft.Json包来处理
            IList<Department> departments = JsonConvert.DeserializeObject<IList<Department>>(departmentJsonData);
            modelBuilder.Entity<Department>().HasData(departments);

            // 添加药品数据
            var medicineJsonData = File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"/Database/MedicineMockData.json");
            IList<Medicine> medicine = JsonConvert.DeserializeObject<IList<Medicine>>(medicineJsonData);
            modelBuilder.Entity<Medicine>().HasData(medicine);
            // modelBuilder.HasSequence("SEQ_PATIENT_ID", "C##TEST").IncrementsBy(1);
            base.OnModelCreating(modelBuilder);
        }
    }
}
