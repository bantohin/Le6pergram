using Le6pergram.Web.Models;
using Le6pergram.Web.Utilities;
using Le6pergram.Web.Validations;
using Le6pergram.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Le6pergram.Web
{
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
                    return RedirectToAction("Details/" +  AuthManager.GetLoggedUser(username).Id);
                }
                else
                {
                    //TODO: clear form
                    //TODO: show notification
                }
            }

            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
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
                return RedirectToAction("Create");
                //TODO: Add notification
            }

            if (!UserValidations.ValidateUsername(user.Username))
            {
                return RedirectToAction("Create");
                //TODO: Add notification
            }

            if (!UserValidations.ValidatePassword(user.Password))
            {
                return RedirectToAction("Create");
                //TODO: Add notification
            }

            if (!UserValidations.ValidateRepeatedPassword(user.Password, user.RepeatPassword))
            {
                return RedirectToAction("Create");
                //TODO: Add notification
            }

            if (!UserValidations.ValidateProfilePicture(profilePictureFile))
            {
                return RedirectToAction("Create");
                //TODO: Add notification
            }

            if (ModelState.IsValid)
            {
                user.RegisterProfilePicture = PictureToByteArray(profilePictureFile);
                db.Users.Add(user);
                db.SaveChanges();
                Utilities.AuthManager.SetCurrentUser(user.Username, user.Password);
                RedirectToAction("Index");
            }

            return View(user);
        }



        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
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
        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Username,Password,Email,RepeatPassword,Biography")] User user, HttpPostedFileBase profilePictureFile)
        {
            if (!UserValidations.ValidateEmail(user.Email))
            {
                return RedirectToAction("Edit");
                //TODO: Add notification
            }

            if (!UserValidations.ValidateUsername(user.Username))
            {
                return RedirectToAction("Edit");
                //TODO: Add notification
            }

            if (!UserValidations.ValidatePassword(user.Password))
            {
                return RedirectToAction("Edit");
                //TODO: Add notification
            }

            if (!UserValidations.ValidateRepeatedPassword(user.Password, user.RepeatPassword))
            {
                return RedirectToAction("Edit");
                //TODO: Add notification
            }

            if (!UserValidations.ValidateProfilePicture(profilePictureFile))
            {
                return RedirectToAction("Edit");
                //TODO: Add notification
            }

            if (ModelState.IsValid)
            {
                user.RegisterProfilePicture = PictureToByteArray(profilePictureFile);
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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
            return RedirectToAction($"Details/{id}");
        }

        [ActionName("Unfollow")]
        public ActionResult Unfollow(int id, int loggedId)
        {
            User userToUnfollow = db.Users.Find(id);
            User userUnfollowing = db.Users.Find(loggedId);
            userToUnfollow.Followers.Remove(userUnfollowing);
            db.SaveChanges();
            return RedirectToAction($"Details/{id}");
        }

        private byte[] PictureToByteArray(HttpPostedFileBase contentFile)
        {
            MemoryStream stream = new MemoryStream();            
            contentFile.InputStream.CopyTo(stream);
            byte[] data = stream.ToArray();

            return data;
        }
    }
}
