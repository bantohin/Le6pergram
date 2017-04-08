using Le6pergram.Web.Configurations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

public class Le6pergramDatabase : DbContext
{    

    public Le6pergramDatabase() : base("name=Le6pergramDatabase")
    {
        Database.SetInitializer(new DropCreateDatabaseAlways<Le6pergramDatabase>());
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        modelBuilder.Configurations.Add(new UserConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    public System.Data.Entity.DbSet<Le6pergram.Web.User> Users { get; set; }
}
