using Le6pergram.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Le6pergram.Web.Controllers
{
    public class SearchController : Controller
    {
        private Le6pergramDatabase db = new Le6pergramDatabase();

        // GET: Search
        [HttpPost]
        public ActionResult Index()
        {
            string searchedWord;
            searchedWord = Request.Form["searchEngine"];
            if(searchedWord == null || searchedWord == "")
            {
                IEnumerable<User> users = new List<User>();
                return View(users);
            }
            else
            {                
                var users = db.Users.Where(u => u.Username.Contains(searchedWord)).ToList();
                return View(users);
            }
        }
    }
}
