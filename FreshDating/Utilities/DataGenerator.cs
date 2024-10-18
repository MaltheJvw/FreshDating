using Bogus;
using FreshDating.Data;
using FreshDating.Model;
using Microsoft.EntityFrameworkCore;


public static class DataGenerator
{
    public static async Task SeedUsersAndProfilesAsync(AppDbContext dbContext)
    {
        await dbContext.Database.EnsureCreatedAsync();

        // Ensure there are cities and genders in the database
        if (!dbContext.Cities.Any() || !dbContext.Genders.Any())
        {
            throw new InvalidOperationException("Cities and Genders must be seeded before creating Users and Profiles.");
        }

        if (dbContext.Users.Count() >= 10)
        {
            return; // Data already exists, skip seeding
        }

        var cities = await dbContext.Cities.ToListAsync();
        var genders = await dbContext.Genders.ToListAsync();

        var userFaker = new Faker<User>()
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.Email, (f, u) => GenerateValidEmail(u.FirstName, f))
            .RuleFor(u => u.Username, (f, u) => CapitalizeFirstLetter(u.FirstName)) // Capitalize Username
            .RuleFor(u => u.Password, "Qwerty1630")  // Fixed password
            .RuleFor(u => u.BirthDate, f => f.Date.Past(40, DateTime.Now.AddYears(-18)));  // Random birthdate

       

        var users = userFaker.Generate(10);  // Generate 10 random users

        
        var profiles = new List<Profile>();

        foreach (var user in users)
        {
            var profile = new Profile
            {
                UserId = user.UserId, // Link Profile with User
                SmokerStatus = (SmokerStatus)new Random().Next(Enum.GetValues(typeof(SmokerStatus)).Length),
                Height = new Random().Next(150, 200),
                Weight = new Random().Next(50, 100),
                Gender = new Faker().PickRandom(genders),  // Pick a random gender
                City = new Faker().PickRandom(cities),
            };

            user.Profile = profile;  // Associate Profile with User

            profiles.Add(profile);  // Add to profile list
        }

        dbContext.Users.AddRange(users);
        dbContext.Profiles.AddRange(profiles);
        await dbContext.SaveChangesAsync();
    }

    private static string CapitalizeFirstLetter(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }

        return char.ToUpper(text[0]) + text.Substring(1).ToLower();
    }
    private static string GenerateValidEmail(string firstName, Faker faker)
    {
        // Basic email constraints
        var localPart = $"{firstName.ToLower()}";
        var domain = faker.Internet.DomainName();
        var email = $"{localPart}@{domain}";

        // Ensure email follows a basic format
        return email;
    }

}