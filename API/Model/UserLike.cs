
namespace API.Model
{
    public class UserLike
    {
        public AppUser SourceUser {get; set;}
        public int SourseUserId {get; set;}
        public AppUser LikedUser { get; set; }
        public int LikedUserId { get; set; }

    }
}