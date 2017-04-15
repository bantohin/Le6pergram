using System.Collections.Generic;

namespace Le6pergram.Web.Models
{
    public class Tag
    {
        public Tag()
        {
            this.Pictures = new HashSet<Picture>();
            //this.picturesCount = Pictures.Count;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Picture> Pictures { get; set; }
        
        //public int picturesCount { get; set; }
    }
}