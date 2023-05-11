using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankManagement.Models.DAO;
namespace BankManagement.Controllers
{
    public class ServiceController : Controller
    {
        // GET: Service
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetState()
        {
            return Json(CustomerDAO.Instance.StateAccountNumber(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveLichSuKhoaThe(string reason)
        {
            CustomerDAO.Instance.SaveLock(reason);
            return Json("success", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveLichSuMoThe()
        {
            CustomerDAO.Instance.SaveUnlock();
            return Json("success", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ChangeMainSTK(string STK)
        {
            CustomerDAO.Instance.ChangeMainSTK(STK);
            return Json(CustomerDAO.Instance.AccountIssueDate(), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public string GetSTK() { return CustomerDAO.Instance.GetMainCustomer().AccountNumber; }
    }
}