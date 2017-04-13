using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Le6pergram.Web.ViewModels
{
    public class CreatePictureViewModel
    {
        public string Description { get; set; }

        public HttpPostedFileBase ContentFile { get; set; }
    }
}