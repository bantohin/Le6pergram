namespace Le6pergram.Web.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Picture
    {
        public Picture()
        {
            this.Comments = new HashSet<Comment>();
            this.Likes = new HashSet<User>();
            this.Tags = new HashSet<Tag>();
        }

        public int Id { get; set; }

        public int AuthorId { get; set; }

        [Required]
        public virtual User Author { get; set; }

        public string Description { get; set; }

        [Required]
        public byte[] Content { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<User> Likes { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}