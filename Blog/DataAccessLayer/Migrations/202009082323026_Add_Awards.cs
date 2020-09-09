namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Awards : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Awards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PostReadCount = c.Int(nullable: false),
                        PostWriteCount = c.Int(nullable: false),
                        CommentWriteCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AwardsApplicationUsers",
                c => new
                    {
                        Awards_Id = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Awards_Id, t.ApplicationUser_Id })
                .ForeignKey("dbo.Awards", t => t.Awards_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.Awards_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AwardsApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AwardsApplicationUsers", "Awards_Id", "dbo.Awards");
            DropIndex("dbo.AwardsApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AwardsApplicationUsers", new[] { "Awards_Id" });
            DropTable("dbo.AwardsApplicationUsers");
            DropTable("dbo.Awards");
        }
    }
}
