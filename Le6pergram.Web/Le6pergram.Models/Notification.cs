﻿namespace Le6pergram.Models
{
    using System;

    public enum NotificationType
    {
        Follow,
        Like,
        Comment,
        Request
    }

    public class Notification : Entity
    {
        public Notification()
        {
            Date = DateTime.Now;
        }        

        public int SenderId { get; set; }

        public virtual User Sender { get; set; }

        public int ReceiverId { get; set; }

        public virtual User Receiver { get; set; }

        public int? PictureId { get; set; }

        public virtual Picture Picture { get; set; }

        public NotificationType Type { get; set; }

        public DateTime Date { get; set; }
    }
}
