using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankManagement.Models.DAO;
using BankManagement.Models.LIB;
namespace BankManagement.Controllers
{
    public class ForgetPassController : Controller
    {
        // GET: ForgetPass
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetData(string SDT, string Email, string Name, string NS, string CMND)
        {
            return Json(AccountStaffDAO.CheckInfoCustomer(SDT, Email, Name, NS, CMND), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetOTP(string OTP)
        {
            return Json(SeenEmail.Instance.CheckOTP(OTP), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetNotifyReturnPass()
        {
            return Json(CustomerDAO.Instance.NotifyReturnPass(), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult SaveAndSeenNewPassword()
        {
            CustomerDAO.Instance.SaveRandomPassAndSeenEmail();
            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}