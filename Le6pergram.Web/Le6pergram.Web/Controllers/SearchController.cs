namespace Le6pergram.Web.Controllers
{
    using Le6pergram.Models;
    using Le6pergram.Models.ViewModels;
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
                SearchViewModel empty = new SearchViewModel()
                {
                    isAnythingFound = false
                };
                return View(empty);
            }
            else
            {
                SearchViewModel model = new SearchViewModel()
                {
                    Tags = db.Tags.Where(t => t.Name.Contains(searchedWord)).ToList(),
                    Users = db.Users.Where(u => u.Username.Contains(searchedWord)).ToList(),
                    isAnythingFound = true
                };
                if (model.Tags.Count == 0 && model.Users.Count == 0)
                {
                    model.isAnythingFound = false;                    
                }
                ViewBag.Count = model.Tags.Count + model.Users.Count;
                return View(model);
            }
        }
    }
}
