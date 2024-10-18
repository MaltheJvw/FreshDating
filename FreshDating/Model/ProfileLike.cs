namespace FreshDating.Model
{
    public class ProfileLike
    {
        public int FromProfileId { get; set; }
        public Profile FromProfile { get; set; }

        public int ToProfileId { get; set; }
        public Profile ToProfile { get; set; }
    }
}
