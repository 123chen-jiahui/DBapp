using Hospital.Database;
using Hospital.Helper;
using Hospital.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class UserRepository : IUserRepository
    {
        // 注入数据库服务依赖
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> PatientExistsByGlobalIdAsync(string patientGlobalId)
        {
            return await _context.Patients.AnyAsync(p => p.GlobalId == patientGlobalId);
        }
        public async Task<bool> PatientExistsByPatientIdAsync(int patientId)
        {
            return await _context.Patients.AnyAsync(p => p.Id == patientId);
        }

        public async Task<bool> StaffExistsByGlobalIdAsync(string staffGlobalId)
        {
            return await _context.Staff.AnyAsync(s => s.GlobalId == staffGlobalId);
        }

        /*public bool StaffExistsByStaffId(string staffId)
        {
            return _context.Staff.Any(s => s.Id == staffId);
        }*/
        public void AddPatient(Patient patient)
        {
            _context.Patients.Add(patient);
        }

        public void AddStaff(Staff staff)
        {
            _context.Staff.Add(staff);
        }

        public async Task<Patient> GetPatientByPatientIdAsync(int patientId)
        {
            return await _context.Patients.Where(p => p.Id == patientId).FirstOrDefaultAsync(); // 一定要加FirstOrDefault来执行sql语句
        }

        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public async Task<Staff> GetStaffByStaffIdAsync(int staffId)
        {
            return await _context.Staff.Where(s => s.Id == staffId).FirstOrDefaultAsync();
        }

        public async Task<PaginationList<Staff>> GetStaffsAsync(int departmentId, int pageNumber, int pageSize)
        {
            /*IQueryable<Staff> result = _context.Staff;
            result.Where(s => s.DepartmentId == departmentId);
            return result.ToList();*/
            IQueryable<Staff> result = _context.Staff;

            /*// pagination
            // skip
            var skip = (pageNumber - 1) * pageSize;
            result = result.Skip(skip);
            // 以pageSize为标准显示一定量的数据
            result = result.Take(pageSize); 

            return await result.ToListAsync();*/
            return await PaginationList<Staff>.CreateAsync(pageNumber, pageSize, result);
            // return await _context.Staff.Where(s => s.DepartmentId == departmentId).ToListAsync();
        }

        public void AddRegistration(Registration reg)
        {
            _context.Registrations.Add(reg);
        }

        public async Task<ShoppingCart> GetShoppingCartByPatientIdAsync(int patientId)
        {
            return await _context.ShoppingCarts
                .Include(s => s.Patient)
                .Include(s => s.ShoppingCartItems).ThenInclude(li => li.Medicine)
                .Where(s => s.PatientId == patientId)
                .FirstOrDefaultAsync();
        }

        public void CreateShoppingCart(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Add(shoppingCart);
        }

        public async Task AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public async Task<PaginationList<Order>> GetOrdersByPatientIdAsync(int patientId, int pageNumber, int pageSize)
        {
            IQueryable<Order> result = _context.Orders;
            result = result.Where(o => o.PatientId == patientId);

            return await PaginationList<Order>.CreateAsync(pageNumber, pageSize, result);
            // return await _context.Orders.Where(o => o.PatientId == patientId).ToListAsync();
        }
    }
}
