using System.Collections.Generic;
using System.Threading.Tasks;
using FreshDating.Model;

namespace FreshDating.Services
{
    public interface ICityService
    {
        Task<List<City>> GetAllCitiesAsync();
        Task<City> GetCityByIdAsync(int id);
        Task AddCityAsync(City city);
        Task UpdateCityAsync(City city, int id);
        Task DeleteCityAsync(int id);
    }
}
