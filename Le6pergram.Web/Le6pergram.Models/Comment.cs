namespace Le6pergram.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class Comment
    {                
        public int Id { get; set; }

        public string Content { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public virtual Picture Picture { get; set; }        
    }
}