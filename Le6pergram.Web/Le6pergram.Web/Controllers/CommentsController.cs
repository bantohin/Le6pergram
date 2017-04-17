namespace Le6pergram.Web.Controllers
{
    using Le6pergram.Models;
    using Le6pergram.Web.Utilities;
    using System.Web.Mvc;

    public class CommentsController : Controller
    {
        private static Le6pergramDatabase db = new Le6pergramDatabase();       

        [ActionName("DeleteComment")]
        public ActionResult DeleteComment()
        {
            int deleteCommentId;
            deleteCommentId = int.Parse(Request.Form["currentCommentID"]);
            int pictureId = int.Parse(Request.Form["CurrentPictureID"]);
            Comment commentToDelete = db.Comments.Find(deleteCommentId);
            db.Comments.Remove(commentToDelete);
            db.SaveChanges();

            NotificationsController.RemoveCommentNotification(commentToDelete, pictureId);
            return RedirectToAction("Details/" + pictureId, "Pictures");
        }

        [ActionName("AddComment")]
        public ActionResult AddComment()
        {
            string newComment;
            newComment = Request.Form["NewComment"];
            int pictureId = int.Parse(Request.Form["currentID"]);
            if (newComment == "")
            {
                return RedirectToAction("Details/" + pictureId, "Pictures");
            }
            int userId = AuthManager.GetAuthenticated().Id;
            Comment comment = new Comment()
            {
                UserId = userId,
                Content = newComment,
                Picture = db.Pictures.Find(pictureId)
            };

            db.Comments.Add(comment);
            db.SaveChanges();

            NotificationsController.AddCommentNotification(pictureId, userId);
            return RedirectToAction("Details/" + pictureId,"Pictures");
        }
    }
}