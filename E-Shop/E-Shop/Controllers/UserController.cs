using DataAccessLayer.Context;
using EntitiyLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace E_Shop.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        DataContext db = new DataContext();
        public ActionResult Update()
        {
            var username = (string)Session["Mail"];
            var değerler = db.Users.FirstOrDefault(x => x.Email == username);
            return View(değerler);
            
        }

        [HttpPost]
        public ActionResult Update(User data)
        {
            var username = (string)Session["Mail"];
            var user = db.Users.Where(x => x.Email == username).FirstOrDefault();
            user.Name = data.Name;
            user.SurName = data.SurName;
            user.Email = data.Email;
            user.Password = data.Password;
            user.RePassword = data.RePassword;
            user.UserName = data.UserName;
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult PasswordReset()
        {
            return View();
        }
        [HttpPost]
        
        public ActionResult PasswordReset(string eposta)
        {
            var email = db.Users.Where(x => x.Email == eposta).SingleOrDefault();
            if (email != null)
            {
                Random rnd = new Random();
                int yenisifre = rnd.Next();
                User sifre = new User ();
                email.Password = (Convert.ToString(yenisifre));
                db.SaveChanges();
                WebMail.SmtpServer = "smtp.gamil.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "projedeneme777@outlok.com";
                WebMail.Password = "Canberk1910";
                WebMail.SmtpPort = 587;
                WebMail.Send(eposta, "Giriş Şifreniz", "Şifreniz" + yenisifre);
                ViewBag.uyari = "Şifreniz başarıyla gönderilmiştir.";
            }
            else
            {
                ViewBag.uyari = "Hata oluştu tekrar deneyiniz";
            }
            return View();
        }

    }
}