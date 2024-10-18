namespace FreshDating.Model
{
    public interface IProfile
    {
        int ProfileId { get; set; }
        int UserId { get; set; }
        User? User { get; set; }
        string AboutMe { get; set; }
        string ProfilePicture { get; set; }
        SmokerStatus? SmokerStatus { get; set; }
        int? Height { get; set; }
        int? Weight { get; set; }
        int? GenderId { get; set; }
        Gender? Gender { get; set; }
        int? CityId { get; set; }
        City? City { get; set; }
    }
}