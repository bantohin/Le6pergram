namespace Le6pergram.Web.Repositories
{
    using Le6pergram.Models;

    public class UserRepository : BaseRepository<User>
    {
       
        public void Follow(int userToFollow, int userFollowing)
        {
            using (Le6pergramDatabase context = new Le6pergramDatabase())
            {
                User userfollow = base.GetById(userToFollow);
                User userfollowing = base.GetById(userFollowing);
                userfollow.Followers.Add(userfollowing);
                context.SaveChanges();
            }               
        }

        public void Unfollow(int userToUnfollow, int userUnfollowing)
        {
            using (Le6pergramDatabase context = new Le6pergramDatabase())
            {
                User userunfollow = base.GetById(userToUnfollow);
                User userunfollowing = base.GetById(userUnfollowing);
                userunfollow.Followers.Remove(userunfollowing);
                context.SaveChanges();
            }
        }
    }
}