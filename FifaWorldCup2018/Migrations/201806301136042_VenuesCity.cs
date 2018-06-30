namespace FifaWorldCup2018.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VenuesCity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Scores", "Group_GroupID", c => c.Int());
            CreateIndex("dbo.Scores", "Group_GroupID");
            AddForeignKey("dbo.Scores", "Group_GroupID", "dbo.Groups", "GroupID");
            DropColumn("dbo.Teams", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Teams", "Image", c => c.String());
            DropForeignKey("dbo.Scores", "Group_GroupID", "dbo.Groups");
            DropIndex("dbo.Scores", new[] { "Group_GroupID" });
            DropColumn("dbo.Scores", "Group_GroupID");
        }
    }
}
