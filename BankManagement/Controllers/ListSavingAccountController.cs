using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankManagement.Models.DAO;
namespace BankManagement.Controllers
{
    public class ListSavingAccountController : Controller
    {
        // GET: ListSavingAccount
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetCountListSavingAccount()
        {
            return Json(CustomerDAO.Instance.GetCountListSavingAccount(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetListSavingAccount(int index)
        {
            return Json(CustomerDAO.Instance.GetDataSavingAccount(index), JsonRequestBehavior.AllowGet);
        }
    }
}