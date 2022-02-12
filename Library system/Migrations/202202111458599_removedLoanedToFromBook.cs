namespace Library_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedLoanedToFromBook : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Books", "LoanedTo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "LoanedTo", c => c.Int(nullable: false));
        }
    }
}
