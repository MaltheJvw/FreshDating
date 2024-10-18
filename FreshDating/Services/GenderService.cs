using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FreshDating.Data;
using FreshDating.Model;

namespace FreshDating.Services
{
    public class GenderService : IGenderService
    {
        private readonly AppDbContext _context;

        public GenderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Gender>> GetAllGendersAsync()
        {
            return await _context.Genders
                .Where(g => g.GenderName == "Male" || g.GenderName == "Female")
                .ToListAsync();
        }

        public async Task<Gender> GetGenderByIdAsync(int id)
        {
            return await _context.Genders.FindAsync(id);
        }

        public async Task AddGenderAsync(Gender gender)
        {
            _context.Genders.Add(gender);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGenderAsync(Gender gender, int id)
        {
            var existingGender = await _context.Genders.FindAsync(id);
            if (existingGender != null)
            {
                existingGender.GenderName = gender.GenderName;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteGenderAsync(int id)
        {
            var gender = await _context.Genders.FindAsync(id);
            if (gender != null)
            {
                _context.Genders.Remove(gender);
                await _context.SaveChangesAsync();
            }
        }
    }
}