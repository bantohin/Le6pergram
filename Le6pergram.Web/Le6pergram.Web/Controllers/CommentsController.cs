using Le6pergram.Web.Models;
using Le6pergram.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Le6pergram.Web.Controllers
{
    public class CommentsController : Controller
    {
        private static Le6pergramDatabase db = new Le6pergramDatabase();

        public static void InsertComment(string text, int pictureId)
        {
            int userId = AuthManager.GetAuthenticated().Id;

            Comment currentComment = new Comment()
            {
                UserId = userId,
                Content = text,
                Picture = db.Pictures.Find(pictureId)
            };

            db.Comments.Add(currentComment);
            db.SaveChanges();
        }
    }
}