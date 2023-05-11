using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankManagement.Models.DAO;
namespace BankManagement.Controllers
{
    public class TransfersCustomerController : Controller
    {
        // GET: TransfersCustomer
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult CheckMoney(string SoTien)
        {
            return Json(CustomerDAO.Instance.CheckMoney(SoTien),JsonRequestBehavior.AllowGet );
        }
        [HttpPost]
        public JsonResult SetCustomerTransfers(string STK)
        {
            CustomerTransfersDAO.Instance.SetAccountNumber(CustomerDAO.Instance.GetMainCustomer().AccountNumber,STK);
            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}