namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Post_Comment_Read_Write_count : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "PostsReadCount", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "PostsWriteCount", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "CommentsWriteCount", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "UsersReadCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "UsersReadCount");
            DropColumn("dbo.AspNetUsers", "CommentsWriteCount");
            DropColumn("dbo.AspNetUsers", "PostsWriteCount");
            DropColumn("dbo.AspNetUsers", "PostsReadCount");
        }
    }
}
