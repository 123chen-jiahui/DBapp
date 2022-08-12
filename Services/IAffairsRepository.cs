using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public interface IAffairsRepository
    {
        Task<TimeSlot> GetTimeSlotAsync(int timeSlotId);
        Task<IEnumerable<TimeSlot>> GetTimeSlotsAsync();
        void AddTimeSlot(TimeSlot timeSlot);
        Task<bool> TimeSlotExistsAsync(TimeSlot timeSlot);
        Task<bool> ScheduleExistsAsync(int staffId);

        Task<IEnumerable<Schedule>> GetScheduleAsync(int staffId);
        Task<Schedule> GetScheduleOfOneDay(int staffId, int day);
        void AddSchedule(Schedule staff_TimeSlot);

        // 挂号
        Task AddRegistrationAsync(Registration registration);
        Task<bool> SaveAsync();
    }
}
