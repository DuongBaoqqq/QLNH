using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankManagement.Models.DAO;
namespace BankManagement.Controllers
{
    public class ChangePasswordController : Controller
    {
        // GET: ChangePassword
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult CheckPass(string Pass)
        {
            return Json(CustomerDAO.Instance.CheckPass(Pass), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ChangePass(string Pass)
        {
            CustomerDAO.Instance.ChangeNewPassword(Pass);
            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}