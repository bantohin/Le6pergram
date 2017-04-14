using Le6pergram.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Le6pergram.Web.Utilities
{
    public class AuthManager
    {
        static User currentUser;
        private static Le6pergramDatabase context = new Le6pergramDatabase();

        public static bool IsUserExisting(string username, string password)
        {            
                return context.Users.Any(u => u.Name == username && u.Password == password);            
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
            return currentUser;
        }

        public static User GetLoggedUser(string username)
        {                        
                return context.Users.Where(u => u.Username == username).FirstOrDefault();            
        }
        
        public static void LogoutUser()
        {            
            currentUser = null;            
        }
    }
}