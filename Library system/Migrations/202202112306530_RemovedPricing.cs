namespace Library_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedPricing : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Books", "pricePerDay");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "pricePerDay", c => c.Double(nullable: false));
        }
    }
}
