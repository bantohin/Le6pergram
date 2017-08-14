namespace Le6pergram.Models.ViewModels
{
    using Models;
    using System.Collections.Generic;

    public class PictureDetailsViewModel
    {
        public PictureDetailsViewModel()
        {
            Tags = new HashSet<Tag>();
            Likes = new HashSet<User>();
            Comments = new HashSet<Comment>();
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