using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Employee_List.Models;

namespace Employee_List.Controllers
{
    public class LoginController : Controller
    {
        private EmployeeContext db = new EmployeeContext();

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "username,password")] Login login)
        {
            Login loginUser = db.Logins.Where(l => l.username == login.username).FirstOrDefault();

            if(loginUser != null)
            {
                if (loginUser.password.Equals(login.password))
                {
                    return RedirectToAction("index", "Employee");
                }
                else
                {
                    ViewBag.Warning = "Invalid Password";
                    return View();
                }
            }
            ViewBag.Warning = "Invalid Username";

            return View();
        }        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
