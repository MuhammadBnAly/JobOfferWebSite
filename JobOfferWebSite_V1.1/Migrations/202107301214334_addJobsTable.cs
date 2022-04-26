namespace JobOfferWebSite_V1._1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addJobsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobTitle = c.String(),
                        JobContent = c.String(),
                        JobImage = c.String(),
                        CategoryId = c.Int(nullable: false),
                        Categories_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Categories_ID)
                .Index(t => t.Categories_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "Categories_ID", "dbo.Categories");
            DropIndex("dbo.Jobs", new[] { "Categories_ID" });
            DropTable("dbo.Jobs");
        }
    }
}
