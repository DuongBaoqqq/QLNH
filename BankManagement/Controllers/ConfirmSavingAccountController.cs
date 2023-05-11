using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankManagement.Models.DAO;
namespace BankManagement.Controllers
{
    public class ConfirmSavingAccountController : Controller
    {
        // GET: ConfirmSavingAccount
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Save()
        {
            SavingAccountDAO.Instance.SaveLS();
            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}