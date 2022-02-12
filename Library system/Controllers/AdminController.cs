using Library_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library_system.Controllers
{
    public class AdminController : Controller
    {
        BlogDbContext db = new BlogDbContext();

        public string checkLogin()
        {
            if (UserProfile.ID != 0)
                return "approved";

            else
                return "denied";
        }
        public ActionResult ShowUsers() 
        {
            var value=checkLogin();
            if(value== "approved") 
            {
                var UserList = db.Users.ToList();
                return View(UserList);
            }
            else 
            {
                return RedirectToAction("Index","Home");
            }
        }

        public ActionResult ShowLoans()
        {
            var value = checkLogin();
            if (value == "approved")
            {
                List<Loan> LoansList;
                if (UserProfile.Admin == true)
                {
                    LoansList = db.loans.ToList();
                    ViewBag.Admin = true;
                }
                else
                {
                    LoansList = db.loans.Where(x => x.UserId == UserProfile.ID).ToList();
                    ViewBag.Admin = false;
                }
                return View(LoansList);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public string addUser(UserProfileEdit User) 
        {
            var SearchedUser = db.Users.Where(x => x.UserName == User.UserName).FirstOrDefault();
            var controller = new HomeController();
            var EncryptedPass=controller.Encrypt(User.Password);
            if (SearchedUser == null)
            {
                db.Users.Add(new User
                {
                    UserName = User.UserName,
                    Email = User.Email,
                    IsAdmin = User.Admin,
                    Password = EncryptedPass
                });
                db.SaveChanges();
                return "Success";
            }
            return "Failed";
        } 
        public string addBook(BookEdit Book) 
        {
            var SearchedBook = db.Books.Where(x => x.BookName == Book.BookName).FirstOrDefault();
            if (SearchedBook == null) 
            {
                db.Books.Add(new Book
                {
                    BookName = Book.BookName,
                    CopiesLoaned = Book.CopiesLoaned,
                    TotalCopies = Book.TotalCopies
                });
                db.SaveChanges();
                return "Success";

            }
            return "Failed";
        }
        public string addLoan(LoanEdit Loan)
        {
            var SearchedBook = db.Books.Where(x => x.BookId == Loan.BookId).FirstOrDefault();
            var UserLoanedAlready=db.loans.Where(x=>x.UserId ==UserProfile.ID && x.BookId==Loan.BookId).FirstOrDefault();
            if (SearchedBook.CopiesLoaned < SearchedBook.TotalCopies && UserLoanedAlready==null)
            {
                SearchedBook.CopiesLoaned += 1;
                db.loans.Add(new Loan
                {
                    BookId = Loan.BookId,
                    UserId = UserProfile.ID,
                    LoanedUntil = Loan.LoanedUntil
                });
                db.SaveChanges();
                return "Success";

            }
            return "Failed";
        }
        [HttpPost]
        public string EditUser(UserProfileEdit EditedUser ,int UserId = 0) 
        {
            if(UserId == 0) { UserId=UserProfile.ID;}

            var user = db.Users.Where(x => x.UserId == UserId).FirstOrDefault();
            if(user != null) 
            {
                var controller = new HomeController();
                var EncryptedPass = controller.Encrypt(user.Password);

                user.UserName = EditedUser.UserName;
                user.Password = EncryptedPass;
                user.IsAdmin = EditedUser.Admin;
                user.Email = EditedUser.Email;
                db.SaveChanges();

                UserProfile.Admin= EditedUser.Admin;
                UserProfile.Email= EditedUser.Email;
                UserProfile.User= EditedUser.UserName;

                return "Success";

            }
            return "Failed";

        }
        public string DeleteUser(int UserId) 
        {
            var user = db.Users.Where(x => x.UserId == UserId).FirstOrDefault();
            if(user != null) 
            {
                db.Users.Remove(user);
                db.SaveChanges();
                return "Success";
            }
            return "Failed";
        }   
        public string EditBook(int bookId, BookEdit EditedBook) 
        {
            var book = db.Books.Where(x => x.BookId == bookId).FirstOrDefault();
            if (book != null)
            {
                book.BookName = EditedBook.BookName;
                book.TotalCopies = EditedBook.TotalCopies;
                book.CopiesLoaned = EditedBook.CopiesLoaned;
                db.SaveChanges();
                return "Success";
            }
            return "Failed";
        }     
        public string DeleteBook(int BookId) 
        {
            var book = db.Books.Where(x => x.BookId == BookId).FirstOrDefault();
            if (book != null)
            {
                db.Books.Remove(book);
                db.SaveChanges();
                return "Success";
            }
            return "Failed";
        }

        public string EditLoan(LoanEdit Loan)
        {
            var SearchedLoan = db.loans.Where(x => x.BookId == Loan.BookId && x.UserId==Loan.UserId).FirstOrDefault();
            if (SearchedLoan!=null)
            {
                SearchedLoan.BookId = Loan.BookId;
                SearchedLoan.UserId = Loan.UserId;
                SearchedLoan.LoanedUntil = Loan.LoanedUntil;
                db.SaveChanges();
                return "Success";

            }
            return "Failed";
        }
        public string DeleteLoan(int LoanID)
        {
            var SearchedLoan = db.loans.Where(x => x.LoanId == LoanID).FirstOrDefault();
            var SearchedBook = db.Books.Where(x => x.BookId == SearchedLoan.BookId).FirstOrDefault();
            SearchedBook.CopiesLoaned -= 1;
            if (SearchedLoan != null)
            {
                db.loans.Remove(SearchedLoan);
                db.SaveChanges();
                return "Success";

            }
            return "Failed";
        }


    }

}