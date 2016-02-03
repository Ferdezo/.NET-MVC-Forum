namespace ForumModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TopicNewRelation : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Topics", name: "User_usrID", newName: "author_usrID");
            RenameIndex(table: "dbo.Topics", name: "IX_User_usrID", newName: "IX_author_usrID");
            AddColumn("dbo.Topics", "createdAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Topics", "createdAt");
            RenameIndex(table: "dbo.Topics", name: "IX_author_usrID", newName: "IX_User_usrID");
            RenameColumn(table: "dbo.Topics", name: "author_usrID", newName: "User_usrID");
        }
    }
}
