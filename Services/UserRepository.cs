using Hospital.Database;
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
        public bool PatientExistsByGlobalId(string patientGlobalId)
        {
            return _context.Patients.Any(p => p.GlobalId == patientGlobalId);
        }
        public bool PatientExistsByPatientId(int patientId)
        {
            return _context.Patients.Any(p => p.Id == patientId);
        }

        public bool StaffExistsByGlobalId(string staffGlobalId)
        {
            return _context.Staff.Any(s => s.GlobalId == staffGlobalId);
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

        public Patient GetPatientByPatientId(int patientId)
        {
            return _context.Patients.Where(p => p.Id == patientId).FirstOrDefault(); // 一定要加FirstOrDefault来执行sql语句
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public Staff GetStaffByStaffId(int staffId)
        {
            return _context.Staff.Where(s => s.Id == staffId).FirstOrDefault();
        }

        public IEnumerable<Staff> GetStaffs(int departmentId)
        {
            /*IQueryable<Staff> result = _context.Staff;
            result.Where(s => s.DepartmentId == departmentId);
            return result.ToList();*/
            return _context.Staff.Where(s => s.DepartmentId == departmentId).ToList();
        }

        public void AddRegistration(Registration reg)
        {
            _context.Registrations.Add(reg);
        }

        public async Task<ShoppingCart> GetShoppingCartByPatientId(int patientId)
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
    }
}
