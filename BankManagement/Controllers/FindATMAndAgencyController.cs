using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankManagement.Models.DAO;
namespace BankManagement.Controllers
{
    public class FindATMAndAgencyController : Controller
    {
        // GET: FindATMAndAgency
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetAddress()
        {
            return Json(GetAddressDAO.Instance.GetAddress(), JsonRequestBehavior.AllowGet);
        }
    }
}