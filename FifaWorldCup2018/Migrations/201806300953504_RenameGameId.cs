namespace FifaWorldCup2018.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameGameId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Scores", "Game_ID", "dbo.Games");
            RenameColumn(table: "dbo.Scores", name: "Game_ID", newName: "Game_GameID");
            RenameIndex(table: "dbo.Scores", name: "IX_Game_ID", newName: "IX_Game_GameID");
            DropPrimaryKey("dbo.Games");
            DropColumn("dbo.Games", "ID");
            AddColumn("dbo.Games", "GameID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Games", "GameID");
            AddForeignKey("dbo.Scores", "Game_GameID", "dbo.Games", "GameID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Games", "ID", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Scores", "Game_GameID", "dbo.Games");
            DropPrimaryKey("dbo.Games");
            DropColumn("dbo.Games", "GameID");
            AddPrimaryKey("dbo.Games", "ID");
            RenameIndex(table: "dbo.Scores", name: "IX_Game_GameID", newName: "IX_Game_ID");
            RenameColumn(table: "dbo.Scores", name: "Game_GameID", newName: "Game_ID");
            AddForeignKey("dbo.Scores", "Game_ID", "dbo.Games", "ID");
        }
    }
}
