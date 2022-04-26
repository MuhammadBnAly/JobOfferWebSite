namespace JobOfferWebSite_V1._1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class applyForJob : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplyForJobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        ApplyDate = c.DateTime(nullable: false),
                        JobId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        jobs_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jobs", t => t.jobs_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.jobs_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplyForJobs", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplyForJobs", "jobs_Id", "dbo.Jobs");
            DropIndex("dbo.ApplyForJobs", new[] { "jobs_Id" });
            DropIndex("dbo.ApplyForJobs", new[] { "UserId" });
            DropTable("dbo.ApplyForJobs");
        }
    }
}
