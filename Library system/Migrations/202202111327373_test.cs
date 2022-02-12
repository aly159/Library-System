namespace Library_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "pricePerDay", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "pricePerDay", c => c.Int(nullable: false));
        }
    }
}
