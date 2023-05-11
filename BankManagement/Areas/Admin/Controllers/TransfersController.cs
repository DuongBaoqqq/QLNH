using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankManagement.Models.DAO;
using BankManagement.Models.LIB;
namespace BankManagement.Areas.Admin.Controllers
{
    public class TransfersController : Controller
    {
        // GET: Admin/TransfersCustomer
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetNameCustomer(string AccountNumberCustomer)
        {
            return Json(CustomerDAO.GetNameCustomerByAccountNumberCustomer(AccountNumberCustomer), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult StringMoney(string Money)
        {
            return Json(Function.StringMoney(Money), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ConvertMoney(string SoTien)
        {
            return Json(Function.ConvertMoneyToMoney(SoTien), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveTransaction(string Money, string STK, string Content)
        {
            StaffDAO.Instance.CustomerReceiveMoney(Money, STK);
            StaffDAO.Instance.MoneyTransferBank(STK, Money, Content);
            StaffDAO.Instance.CashReceivingBank(Money);
            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}