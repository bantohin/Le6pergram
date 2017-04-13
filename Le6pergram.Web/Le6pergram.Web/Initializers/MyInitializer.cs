using System.Data.Entity;

namespace Le6pergram.Web.Initializers
{
    public class MyInitializer : DropCreateDatabaseAlways<Le6pergramDatabase>
    {
        protected override void Seed(Le6pergramDatabase context)
        {
            var pesho = new User()
            {
                Name = "Pesho",
                Password = "pesho",
                Username = "pesho",
                Biography = "pesho oesho",
                Email = "pesho@pesho.bg",
            };

            var mitio = new User()
            {
                Name = "Mitio",
                Password = "mitio",
                Username = "mitio",
                Biography = "mitio mitio",
                Email = "mitio@mitio.mitio",
            };

            pesho.Followers.Add(mitio);
            context.Users.Add(pesho);
            context.Users.Add(mitio);
            context.SaveChanges();
            base.Seed(context);
        }
    }
}