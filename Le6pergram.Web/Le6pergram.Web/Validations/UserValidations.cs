namespace Le6pergram.Web.Validations
{
    using System.Text.RegularExpressions;
    using System.Web;

    public class UserValidations
    {
        private static Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        private static Regex passRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$");

        public static bool ValidateEmail(string email)
        {
            if (emailRegex.IsMatch(email))
                return true;
            else
                return false;
        }

        public static bool ValidateUsername(string username)
        {
            if (username.Length < 3 || username.Length > 50)
                return false;
            else
                return true;
        }

        public static bool ValidatePassword(string pass)
        {
            if (passRegex.IsMatch(pass))
                return true;
            else
                return false;
        }

        public static bool ValidateRepeatedPassword(string pass, string repeatedPass)
        {
            if (pass == repeatedPass)
                return true;
            else
                return false;
        }

        public static bool ValidateProfilePicture(HttpPostedFileBase profPic)
        {
            if (profPic == null)
                return false;
            else
                return true;
        }
    }
}