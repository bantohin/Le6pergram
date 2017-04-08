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

    public System.Data.Entity.DbSet<Le6pergram.Web.User> Users { get; set; }
}
