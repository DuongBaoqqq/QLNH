using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankManagement.Models.DAO;
namespace BankManagement.Controllers
{
    public class MoneyTransferController : Controller
    {
        // GET: MoneyTransfer
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Content()
        {
            return Json(CustomerDAO.Instance.Content(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetString(string Money, string Content)
        {
            CustomerTransfersDAO.Instance.SetDataCustomerTransfers(Money, Content);
            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}