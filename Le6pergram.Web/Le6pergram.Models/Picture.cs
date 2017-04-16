namespace Le6pergram.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Picture
    {
        public Picture()
        {
            this.Comments = new HashSet<Comment>();
            this.Likes = new HashSet<User>();
            this.Tags = new HashSet<Tag>();
            this.UploadDate = DateTime.Now;
        }

        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public string Description { get; set; }

        public DateTime UploadDate { get; set; }

        public byte[] Content { get; set; }
        
        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<User> Likes { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}