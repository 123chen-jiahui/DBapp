using Hospital.Database;
using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class PatientRepository : IPatientRepository
    {
        // 注入数据库服务依赖
        private readonly AppDbContext _context;
        public PatientRepository(AppDbContext context)
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
        public void AddPatient(Patient patient)
        {
            _context.Patients.Add(patient);
        }

        public Patient GetPatientByPatientId(int patientId)
        {
            return _context.Patients.Where(p => p.Id == patientId).FirstOrDefault(); // 一定要加FirstOrDefault来执行sql语句
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
