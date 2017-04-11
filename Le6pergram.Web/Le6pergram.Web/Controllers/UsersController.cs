using Le6pergram.Web.Validations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

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
                if (Utilities.AuthenticationManager.IsUserExisting(username, password))
                {
                    Utilities.AuthenticationManager.SetCurrentUser(username, password);
                    user = Utilities.AuthenticationManager.GetAuthenticated();
                }
                return RedirectToAction("Index");
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
        public ActionResult Create([Bind(Include = "Id,Name,Username,Password,Email,RepeatPassword,Biography")] User user)
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

            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                Utilities.AuthenticationManager.SetCurrentUser(user.Username, user.Password);
                return RedirectToAction("Index");
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
        public ActionResult Edit([Bind(Include = "Id,Name,Username,Password,Email,RepeatPassword,Biography")] User user)
        {
            if (ModelState.IsValid)
            {
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
    }
}
