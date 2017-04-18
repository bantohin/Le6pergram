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
            if (senderId != receiverId)
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

        public static void RemoveLikeNotification(int senderId, int pictureId)
        {
            db.Notifications.Remove(db.Notifications.Where(n => n.SenderId == senderId && n.PictureId == pictureId).First());
            db.SaveChanges();
        }

        public static void AddFollowNotification(int senderId, int receiverId)
        {
            var notification = new Notification()
            {
                ReceiverId = receiverId,
                SenderId = senderId,
                Type = (NotificationType)int.Parse("0")                
            };

            db.Notifications.Add(notification);
            db.SaveChanges();
        }

        public static void RemoveFollowNotification(int senderId, int receiverId)
        {
            db.Notifications.Remove(db.Notifications.Where(n => n.Type == 0 && n.ReceiverId == receiverId && n.SenderId == senderId).First());
            db.SaveChanges();
        }

        public static void AddCommentNotification(int picId, int senderId)
        {
            int receiverId = db.Pictures.Find(picId).UserId;
            if (receiverId != senderId)
            {
                var notification = new Notification()
                {
                    ReceiverId = receiverId,
                    SenderId = senderId,
                    PictureId = picId,
                    Type = (NotificationType)int.Parse("2")
                };

                db.Notifications.Add(notification);
                db.SaveChanges();
            }
        }

        public static void RemoveCommentNotification(Comment comment, int pictureId)
        {
            var receiverId = db.Pictures.Find(pictureId).UserId;
            var senderId = comment.UserId;
                db.Notifications.Remove(db.Notifications.Where(n => n.Type.ToString() == "Comment" && n.SenderId == senderId && n.ReceiverId == receiverId).FirstOrDefault());
                db.SaveChanges();
        }
    }
}