namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Description_And_Image_To_Post : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Image", c => c.String());
            AddColumn("dbo.Posts", "Discription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Discription");
            DropColumn("dbo.Posts", "Image");
        }
    }
}
