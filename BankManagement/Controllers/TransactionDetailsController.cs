using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankManagement.Models.DAO;
namespace BankManagement.Controllers
{
    public class TransactionDetailsController : Controller
    {
        // GET: TransactionDetails
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult list()
        {
            return Json(ListTransactionDAO.Instance.ls, JsonRequestBehavior.AllowGet);
        }
    }
}