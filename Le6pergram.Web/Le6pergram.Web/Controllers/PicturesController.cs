namespace Le6pergram.Web
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using Utilities;
    using Models;
    using Models.ViewModels;
    using Controllers;

    public class PicturesController : Controller
    {
        private Le6pergramDatabase db = new Le6pergramDatabase();

        // GET: Pictures
        public ActionResult Index()
        {
            var pictures = db.Pictures.Include(p => p.User);
            return View(pictures.ToList());
        }

        // GET: Pictures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Picture picture = db.Pictures.Find(id);
            if (picture == null)
            {
                return HttpNotFound();
            }
            PictureDetailsViewModel pictureToView = new PictureDetailsViewModel()
            {
                Id = picture.Id,
                Content = picture.Content,
                Description = picture.Description,
                Tags = picture.Tags,
                UserId = picture.UserId,
                Likes = picture.Likes,
                User = picture.User,
                Comments = picture.Comments
            };

            var authent = AuthManager.GetAuthenticated();
            ViewBag.IsLogged = authent != null;
            ViewBag.CurrentUser = authent;
            ViewBag.HasComments = pictureToView.Comments.ToList().Count > 0;
            ViewBag.HasLikes = pictureToView.Likes.ToList().Count > 0;
            ViewBag.CurrentUserLiked = pictureToView.Likes.Any(l => l.Id == authent.Id);            
            return View(pictureToView);
        }

        // GET: Pictures/Create
        public ActionResult Create()
        {
            var authentication = AuthManager.GetAuthenticated();
            if(authentication == null)
            {
                return RedirectToAction("Login", "Users");
            }

            if (ViewBag.ShowError == null)
                ViewBag.ShowError = false;

            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: Pictures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,Description,Content")] CreatePictureViewModel picture, string tags, HttpPostedFileBase ContentFile)
        {
            if (ContentFile == null)
            {
                ViewBag.Error = "You have not chosen a picture.";
                ViewBag.ShowError = true;
                return View();
            }

            if (ModelState.IsValid)
            {
                var tagsList = tags.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                var currentUserId = AuthManager.GetAuthenticated().Id;
                var description = picture.Description;
                var contentFile = ContentFile;

                var content = PictureUtilities.PictureToByteArray(contentFile);
                var pictureEntity = new Picture()
                {
                    Content = content,
                    Description = description,
                    UserId = currentUserId
                };
                pictureEntity = TagsController.Create(tagsList, pictureEntity, db);
                db.Pictures.Add(pictureEntity);
                db.SaveChanges();
                return RedirectToAction("Details/" + currentUserId, "Users");
            }
            return View(picture);
        }

        // GET: Pictures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Picture picture = db.Pictures.Find(id);
            if (picture == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", picture.UserId);
            return View(picture);
        }

        // POST: Pictures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Description,Content")] Picture picture)
        {
            if (ModelState.IsValid)
            {
                db.Entry(picture).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", picture.UserId);
            return View(picture);
        }

        // GET: Pictures/Delete/5
        public ActionResult Delete(int? id)
        {            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Picture picture = db.Pictures.Find(id);

            var authentication = AuthManager.GetAuthenticated();
            if (authentication == null || authentication.Id != picture.UserId)
            {
                return RedirectToAction("Details/" + picture.Id, "Pictures");
            }

                if (picture == null)
            {
                return HttpNotFound();
            }
            return View(picture);
        }

        // POST: Pictures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Picture picture = db.Pictures.Find(id);
            db.Pictures.Remove(picture);
            db.SaveChanges();
            return RedirectToAction("Details/" + AuthManager.GetAuthenticated().Id, "Users");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [ActionName("Like")]
        public ActionResult LikePicture(int id, int loggedId)
        {
            var pic = db.Pictures.Find(id);
            var user = db.Users.Find(loggedId);
            pic.Likes.Add(user);
            db.SaveChanges();

            NotificationsController.AddLikeNotification(loggedId, id, pic.UserId);
            return RedirectToAction($"Details/{id}");
        }

        [ActionName("Unlike")]
        public ActionResult UnlikePicture(int id, int loggedId)
        {
            var pic = db.Pictures.Find(id);
            var user = db.Users.Find(loggedId);
            pic.Likes.Remove(user);
            db.SaveChanges();

            NotificationsController.RemoveLikeNotification(loggedId, id);
            return RedirectToAction($"Details/{id}");
        }
    }
}
