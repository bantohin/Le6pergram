namespace Le6pergram.Models.ViewModels
{
    using System.Collections.Generic;

    public class SearchViewModel 
    {
        public SearchViewModel()
        {
            Tags = new HashSet<Tag>();
            Users = new HashSet<User>();
        }

        public ICollection<Tag> Tags { get; set; }

        public ICollection<User> Users { get; set; }

        public bool isAnythingFound { get; set; }
    }   
}
