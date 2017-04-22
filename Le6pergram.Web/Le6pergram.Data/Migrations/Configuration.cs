namespace Le6pergram.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Le6pergramDatabase>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Le6pergramDatabase";
        }

        protected override void Seed(Le6pergramDatabase context)
        {
            
        }
    }
}
