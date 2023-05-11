using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankManagement.Models.DAO;
namespace BankManagement.Controllers
{
    public class ConfirmTransferController : Controller
    {
        // GET: ConfirmTransfer
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Save()
        {
            CustomerTransfersDAO.Instance.Transfer();
            return Json("success", JsonRequestBehavior.AllowGet);
        }
    }
}