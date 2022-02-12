using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library_system.Models
{
    public static class UserProfile
    {
        public static string User { get; set; }
        public static string Email { get; set; }
        public static int ID { get; set; }
        public static bool Admin { get; set; }

    }
    public class UserProfileEdit 
    {
    public string UserName { get; set; }
    public string Email { get; set; }
    public bool Admin { get; set; }
    public string Password { get; set; }
    }

    public class BookEdit
    {
        public string BookName { get; set; }
        public int TotalCopies { get; set; }
        public int CopiesLoaned { get; set; }
    }
    public class LoanEdit
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime LoanedUntil { get; set; }


    }
}