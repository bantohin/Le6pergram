namespace Le6pergram.Web
{    
    using System.Collections.Generic;

    public class User
    {
        public User()
        {
            //this.picturesCount = Pictures.Count;
            this.Followers = new HashSet<User>();
            this.Following = new HashSet<User>();
        }

        public int picturesCount;

        public int Id { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string RepeatPassword { get; set; }

        public string Biography { get; set; }

        public virtual ICollection<User> Followers { get; set; }

        public virtual ICollection<User> Following { get; set; }

        //ProfilePicture;

        //Pictures;
    }
}