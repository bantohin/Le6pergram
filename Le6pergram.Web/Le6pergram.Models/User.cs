namespace Le6pergram.Models
{    
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    public class User : Entity
    {
        public User()
        {
            this.Pictures = new HashSet<Picture>();            
            this.Followers = new HashSet<User>();
            this.Following = new HashSet<User>();
            this.Comments = new HashSet<Comment>();
            this.LikedPictures = new HashSet<Picture>();
            this.Notifications = new HashSet<Notification>();
        }        

        public string Name { get; set; }
        
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

        public bool IsPrivate { get; set; }

        public virtual ICollection<User> Followers { get; set; }

        public virtual ICollection<User> Following { get; set; }        
        
        public byte[] RegisterProfilePicture { get; set; }
        
        public HttpPostedFileBase ProfilePictureFile { get; set; }

        public virtual ICollection<Picture> Pictures { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Picture> LikedPictures { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }
    }
}