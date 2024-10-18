using FreshDating.Data;
using Microsoft.EntityFrameworkCore;
using FreshDating.Model;

namespace FreshDating.Services
{
    public class UserService
    {
        private readonly AppDbContext _dbContext;

        // Static property to simulate login state
        public static bool IsLoggedIn { get; private set; } = false;

        // Event to notify when the login state changes
        public event Action OnLoginStateChanged;
        public UserService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(UserModel? User, int? ProfileId)> LoginAsync(string username, string password)
        {
            var user = await _dbContext.Users
                .Include(u => u.Profile)
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

            if (user == null)
            {
                // User not found or password incorrect
                return (null, null);
            }

            IsLoggedIn = true; // Set login state
            OnLoginStateChanged?.Invoke();

            // Map User entity to UserModel for use in the app
            var userModel = new UserModel
            {
                UserId = user.UserId,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                BirthDate = user.BirthDate,
                Profile = user.Profile,
                Matches = user.Matches
            };

            

            return (userModel, user.Profile?.ProfileId);
        }

        public async Task RegisterAsync(string username, string password, string firstName, string lastName, string email, DateTime birthDate)
        {
            // Check if the username or email already exists
            var existingUser = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Username == username || u.Email == email);

            if (existingUser != null)
            {
                throw new InvalidOperationException("Username or email already exists.");
            }

            // Create a new user
            var newUser = new User
            {
                Username = username,
                Password = password, 
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                BirthDate = birthDate,
                Profile = new Profile() 
            };

            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync();
        }

        public async Task LogoutAsync()
        {
            IsLoggedIn = false;
            OnLoginStateChanged?.Invoke(); // Notify about login state change
        }
    }
}
