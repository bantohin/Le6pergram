namespace Le6pergram.Web.Controllers
{
    using Le6pergram.Models;
    using Le6pergram.Web.Utilities;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        private Le6pergramDatabase db = new Le6pergramDatabase();

        public ActionResult Index()
        {
            User currentUser = AuthManager.GetAuthenticated();
            ViewBag.IsLogged = currentUser != null;
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
    }
}