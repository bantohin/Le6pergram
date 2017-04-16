namespace Le6pergram.Web.Controllers
{
    using Le6pergram.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class SearchController : Controller
    {
        private Le6pergramDatabase db = new Le6pergramDatabase();

        // GET: Search
        [HttpPost]
        public ActionResult Index()
        {
            string searchedWord;
            searchedWord = Request.Form["searchEngine"];
            if (searchedWord == null || searchedWord == "")
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
