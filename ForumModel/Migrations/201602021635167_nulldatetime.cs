namespace ForumModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nulldatetime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "modifiedAt", c => c.DateTime());
            AlterColumn("dbo.Topics", "modifiedAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Topics", "modifiedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Posts", "modifiedAt", c => c.DateTime(nullable: false));
        }
    }
}
