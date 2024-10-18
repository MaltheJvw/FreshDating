using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FreshDating.Model
{
    public class Match
    {
        [Key]
        public int MatchId { get; set; }

        // First profile in the match (Profile1)
        public int Profile1Id { get; set; }

        // Second profile in the match (Profile2)
        public int Profile2Id { get; set; }

        // Navigation properties to link to Profile entities
        [ForeignKey("Profile1Id")]
        public Profile Profile1 { get; set; }

        [ForeignKey("Profile2Id")]
        public Profile Profile2 { get; set; }

    }
}
