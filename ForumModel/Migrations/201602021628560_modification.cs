namespace ForumModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "modifiedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Topics", "modifiedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Topics", "modifiedAt");
            DropColumn("dbo.Posts", "modifiedAt");
        }
    }
}
