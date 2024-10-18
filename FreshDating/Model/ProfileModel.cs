namespace FreshDating.Model
{
    public class ProfileModel
    {
        public int ProfileId { get; set; }
        public int UserId { get; set; } // Link to the user

        public string FirstName { get; set; } // Add FirstName property
        public string? AboutMe { get; set; }
        public string ProfilePicture { get; set; } = string.Empty; 
        public SmokerStatus? SmokerStatus { get; set; }
        public int? Height { get; set; }
        public int? Weight { get; set; }

        public Gender Gender { get; set; }
        public int? GenderId { get; set; }

        public City City { get; set; }
        public int? CityId { get; set; }

        private int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;
            return age;
        }

    }
}
