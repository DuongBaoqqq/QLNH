using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankManagement.Models.DAO;
using BankManagement.Models.LIB;
namespace BankManagement.Controllers
{
    public class CreateSavingAccountController : Controller
    {
        // GET: CreateSavingAccount
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetLaiSuatByKiHan(string Kihan)
        {
            return Json(CustomerDAO.GetInterestRateByTerm(Kihan), JsonRequestBehavior.AllowGet);
        }
        public JsonResult ConvertMoney(string SoTien)
        {
            return Json(Function.ConvertMoneyToMoney(SoTien), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CheckMoney(string SoTien)
        {
            return Json(CustomerDAO.Instance.CheckMoney(SoTien), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult StringMoney(string Money)
        {
            return Json(Function.StringMoney(Money), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult TienLai(string money, string KiHan)
        {
            return Json(CustomerDAO.CalculateInterestRate(money, KiHan), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CreateSavingAccount1(string Sotien, string Kihan)
        {
            SavingAccountDAO.Instance.CreateSavingAccount(Sotien, Kihan);
            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}