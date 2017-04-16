namespace Le6pergram.Data.Configurations
{
    using Le6pergram.Models;
    using System.Data.Entity.ModelConfiguration;

    public class PictureConfiguration : EntityTypeConfiguration<Picture>
    {
        public PictureConfiguration()
        {
            this.HasMany(p => p.Comments).WithRequired(c => c.Picture).WillCascadeOnDelete(true);
        }
    }
}