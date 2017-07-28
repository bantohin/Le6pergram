namespace Le6pergram.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Picture : Entity
    {
        public Picture()
        {
            this.Comments = new HashSet<Comment>();
            this.Likes = new HashSet<User>();
            this.Tags = new HashSet<Tag>();
            this.UploadDate = DateTime.Now;
            this.Notifications = new HashSet<Notification>();
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