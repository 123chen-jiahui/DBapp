using Hospital.Helper;
using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public interface IUserRepository
    {
        Task<bool> PatientExistsByGlobalIdAsync(string patientGlobalId); // 根据身份证号查找病人是否存在
        Task<bool> PatientExistsByPatientIdAsync(int patientId);
        Task<bool> StaffExistsByGlobalIdAsync(string staffGlobalId);
        // bool StaffExistsByStaffId(string staffId);
        void AddPatient(Patient patient);
        void AddStaff(Staff staff);
        void AddRegistration(Registration reg);
        Task<Patient> GetPatientByPatientIdAsync(int patientId); // 根据病人Id获取病人model
        Task<Staff> GetStaffByStaffIdAsync(int staffId);
        Task<PaginationList<Staff>> GetStaffsAsync(int departmentId, int pageNumber, int pageSize);
        Task<ShoppingCart> GetShoppingCartByPatientIdAsync(int patientId);
        void CreateShoppingCart(ShoppingCart shoppingCart);
        Task AddOrderAsync(Order order);
        Task<bool> SaveAsync();
        Task<PaginationList<Order>> GetOrdersByPatientIdAsync(int patientId, int pageNumber, int pageSize);
    }
}
