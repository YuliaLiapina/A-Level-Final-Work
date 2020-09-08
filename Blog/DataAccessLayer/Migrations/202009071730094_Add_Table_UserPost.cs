namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Table_UserPost : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.PostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserPosts", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserPosts", "PostId", "dbo.Posts");
            DropIndex("dbo.UserPosts", new[] { "PostId" });
            DropIndex("dbo.UserPosts", new[] { "UserId" });
            DropTable("dbo.UserPosts");
        }
    }
}
