using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankManagement.Models.DAO;
namespace BankManagement.Controllers
{
    public class CreateNewAccountController : Controller
    {
        // GET: CreateNewAccount
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult CheckSTK(string STK)
        {
            return Json(CustomerDAO.Instance.CheckSTK(STK), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SetSTK(string STK)
        {
            CustomerDAO.Instance.AddSTK(STK);
            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}