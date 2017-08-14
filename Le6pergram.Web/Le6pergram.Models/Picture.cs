namespace Le6pergram.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Picture : Entity
    {
        public Picture()
        {
            Comments = new HashSet<Comment>();
            Likes = new HashSet<User>();
            Tags = new HashSet<Tag>();
            UploadDate = DateTime.Now;
            Notifications = new HashSet<Notification>();
        }        

        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public string Description { get; set; }

        public DateTime UploadDate { get; set; }

        public byte[] Content { get; set; }
        
        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<User> Likes { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }
    }
}