using System.ComponentModel.DataAnnotations;

namespace FreshDating.Model
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime? DeleteDate { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime BirthDate { get; set; }
        public Profile Profile { get; set; } = new Profile();
        public ICollection<Match> Matches { get; set; } = new List<Match>();
    }
}
