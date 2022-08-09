using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public interface IAffairsRepository
    {
        Task<TimeSlot> GetTimeSlot(int timeSlotId);
        void AddTimeSlot(TimeSlot timeSlot);

        Task<bool> SaveAsync();
    }
}
