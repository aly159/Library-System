using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Library_system.Models
{
    public class BlogDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Loan> loans { get; set; }
    }
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string BookName { get; set; }
        public int TotalCopies { get; set; }
        public int CopiesLoaned { get; set; }
    }
    public class Loan 
    {
        [Key]
        public int LoanId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime LoanedUntil { get; set; }

    
    }
   
}