namespace Le6pergram.Data.Configurations
{
    using Models;
    using System.Data.Entity.ModelConfiguration;

    public class PictureConfiguration : EntityTypeConfiguration<Picture>
    {
        public PictureConfiguration()
        {
            HasMany(p => p.Comments).WithRequired(c => c.Picture).WillCascadeOnDelete(true);

            HasMany(p => p.Notifications).WithOptional(n => n.Picture).WillCascadeOnDelete(true);
        }
    }
}