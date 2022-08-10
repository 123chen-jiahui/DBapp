using Hospital.Database;
using Hospital.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class AffairsRepository : IAffairsRepository
    {
        private readonly AppDbContext _context;

        public AffairsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TimeSlot> GetTimeSlotAsync(int timeSlotId)
        {
            return await _context.TimeSlots.Where(ts => ts.Id == timeSlotId).FirstOrDefaultAsync();
        }
        public void AddTimeSlot(TimeSlot timeSlot)
        {
            _context.TimeSlots.Add(timeSlot);
        }

        public async Task<IEnumerable<TimeSlot>> GetTimeSlotsAsync()
        {
            return await _context.TimeSlots.ToListAsync();
        }

        public async Task<bool> TimeSlotExistsAsync(TimeSlot timeSlot)
        {
            return await _context.TimeSlots.AnyAsync(ts => ts.StartTime == timeSlot.StartTime && ts.EndTime == timeSlot.EndTime);
        }

        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
