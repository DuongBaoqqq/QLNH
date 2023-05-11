using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankManagement.Models.DAO;
namespace BankManagement.Controllers
{
    public class CustomerLoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        // Input: user + password | output: valid ? true : false
        [HttpPost]
        public JsonResult CheckAccount(string User, string Password)
        {
            AccountStaffDAO ac = new AccountStaffDAO(User, Password);
            int check = ac.CheckAccountCustomer();
            if (check == 1)
            {
                CustomerDAO.Instance.SetCustomer(User);
            }
            return Json(check, JsonRequestBehavior.AllowGet);
        }
    }
}