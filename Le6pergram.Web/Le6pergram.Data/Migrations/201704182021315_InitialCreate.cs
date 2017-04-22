namespace Le6pergram.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        UserId = c.Int(nullable: false),
                        Picture_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pictures", t => t.Picture_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.Picture_Id);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Description = c.String(),
                        UploadDate = c.DateTime(nullable: false),
                        Content = c.Binary(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Username = c.String(maxLength: 50, unicode: false),
                        Password = c.String(),
                        Email = c.String(maxLength: 50, unicode: false),
                        Biography = c.String(),
                        RegisterProfilePicture = c.Binary(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Username, unique: true, name: "IX_User_Username")
                .Index(t => t.Email, unique: true, name: "IX_User_Email");
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SenderId = c.Int(nullable: false),
                        ReceiverId = c.Int(nullable: false),
                        PictureId = c.Int(),
                        Type = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ReceiverId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.SenderId)
                .ForeignKey("dbo.Pictures", t => t.PictureId)
                .Index(t => t.SenderId)
                .Index(t => t.ReceiverId)
                .Index(t => t.PictureId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UsersFollowers",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        FollowerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.FollowerId })
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Users", t => t.FollowerId)
                .Index(t => t.UserId)
                .Index(t => t.FollowerId);
            
            CreateTable(
                "dbo.LikesOfPicture",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        PictureId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.PictureId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Pictures", t => t.PictureId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.PictureId);
            
            CreateTable(
                "dbo.TagPictures",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        Picture_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Picture_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.Pictures", t => t.Picture_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.Picture_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagPictures", "Picture_Id", "dbo.Pictures");
            DropForeignKey("dbo.TagPictures", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.Notifications", "PictureId", "dbo.Pictures");
            DropForeignKey("dbo.Pictures", "UserId", "dbo.Users");
            DropForeignKey("dbo.Notifications", "SenderId", "dbo.Users");
            DropForeignKey("dbo.Notifications", "ReceiverId", "dbo.Users");
            DropForeignKey("dbo.LikesOfPicture", "PictureId", "dbo.Pictures");
            DropForeignKey("dbo.LikesOfPicture", "UserId", "dbo.Users");
            DropForeignKey("dbo.UsersFollowers", "FollowerId", "dbo.Users");
            DropForeignKey("dbo.UsersFollowers", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "Picture_Id", "dbo.Pictures");
            DropIndex("dbo.TagPictures", new[] { "Picture_Id" });
            DropIndex("dbo.TagPictures", new[] { "Tag_Id" });
            DropIndex("dbo.LikesOfPicture", new[] { "PictureId" });
            DropIndex("dbo.LikesOfPicture", new[] { "UserId" });
            DropIndex("dbo.UsersFollowers", new[] { "FollowerId" });
            DropIndex("dbo.UsersFollowers", new[] { "UserId" });
            DropIndex("dbo.Notifications", new[] { "PictureId" });
            DropIndex("dbo.Notifications", new[] { "ReceiverId" });
            DropIndex("dbo.Notifications", new[] { "SenderId" });
            DropIndex("dbo.Users", "IX_User_Email");
            DropIndex("dbo.Users", "IX_User_Username");
            DropIndex("dbo.Pictures", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "Picture_Id" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropTable("dbo.TagPictures");
            DropTable("dbo.LikesOfPicture");
            DropTable("dbo.UsersFollowers");
            DropTable("dbo.Tags");
            DropTable("dbo.Notifications");
            DropTable("dbo.Users");
            DropTable("dbo.Pictures");
            DropTable("dbo.Comments");
        }
    }
}
