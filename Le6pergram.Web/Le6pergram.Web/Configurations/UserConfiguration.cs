using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Le6pergram.Web.Configurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            this.HasMany(u => u.Followers).WithMany(f => f.Following).Map(uf =>
            {
                uf.MapLeftKey("UserId");
                uf.MapRightKey("FollowerId");
                uf.ToTable("UsersFollowers");
            });
            this.Ignore(u => u.RepeatPassword);            
            this.Ignore(u => u.ProfilePictureFile);

            this.HasMany(u => u.Pictures)
                .WithRequired(p => p.User).WillCascadeOnDelete(false);

            this.HasMany(u => u.Comments)
                .WithRequired(c => c.User);

            this.HasMany(u => u.LikedPictures).WithMany(p => p.Likes).Map(m =>
            {
                m.MapLeftKey("UserId");
                m.MapRightKey("PictureId");
                m.ToTable("LikesOfPicture");
            });
        }
    }
}