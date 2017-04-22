namespace Le6pergram.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PrivateAccountsProperty : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Notifications", "PictureId", "dbo.Pictures");
            AddColumn("dbo.Users", "IsPrivate", c => c.Boolean(nullable: false, defaultValue: false));
            AddForeignKey("dbo.Notifications", "PictureId", "dbo.Pictures", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "PictureId", "dbo.Pictures");
            DropColumn("dbo.Users", "IsPrivate");
            AddForeignKey("dbo.Notifications", "PictureId", "dbo.Pictures", "Id");
        }
    }
}
