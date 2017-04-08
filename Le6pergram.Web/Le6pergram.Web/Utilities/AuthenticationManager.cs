using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Le6pergram.Web.Utilities
{
    public class AuthenticationManager
    {
        static User currentUser;

        static bool IsUserExisting(string username)
        {
            using (Le6pergramDatabase context = new Le6pergramDatabase())
            {
                return context.Users.Any(u => u.Name == username);
            }
        }

        static void SetCurrentUser(string username)
        {
            if (IsUserExisting(username))
            {
                currentUser = GetLoggedUser(username);
            }
            else
            {
                throw new InvalidOperationException("u suk");
            }
        }

        static User GetAuthenticated()
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

        static User GetLoggedUser(string username)
        {
            using (Le6pergramDatabase context = new Le6pergramDatabase())
            {
                return context.Users.Where(u => u.Username == username).FirstOrDefault();
            }
        }
    }
}