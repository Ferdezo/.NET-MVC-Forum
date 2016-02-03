namespace ForumModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        posID = c.Int(nullable: false, identity: true),
                        content = c.String(),
                        createdAt = c.DateTime(nullable: false),
                        usrID = c.Int(nullable: false),
                        topID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.posID)
                .ForeignKey("dbo.Topics", t => t.topID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.usrID, cascadeDelete: true)
                .Index(t => t.usrID)
                .Index(t => t.topID);
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        topID = c.Int(nullable: false, identity: true),
                        title = c.String(),
                        User_usrID = c.Int(),
                    })
                .PrimaryKey(t => t.topID)
                .ForeignKey("dbo.Users", t => t.User_usrID)
                .Index(t => t.User_usrID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        usrID = c.Int(nullable: false, identity: true),
                        login = c.String(),
                        password = c.String(),
                        role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.usrID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Topics", "User_usrID", "dbo.Users");
            DropForeignKey("dbo.Posts", "usrID", "dbo.Users");
            DropForeignKey("dbo.Posts", "topID", "dbo.Topics");
            DropIndex("dbo.Topics", new[] { "User_usrID" });
            DropIndex("dbo.Posts", new[] { "topID" });
            DropIndex("dbo.Posts", new[] { "usrID" });
            DropTable("dbo.Users");
            DropTable("dbo.Topics");
            DropTable("dbo.Posts");
        }
    }
}
