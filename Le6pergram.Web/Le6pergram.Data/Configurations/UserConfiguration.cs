namespace Le6pergram.Data.Configurations
{
    using Models;
    using System.Data.Entity.ModelConfiguration;

    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            HasMany(u => u.Followers).WithMany(f => f.Following).Map(uf =>
            {
                uf.MapLeftKey("UserId");
                uf.MapRightKey("FollowerId");
                uf.ToTable("UsersFollowers");
            });
            Ignore(u => u.RepeatPassword);            
            Ignore(u => u.ProfilePictureFile);

            HasMany(u => u.Pictures)
                .WithRequired(p => p.User).WillCascadeOnDelete(false);

            HasMany(u => u.Comments)
                .WithRequired(c => c.User);

            HasMany(u => u.LikedPictures).WithMany(p => p.Likes).Map(m =>
            {
                m.MapLeftKey("UserId");
                m.MapRightKey("PictureId");
                m.ToTable("LikesOfPicture");
            });

            HasMany(u => u.Notifications).WithRequired(n => n.Receiver);

            HasMany(u => u.Notifications).WithRequired(n => n.Receiver).WillCascadeOnDelete(false);
            HasMany(u => u.Notifications).WithRequired(n => n.Sender).WillCascadeOnDelete(false);
        }
    }
}