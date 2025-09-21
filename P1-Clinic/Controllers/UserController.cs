using P1_Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using P1_Clinic.Global;
using System.Data.Entity;
using System.Web.Security;
namespace P1_Clinic.Controllers
{
    public class UserController:Controller
    {
        ClinicContext DBClinic = new ClinicContext();
        [HttpGet]
        public ActionResult SignUp()
        {
            ViewBag.Languages =new SelectList( DBClinic.Languages.ToList(),"Id","Name");
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(User user)
        {
            var isUserAlreadyInUse = DBClinic.Users.Any(u=>u.Email== user.Email&&u.Password==user.Password);
            if (!isUserAlreadyInUse)
            {
                DBClinic.Users.Add(user);
                DBClinic.SaveChanges();
                return RedirectToAction("SignIn", new User());
            }
            ViewBag.Languages = new SelectList(DBClinic.Languages.ToList(), "Id", "Name");
            ModelState.AddModelError("", "Person already in user try to sign in ");
            return View();
        }

        [HttpGet]
        public ActionResult SignIn()
        {
            return View(); 

        }
        [HttpPost]
        public ActionResult SignIn(User Loginuser) { 

        var IsUserExist =DBClinic.Users.Any(u=>u.Email==Loginuser.Email&&u.Password==Loginuser.Password);
            if (!IsUserExist) {
                ModelState.AddModelError("", "In correct Username or password");
                return View();
            }
           ClsGlobal.User=DBClinic.Users.Include(u=>u.Language).FirstOrDefault(u=>u.Email==Loginuser.Email);
            return RedirectToAction("Index","Home");

        }
        [HttpGet]
        public ActionResult SignOut() {
            FormsAuthentication.SignOut();
            Session.Clear(); 
        return RedirectToAction("SignIn", "User");
        }
    }
}