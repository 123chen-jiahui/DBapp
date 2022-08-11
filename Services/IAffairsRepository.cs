﻿using Hospital.Models;
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

        Task<IEnumerable<Staff_TimeSlot>> GetScheduleAsync(int staffId);
        Task<Staff_TimeSlot> GetScheduleOfOneDay(int staffId, int day);
        void AddSchedule(Staff_TimeSlot staff_TimeSlot);
        Task<bool> SaveAsync();
    }
}