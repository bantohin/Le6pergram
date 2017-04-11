namespace Le6pergram.Web
{
    using Le6pergram.Web.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User
    {
        public User()
        {
            this.Pictures = new HashSet<Picture>();
            this.PicturesCount = Pictures.Count;
            this.Followers = new HashSet<User>();
            this.Following = new HashSet<User>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        //unique
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        [Index("IX_User_Username",IsUnique = true)]
        public string Username { get; set; }

        public string Password { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        [Index("IX_User_Email", IsUnique = true)]
        public string Email { get; set; }

        public string RepeatPassword { get; set; }

        public string Biography { get; set; }

        public int PicturesCount { get; set; }

        public virtual ICollection<User> Followers { get; set; }

        public virtual ICollection<User> Following { get; set; }

        public virtual Picture ProfilePicture { get; set; }

        public virtual ICollection<Picture> Pictures { get; set; }
    }
}