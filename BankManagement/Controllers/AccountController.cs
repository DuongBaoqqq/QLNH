using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankManagement.Models.DAO;
namespace BankManagement.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetSoThe()
        {
            return Json(CustomerDAO.Instance.GetCountAccountNumber(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SetSTK(int index)
        {
            var data = CustomerDAO.Instance.GetListAccountNumberCustomer()[index - 1];
            ListTransactionDAO.Instance.SetSTK(data.AccountNumber);
            return Json("success", JsonRequestBehavior.AllowGet);
        }

    }
}