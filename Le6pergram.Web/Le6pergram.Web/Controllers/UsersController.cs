namespace Le6pergram.Web
{
    using Le6pergram.Models;
    using Le6pergram.Web.Controllers;
    using Le6pergram.Web.Utilities;
    using Le6pergram.Web.Validations;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.UI.WebControls;

    public class UsersController : Controller
    {
        private Le6pergramDatabase db = new Le6pergramDatabase();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Login
        public ActionResult Login()
        {
            var authentication = AuthManager.GetAuthenticated();
            if (authentication != null)
            {
                return RedirectToAction("Details/" + authentication.Id, "Users");
            }

            if (ViewBag.ShowError == null)
                ViewBag.ShowError = false;

            return View();
        }


        // POST: Users/Login
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            var user = new User();
            if (ModelState.IsValid)
            {
                if (AuthManager.IsUserExisting(username, password))
                {
                    AuthManager.SetCurrentUser(username, password);
                    user = AuthManager.GetAuthenticated();
                    return RedirectToAction("Details/" + AuthManager.GetLoggedUser(username).Id);
                }
                else
                {
                    ViewBag.ErrorMessage = "The username or the password is incorrect.";
                    ViewBag.ShowError = true;
                    return View();
                }
            }

            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            var authentication = AuthManager.GetAuthenticated();
            if (authentication != null)
            {
                return RedirectToAction("Details/" + authentication.Id, "Users");
            }

            if (ViewBag.ShowError == null)
                ViewBag.ShowError = false;

            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Username,Password,Email,RepeatPassword,Biography")] User user, HttpPostedFileBase profilePictureFile)
        {
            if (!UserValidations.ValidateEmail(user.Email))
            {
                ViewBag.Error = "Your email is invalid. Please enter a valid one.";
                ViewBag.ShowError = true;
                return View();
            }

            if (UserUtilities.IsEmailTaken(user.Email, db))
            {
                ViewBag.Error = "This email is already taken. Please register with another one.";
                ViewBag.ShowError = true;
                return View();
            }

            if (!UserValidations.ValidateUsername(user.Username))
            {
                ViewBag.Error = "Your username is invalid. It should be between 3 and 50 characters long.";
                ViewBag.ShowError = true;
                return View();
            }

            if (UserUtilities.IsUserExisting(user.Username, db))
            {
                ViewBag.Error = "This username is already taken. Please register with another one.";
                ViewBag.ShowError = true;
                return View();
            }

            if (!UserValidations.ValidatePassword(user.Password))
            {
                ViewBag.Error = "Your password is invalid. It should be at least 8 characters long, contain at least one small and one big letter and one digit.";
                ViewBag.ShowError = true;
                return View();
            }

            if (!UserValidations.ValidateRepeatedPassword(user.Password, user.RepeatPassword))
            {
                ViewBag.Error = "Your passwords do not match.";
                ViewBag.ShowError = true;
                return View();
            }

            if (!UserValidations.ValidateProfilePicture(profilePictureFile))
            {
                ViewBag.Error = "You have not chosen a profile picture.";
                ViewBag.ShowError = true;
                return View();
            }

            if (ModelState.IsValid)
            {
                user.RegisterProfilePicture = PictureUtilities.PictureToByteArray(profilePictureFile);
                db.Users.Add(user);
                db.SaveChanges();
                AuthManager.SetCurrentUser(user.Username, user.Password);
                return RedirectToAction("Details/" + AuthManager.GetAuthenticated().Id, "Users");
            }
            return View(user);
        }



        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (ViewBag.ShowError == null)
                ViewBag.ShowError = false;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            var authentication = AuthManager.GetAuthenticated();
            if (authentication == null || authentication.Id != user.Id)
            {
                return RedirectToAction("Details/" + user.Id, "Users");
            }
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Username,Password,Email,RepeatPassword,Biography")] User user, HttpPostedFileBase profilePictureFile)
        {
            if (!UserValidations.ValidateEmail(user.Email))
            {
                ViewBag.Error = "Your email is invalid. Please enter a valid one.";
                ViewBag.ShowError = true;
                return View();
                //TODO: Add notification
            }

            if (!UserValidations.ValidateUsername(user.Username))
            {
                ViewBag.Error = "Your username is invalid. It should be between 3 and 50 characters long.";
                ViewBag.ShowError = true;
                return View();
                //TODO: Add notification
            }

            if (!UserValidations.ValidatePassword(user.Password))
            {
                ViewBag.Error = "Your password is invalid. It should be at least 8 characters long, contain at least one small and one big letter and one digit.";
                ViewBag.ShowError = true;
                return View();
                //TODO: Add notification
            }

            if (!UserValidations.ValidateRepeatedPassword(user.Password, user.RepeatPassword))
            {
                ViewBag.Error = "Your passwords do not match.";
                ViewBag.ShowError = true;
                return View();
                //TODO: Add notification
            }

            if (!UserValidations.ValidateProfilePicture(profilePictureFile))
            {
                ViewBag.Error = "You have not chosen a profile picture.";
                ViewBag.ShowError = true;
                return View();
                //TODO: Add notification
            }

            if (ModelState.IsValid)
            {
                user.RegisterProfilePicture = PictureUtilities.PictureToByteArray(profilePictureFile);
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details/" + AuthManager.GetAuthenticated().Id, "Users");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [ActionName("Logout")]
        public ActionResult LogoutUser()
        {
            AuthManager.LogoutUser();
            return RedirectToAction("Login");
        }

        [ActionName("Follow")]
        public ActionResult Follow(int id, int loggedId)
        {
            User userToFollow = db.Users.Find(id);
            User userFollowing = db.Users.Find(loggedId);
            userToFollow.Followers.Add(userFollowing);
            db.SaveChanges();

            NotificationsController.AddFollowNotification(loggedId, id);
            return RedirectToAction($"Details/{id}");
        }

        [ActionName("Unfollow")]
        public ActionResult Unfollow(int id, int loggedId)
        {
            User userToUnfollow = db.Users.Find(id);
            User userUnfollowing = db.Users.Find(loggedId);
            userToUnfollow.Followers.Remove(userUnfollowing);
            db.SaveChanges();

            NotificationsController.RemoveFollowNotification(loggedId, id);
            return RedirectToAction($"Details/{id}");
        }
    }
}
