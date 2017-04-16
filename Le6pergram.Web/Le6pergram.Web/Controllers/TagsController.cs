namespace Le6pergram.Web
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Le6pergram.Web.Utilities;
    using Le6pergram.Models;

    public class TagsController : Controller
    {
        private Le6pergramDatabase db = new Le6pergramDatabase();

        // GET: Tags
        public ActionResult Index()
        {
            return View(db.Tags.ToList());
        }

        // GET: Tags/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tag tag = db.Tags.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        // GET: Tags/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public static Picture Create(string[] arrayOfTagStrings, Picture currentPicture, Le6pergramDatabase context)
        {
            List<Tag> tagsList = new List<Tag>();
            foreach (var tag in arrayOfTagStrings)
            {
                if (!TagUtilities.IsTagExisting(tag, context))
                {
                    var currentTag = new Tag()
                    {
                        Name = tag
                    };

                    tagsList.Add(currentTag);
                    context.Tags.Add(currentTag);
                }
                else
                {
                    var currentTag = context.Tags.Where(t => t.Name == tag).FirstOrDefault();
                    tagsList.Add(currentTag);
                }
            }
            context.SaveChanges();

            foreach (Tag tag in tagsList)
            {
                currentPicture.Tags.Add(tag);
            }
            return currentPicture;
        }        

        // GET: Tags/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tag tag = db.Tags.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        // POST: Tags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tag);
        }

        // GET: Tags/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tag tag = db.Tags.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        // POST: Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tag tag = db.Tags.Find(id);
            db.Tags.Remove(tag);
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

        [ActionName("DisplayTag")]
        public ActionResult DisplayTag(int id)
        {
            Tag tag = db.Tags.Find(id);
            return RedirectToAction($"Details/{id}", "Tags");
        }
    }
}
