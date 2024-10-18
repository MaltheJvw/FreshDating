namespace FreshDating.Model
{
    
    public class Profile : IProfile
    {
        public int ProfileId { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public string AboutMe { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = string.Empty;
        public SmokerStatus? SmokerStatus { get; set; }
        public int? Height { get; set; }
        public int? Weight { get; set; }
        public int? GenderId { get; set; }
        public Gender? Gender { get; set; }
        public int? CityId { get; set; }
        public City? City { get; set; }

        public ICollection<ProfileLike> LikesGiven { get; set; } = new List<ProfileLike>(); // Likes the profile has given
        public ICollection<ProfileLike> LikesReceived { get; set; } = new List<ProfileLike>(); // Likes the profile has received

        public ICollection<Match> MatchesAsProfile1 { get; set; } = new List<Match>();
        public ICollection<Match> MatchesAsProfile2 { get; set; } = new List<Match>();

    }
}
