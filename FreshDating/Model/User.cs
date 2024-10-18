namespace FreshDating.Model
{
    public class User : IUser
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime BirthDate { get; set; }
        public Profile Profile { get; set; }
        public ICollection<Match> Matches { get; set; }
    }
}
