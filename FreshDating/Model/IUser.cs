namespace FreshDating.Model
{
    public interface IUser
    {
        DateTime BirthDate { get; set; }
        DateTime CreateTime { get; set; }
        DateTime? DeleteDate { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        ICollection<Match> Matches { get; set; }
        string Password { get; set; }
        Profile Profile { get; set; }
        int UserId { get; set; }
        string Username { get; set; }
    }
}