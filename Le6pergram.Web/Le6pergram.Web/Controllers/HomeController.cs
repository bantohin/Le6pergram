using Le6pergram.Web.Models;
using Le6pergram.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Le6pergram.Web.Controllers
{
    public class HomeController : Controller
    {
        private Le6pergramDatabase db = new Le6pergramDatabase();

        public ActionResult Index()
        {
            User currentUser = AuthManager.GetAuthenticated();
            if (currentUser != null)
            {
                List<Picture> list = new List<Picture>();                
                var picturesToShow = db.Users.Find(currentUser.Id).Following.Select(f => f.Pictures);
                foreach (var userPictures in picturesToShow)
                {
                    foreach (var picture in userPictures)
                    {
                        list.Add(picture);
                    }
                }
                list = list.OrderByDescending(p => p.UploadDate).ToList();
                return View(list);
            }
            else return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}