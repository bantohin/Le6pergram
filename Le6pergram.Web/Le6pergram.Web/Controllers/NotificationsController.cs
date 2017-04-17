using Le6pergram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Le6pergram.Web.Controllers
{
    public class NotificationsController : Controller
    {
        private static Le6pergramDatabase db = new Le6pergramDatabase();

        public static void AddLikeNotification(int senderId, int pictureId, int receiverId)
        {
            var notification = new Notification()
            {
                PictureId = pictureId,
                ReceiverId = receiverId,
                SenderId = senderId,
                Type = (NotificationType)int.Parse("1")
            };

            db.Notifications.Add(notification);
            db.SaveChanges();
        }
    }
}