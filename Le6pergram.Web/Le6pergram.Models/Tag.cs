namespace Le6pergram.Models
{
    using System.Collections.Generic;

    public class Tag : Entity
    {
        public Tag()
        {
            this.Pictures = new HashSet<Picture>();
        }        

        public string Name { get; set; }

        public virtual ICollection<Picture> Pictures { get; set; }
    }
}