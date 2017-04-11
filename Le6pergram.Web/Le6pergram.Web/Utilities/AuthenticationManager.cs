using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Le6pergram.Web.Utilities
{
    public class AuthenticationManager
    {
        static User currentUser;

        public static bool IsUserExisting(string username, string password)
        {
            using (Le6pergramDatabase context = new Le6pergramDatabase())
            {
                return context.Users.Any(u => u.Name == username && u.Password == password);
            }
        }

        public static void SetCurrentUser(string username, string password)
        {
            if (IsUserExisting(username, password))
            {
                currentUser = GetLoggedUser(username);
            }
            else
            {
                throw new InvalidOperationException("u suk");
            }
        }

        public static User GetAuthenticated()
        {
            if(currentUser != null)
            {
                return currentUser;
            }
            else
            {
                throw new InvalidOperationException("u suk");
            }
        }

        public static User GetLoggedUser(string username)
        {
            using (Le6pergramDatabase context = new Le6pergramDatabase())
            {
                return context.Users.Where(u => u.Username == username).FirstOrDefault();
            }
        }

        public static void LogoutUser()
        {
            currentUser = null;            
        }
    }
}