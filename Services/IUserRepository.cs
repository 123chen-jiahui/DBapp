using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public interface IUserRepository
    {
        bool PatientExistsByGlobalId(string patientGlobalId); // 根据身份证号查找病人是否存在
        bool PatientExistsByPatientId(int patientId);
        bool StaffExistsByGlobalId(string staffGlobalId);
        bool StaffExistsByStaffId(string staffId);
        void AddPatient(Patient patient);
        void AddStaff(Staff staff);
        Patient GetPatientByPatientId(int patientId); // 根据病人Id获取病人model
        Staff GetStaff(string staffId);
        bool Save();
    }
}
