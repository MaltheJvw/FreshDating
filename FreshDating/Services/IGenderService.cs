using System.Collections.Generic;
using System.Threading.Tasks;
using FreshDating.Model;

namespace FreshDating.Services
{
    public interface IGenderService
    {
        Task<List<Gender>> GetAllGendersAsync();
        Task<Gender> GetGenderByIdAsync(int id);
        Task AddGenderAsync(Gender gender);
        Task UpdateGenderAsync(Gender gender, int id);
        Task DeleteGenderAsync(int id);
    }
}
