using FreshDating.Data;
using FreshDating.Model;
using Microsoft.EntityFrameworkCore;

namespace FreshDating.Services
{
    public class FilterService
    {
        private readonly AppDbContext _dbContext;

        public FilterService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Find partners based on search filters
        public async Task<List<ProfileModel>> FindPartnerAsync(
            int currentUserId,
            int? genderId = null,
            int? cityId = null,
            int? minAge = null,
            int? maxAge = null)
        {
            var profilesQuery = _dbContext.Profiles
                .Include(p => p.City)
                .Include(p => p.Gender)
                .Include(p => p.User)
                .AsQueryable();

            // Exclude the current user's profile
            profilesQuery = profilesQuery.Where(p => p.UserId != currentUserId);

            // Apply gender filter if specified
            if (genderId.HasValue)
            {
                profilesQuery = profilesQuery.Where(p => p.GenderId == genderId.Value);
            }

            // Apply city filter if specified
            if (cityId.HasValue)
            {
                profilesQuery = profilesQuery.Where(p => p.CityId == cityId.Value);
            }

            // Apply age filters if specified
            if (minAge.HasValue || maxAge.HasValue)
            {
                var now = DateTime.UtcNow;
                if (minAge.HasValue)
                {
                    var minDate = now.AddYears(-minAge.Value);
                    profilesQuery = profilesQuery.Where(p => p.User.BirthDate <= minDate);
                }

                if (maxAge.HasValue)
                {
                    var maxDate = now.AddYears(-maxAge.Value);
                    profilesQuery = profilesQuery.Where(p => p.User.BirthDate >= maxDate);
                }
            }

            var profiles = await profilesQuery.ToListAsync();

            // Return the profiles mapped to ProfileModel
            return profiles.Select(p => new ProfileModel
            {
                ProfileId = p.ProfileId,
                UserId = p.UserId,
                AboutMe = p.AboutMe,
                ProfilePicture = p.ProfilePicture,
                SmokerStatus = p.SmokerStatus,
                Height = p.Height,
                Weight = p.Weight,
                GenderId = p.GenderId,
                Gender = p.Gender,
                CityId = p.CityId,
                City = p.City,
                FirstName = p.User.FirstName

            }).ToList();
        }
    }
}
