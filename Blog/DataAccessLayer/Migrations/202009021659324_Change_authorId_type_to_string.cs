namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change_authorId_type_to_string : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Comments", new[] { "Author_Id" });
            DropIndex("dbo.Posts", new[] { "Author_Id" });
            DropColumn("dbo.Comments", "AuthorId");
            DropColumn("dbo.Posts", "AuthorId");
            RenameColumn(table: "dbo.Comments", name: "Author_Id", newName: "AuthorId");
            RenameColumn(table: "dbo.Posts", name: "Author_Id", newName: "AuthorId");
            AlterColumn("dbo.Comments", "AuthorId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Posts", "AuthorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Comments", "AuthorId");
            CreateIndex("dbo.Posts", "AuthorId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Posts", new[] { "AuthorId" });
            DropIndex("dbo.Comments", new[] { "AuthorId" });
            AlterColumn("dbo.Posts", "AuthorId", c => c.Int(nullable: false));
            AlterColumn("dbo.Comments", "AuthorId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Posts", name: "AuthorId", newName: "Author_Id");
            RenameColumn(table: "dbo.Comments", name: "AuthorId", newName: "Author_Id");
            AddColumn("dbo.Posts", "AuthorId", c => c.Int(nullable: false));
            AddColumn("dbo.Comments", "AuthorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Posts", "Author_Id");
            CreateIndex("dbo.Comments", "Author_Id");
        }
    }
}
