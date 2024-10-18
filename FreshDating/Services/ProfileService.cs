using FreshDating.Data;
using FreshDating.Model;
using Microsoft.EntityFrameworkCore;

namespace FreshDating.Services
{
    public class ProfileService
    {
        private readonly AppDbContext _dbContext;
        private readonly ICityService _cityService;
        private readonly IGenderService _genderService;

        public ProfileModel? UserProfile { get; private set; }

        public ProfileService(AppDbContext dbContext, ICityService cityService, IGenderService genderService)
        {
            _dbContext = dbContext;
            _cityService = cityService;
            _genderService = genderService;
        }

        // Load current user's profile
        public async Task<ProfileModel?> LoadUserProfileAsync(int userId)
        {
            var profile = await _dbContext.Profiles
                .Include(p => p.City)
                .Include(p => p.Gender)
                .Include(p => p.User)  // Include User to access Username and BirthDate
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (profile == null) return null;

            UserProfile = new ProfileModel
            {
                ProfileId = profile.ProfileId,
                UserId = profile.UserId,
                AboutMe = profile.AboutMe,
                ProfilePicture = profile.ProfilePicture,
                SmokerStatus = profile.SmokerStatus,
                Height = profile.Height,
                Weight = profile.Weight,
                GenderId = profile.GenderId,
                Gender = profile.Gender,
                CityId = profile.CityId,
                City = profile.City,
                FirstName = profile.User.FirstName // Include FirstName
            };

            return UserProfile;
        }

        // Update an existing profile
        public async Task UpdateProfileAsync(ProfileModel profileModel)
        {
            var profile = await _dbContext.Profiles
                .Include(p => p.City)
                .Include(p => p.Gender)
                .Include(p => p.User) // Include User if Username is needed for profile
                .FirstOrDefaultAsync(p => p.ProfileId == profileModel.ProfileId);

            if (profile == null) return;

            // Update profile properties
            profile.AboutMe = profileModel.AboutMe;
            profile.Height = profileModel.Height;
            profile.Weight = profileModel.Weight;
            profile.SmokerStatus = profileModel.SmokerStatus;
            profile.ProfilePicture = profileModel.ProfilePicture;

            // Update City if applicable
            if (profileModel.CityId.HasValue)
            {
                var city = await _cityService.GetCityByIdAsync(profileModel.CityId.Value);
                profile.City = city ?? profile.City;
            }

            // Update Gender if applicable
            if (profileModel.GenderId.HasValue)
            {
                var gender = await _genderService.GetGenderByIdAsync(profileModel.GenderId.Value);
                profile.Gender = gender ?? profile.Gender;
            }

            await _dbContext.SaveChangesAsync();
        }

        // Fetch all cities
        public async Task<List<City>> GetAllCitiesAsync()
        {
            return await _dbContext.Cities.ToListAsync();
        }

        // Fetch all genders
        public async Task<List<Gender>> GetAllGendersAsync()
        {
            return await _dbContext.Genders.ToListAsync();
        }

        // Add a like from one profile to another
        public async Task<bool> LikeProfileAsync(int fromUserId, int targetProfileId)
        {
            var fromProfile = await _dbContext.Profiles.FirstOrDefaultAsync(p => p.UserId == fromUserId);
            var targetProfile = await _dbContext.Profiles.FirstOrDefaultAsync(p => p.ProfileId == targetProfileId);

            if (fromProfile == null || targetProfile == null)
            {
                return false;
            }

            // Check if the like already exists
            var existingLike = await _dbContext.ProfileLikes
                .FirstOrDefaultAsync(l => l.FromProfileId == fromProfile.ProfileId && l.ToProfileId == targetProfile.ProfileId);

            if (existingLike != null)
            {
                // Like already exists
                return false;
            }

            // Add a new like
            var like = new ProfileLike
            {
                FromProfileId = fromProfile.ProfileId,
                ToProfileId = targetProfile.ProfileId
            };

            _dbContext.ProfileLikes.Add(like);

            // Save changes
            await _dbContext.SaveChangesAsync();

            // Check if the target user has liked back (matching scenario)
            var mutualLike = await _dbContext.ProfileLikes
                .FirstOrDefaultAsync(l => l.FromProfileId == targetProfile.ProfileId && l.ToProfileId == fromProfile.ProfileId);

            if (mutualLike != null)
            {
                // Create a match record in a separate table if needed
                var match = new Match
                {
                    Profile1Id = fromProfile.ProfileId,
                    Profile2Id = targetProfile.ProfileId
                };

                _dbContext.Matches.Add(match);
                await _dbContext.SaveChangesAsync();

                return true; // They matched
            }

            return true; // Like successful, but no match yet
        }

        public async Task<List<Profile>> GetLikesReceivedAsync(int profileId)
        {
            return await _dbContext.ProfileLikes
                .Where(pl => pl.ToProfileId == profileId)
                .Select(pl => pl.FromProfile)
                .Include(p => p.City)
                .Include(p => p.Gender)
                .ToListAsync();
        }

    }
}
