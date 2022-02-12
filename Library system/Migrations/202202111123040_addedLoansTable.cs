namespace Library_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedLoansTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Loans",
                c => new
                    {
                        LoanId = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        LoanedUntil = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.LoanId);
            
            AddColumn("dbo.Books", "pricePerDay", c => c.Int(nullable: false));
            DropColumn("dbo.Books", "LoanedUntil");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "LoanedUntil", c => c.DateTime(nullable: false));
            DropColumn("dbo.Books", "pricePerDay");
            DropTable("dbo.Loans");
        }
    }
}
