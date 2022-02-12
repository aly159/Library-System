using Library_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library_system.Controllers
{
    public class HomeController : Controller
    {
        BlogDbContext db = new BlogDbContext();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public string Login(string UserName,string Password)
        {
            var EncryptedPass=Encrypt(Password);
             var user = db.Users.Where(x => x.UserName.ToLower()== UserName.ToLower() && x.Password== EncryptedPass).FirstOrDefault();
            if (user != null) 
            {
                checkDates();
                UserProfile.User=user.UserName;
                UserProfile.ID=user.UserId;
                UserProfile.Email=user.Email;
                UserProfile.Admin=user.IsAdmin;
                return "Success";
            }
             return "Failed";
        }
        [HttpPost]
        public string SignUp(string Username, string password, string mail)
        {
            var ifexists = db.Users.Where(x => x.UserName == Username).FirstOrDefault();
            var EncryptedPass = Encrypt(password);
            if (ifexists != null) {
                return "already exists";
            }
            var user = new User {
                Email = mail,
                UserName = Username,
                Password = EncryptedPass,
                IsAdmin = false,
            };
            db.Users.Add(user);
            db.SaveChanges();

            UserProfile.User = Username;
            UserProfile.ID = user.UserId;
            UserProfile.Email = mail;
            UserProfile.Admin = false;
            return "Success";
        }
        public ActionResult HomePage() 
        {
            var value = new AdminController().checkLogin();
            if (value == "approved") 
            {
                ViewBag.IsAdmin = UserProfile.Admin;
                var books = db.Books.ToList();
                return View(books);
            }
            else 
            {
                return RedirectToAction("Index");
            }
        }
        public void checkDates() 
        {
           string now= DateTime.Now.ToString("yyyy/MM/dd");
           DateTime Now= Convert.ToDateTime(now);
            var ReturnList=db.loans.Where(x=>x.LoanedUntil>=Now).ToList();
            if (ReturnList.Count > 0 && ReturnList != null) 
            {
                db.loans.RemoveRange(ReturnList);
            }
        }

        public string ImportCsv(string fyl)
        {
            var SubData = fyl.Split(',');
            for (int i = 0; i < SubData.Length - 1; i += 4)
            {
                 var userName = SubData[i];
                var IsAdmin = false;
                var EncryptedPass = Encrypt(SubData[i + 1]);
                 var user = db.Users.Where(x => x.UserName == userName).FirstOrDefault();
                    if(SubData[i + 3] == "1") { IsAdmin = true; }
                    if (user == null)
                    {
                        db.Users.Add(new User()
                        {
                            UserName = SubData[i],
                            Password = EncryptedPass,
                            Email = SubData[i + 2],
                            IsAdmin = IsAdmin

                        });
                    }
                    else
                    {
                        user.UserName= SubData[i];
                        user.Password = SubData[i + 1];
                        user.Email=SubData[i + 2];
                        user.IsAdmin = IsAdmin;
                    }
            }
            db.SaveChanges();


            return "Done :D";
        }
        public string BatchLoans(string fyl)
        {
            var SubData = fyl.Split(',');
            for (int i = 0; i < SubData.Length - 1; i += 3)
            {
                DateTime date = Convert.ToDateTime(SubData[i+2]);
                var BookID = int.Parse(SubData[i]);
                var Userid = int.Parse(SubData[i + 1]);
                var Loan = db.loans.Where(x => x.BookId == BookID && x.UserId== Userid).FirstOrDefault();
                if (Loan == null)
                {
                    db.loans.Add(new Loan()
                    {
                       BookId= int.Parse(SubData[i]),
                       UserId= int.Parse(SubData[i+1]),
                       LoanedUntil=date
                    });
                }
                else
                {
                    Loan.BookId = int.Parse(SubData[i]);
                    Loan.UserId = int.Parse(SubData[i + 1]);
                    Loan.LoanedUntil = date;
                }
            }
            db.SaveChanges();


            return "Done :D";
        }

        public string BatchBooks(string fyl)
        {
            var SubData = fyl.Split(',');
            for (int i = 0; i < SubData.Length - 1; i += 3)
            {
                var BookName = SubData[i];
                var Book = db.Books.Where(x => x.BookName== BookName).FirstOrDefault();
                if (Book == null)
                {
                    db.Books.Add(new Book()
                    {
                        BookName = SubData[i],
                        TotalCopies = int.Parse(SubData[i + 1]),
                        CopiesLoaned = int.Parse(SubData[i + 2])
                    });
                }
                else
                {
                    Book.BookName = SubData[i];
                    Book.TotalCopies = int.Parse(SubData[i + 1]);
                    Book.CopiesLoaned = int.Parse(SubData[i + 2]);
                }
            }
            db.SaveChanges();

            return "Success";
        }
        public static string key = "adef@@kfxcbv@";
        public string Encrypt(string pass) 
        {
            if (string.IsNullOrEmpty(pass)) 
            {
                return "";
            }
            pass += key;
            var bytes = System.Text.Encoding.UTF8.GetBytes(pass);
            return Convert.ToBase64String(bytes);
        }
        public string Decrypt(string Base64) 
        {
            if (string.IsNullOrEmpty(Base64))
            {
                return "";
            }
            var bytes = Convert.FromBase64String(Base64);
            var result = System.Text.Encoding.UTF8.GetString(bytes);
            return result.Substring(0, result.Length - key.Length);
        }
    }
}