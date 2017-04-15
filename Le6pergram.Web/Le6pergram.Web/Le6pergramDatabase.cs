using Le6pergram.Web;
using Le6pergram.Web.Configurations;
using Le6pergram.Web.Initializers;
using Le6pergram.Web.Models;
using System.Data.Entity;

public class Le6pergramDatabase : DbContext
{    

    public Le6pergramDatabase() : base("name=Le6pergramDatabase")
    {
        Database.SetInitializer(new MyInitializer());
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        modelBuilder.Configurations.Add(new PictureConfiguration());
        modelBuilder.Configurations.Add(new UserConfiguration());

        //modelBuilder.Entity<Tag>()
        //    .Ignore(t => t.picturesCount);

        base.OnModelCreating(modelBuilder);
    }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Picture> Pictures { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }  
    
    public virtual DbSet<Comment> Comments { get; set; }
}
