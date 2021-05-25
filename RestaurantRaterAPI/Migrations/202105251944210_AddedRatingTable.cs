namespace RestaurantRaterAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRatingTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RestaurantID = c.Int(nullable: false),
                        FoodScore = c.Double(nullable: false),
                        EnvironmentScore = c.Double(nullable: false),
                        CleanlinessScore = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantID, cascadeDelete: true)
                .Index(t => t.RestaurantID);
            
            DropColumn("dbo.Restaurants", "Rating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Restaurants", "Rating", c => c.Double(nullable: false));
            DropForeignKey("dbo.Ratings", "RestaurantID", "dbo.Restaurants");
            DropIndex("dbo.Ratings", new[] { "RestaurantID" });
            DropTable("dbo.Ratings");
        }
    }
}
