namespace Le6pergram.Web.ViewModels
{
    using Le6pergram.Web.Models;
    using System.Collections.Generic;

    public class PictureDetailsViewModel
    {
        public PictureDetailsViewModel()
        {
            this.Tags = new HashSet<Tag>();
            this.Likes = new HashSet<User>();
            this.Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }

        public byte[] Content { get; set; }

        public string Description { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public ICollection<User> Likes { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public string NewComment { get; set; }
    }
}