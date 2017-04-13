namespace Le6pergram.Web.Configurations
{
    using Le6pergram.Web.Models;
    using System.Data.Entity.ModelConfiguration;


    public class PictureConfiguration : EntityTypeConfiguration<Picture>
    {
        public PictureConfiguration()
        {
            this.Ignore(p => p.ContentFile);
        }
    }
}