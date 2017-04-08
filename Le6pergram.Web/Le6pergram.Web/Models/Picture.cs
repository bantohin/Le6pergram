namespace Le6pergram.Web.Models
{
    using System.Collections.Generic;

    public class Picture
    {
        public Picture()
        {
            this.Comments = new HashSet<Comment>();
            this.Likes = new HashSet<User>();
            this.Tags = new HashSet<Tag>();
        }

        //Key
        public int Id { get; set; }

        public int AuthorId { get; set; }

        //Required
        public virtual User Author { get; set; }

        //Max len 50
        public string Description { get; set; }

        //Binary representation;

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<User> Likes { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}