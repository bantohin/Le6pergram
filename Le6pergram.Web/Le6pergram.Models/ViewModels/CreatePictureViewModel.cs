namespace Le6pergram.Models.ViewModels
{
    using System.Web;

    public class CreatePictureViewModel
    {
        public string Description { get; set; }

        public string Tags { get; set; }  
        
        public HttpPostedFileBase ContentFile { get; set; }
    }
}