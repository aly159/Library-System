namespace Library_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedMethod : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Users (UserName,Password,Email,IsAdmin) VALUES('Admin','MTIzYWRlZkBAa2Z4Y2J2QA==','aly_ms_99@hotmail.com','1')");
            Sql("INSERT INTO Users (UserName,Password,Email,IsAdmin) VALUES('Aly','MTIzYWRlZkBAa2Z4Y2J2QA==','aly_ms_88@hotmail.com','0')");
            Sql("INSERT INTO Books (BookName,LoanedTo,TotalCopies,CopiesLoaned,pricePerDay) VALUES('Book1','2','1','1','0.1')");
            Sql("INSERT INTO Books (BookName,LoanedTo,TotalCopies,CopiesLoaned,pricePerDay) VALUES('Book2','2','2','0','0.2')");
            Sql("INSERT INTO Books (BookName,LoanedTo,TotalCopies,CopiesLoaned,pricePerDay) VALUES('Book3','2','4','0','0.5')");
            Sql("INSERT INTO Books (BookName,LoanedTo,TotalCopies,CopiesLoaned,pricePerDay) VALUES('Book4','2','10','0','1')");
            Sql("INSERT INTO Books (BookName,LoanedTo,TotalCopies,CopiesLoaned,pricePerDay) VALUES('Book5','2','5','0','0.6')");
            Sql("INSERT INTO Loans (BookId,UserId,LoanedUntil) VALUES('1','2','2022-10-03')");
        }
        
        public override void Down()
        {
        }
    }
}
