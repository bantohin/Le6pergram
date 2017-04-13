
namespace Le6pergram.Web.Models
{
    using System.Collections.Generic;

    public class Comment
    {        

        public Comment()
        {
            this.Likes = new HashSet<User>();
        }

        public int Id { get; set; }

        public string Content { get; set; }

        public virtual User User { get; set; }

        public virtual Picture Picture { get; set; }

        public virtual ICollection<User> Likes { get; set; }
    }
}