namespace Le6pergram.Web.Models
{
    using Le6pergram.Web.Utilities;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Drawing;
    using System.Web;

    public class Picture
    {
        public Picture()
        {
            this.Comments = new HashSet<Comment>();
            this.Likes = new HashSet<User>();
            this.Tags = new HashSet<Tag>();
        }

        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public string Description { get; set; }

        public byte[] Content { get; set; }

        //public Image ContentFile { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<User> Likes { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}