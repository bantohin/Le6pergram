namespace Le6pergram.Web.Utilities
{
    using System.Linq;
    using Le6pergram.Models;

    public static class UserUtilities
    {
        public static bool IsUserExisting(string username, Le6pergramDatabase db)
        {
            return db.Users.Any(u => u.Username == username);
        }

        public static bool IsEmailTaken(string emailAddress, Le6pergramDatabase db)
        {
            return db.Users.Any(u => u.Email == emailAddress);
        }

        public static bool IsFollowing(User currentUser, User user)
        {
            return user.Followers.Any(f => f.Id == currentUser.Id);
        }
    }
}