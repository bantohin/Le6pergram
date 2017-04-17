using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Le6pergram.Models
{

    public enum NotificationType
    {
        Follow, Like
    }

    public class Notification
    {
        public int Id { get; set; }

        public int SenderId { get; set; }

        public virtual User Sender { get; set; }

        public int ReceiverId { get; set; }

        public virtual User Receiver { get; set; }

        public int? PictureId { get; set; }

        public virtual Picture Picture { get; set; }

        public NotificationType Type { get; set; }
    }
}
