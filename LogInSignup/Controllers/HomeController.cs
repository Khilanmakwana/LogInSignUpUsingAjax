using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogInSignup.Models;

namespace LogInSignup.Controllers
{
    public class HomeController : Controller
    {
        getmecabEntities db = new getmecabEntities();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult SaveData(User model)
        {
            db.Users.Add(model);
            db.SaveChanges();
            return Json("Registration Successful", JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckValidUser(User model)
        {
            string result = "Fail";
            var DataItem = db.Users.Where(x => x.Email == model.Email && x.Password == model.Password).SingleOrDefault();
            if (DataItem != null)
            {
                Session["UserId"] = DataItem.Id.ToString();
                Session["UserName"] = DataItem.Email.ToString();
                result = "Success";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AfterLogin()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}