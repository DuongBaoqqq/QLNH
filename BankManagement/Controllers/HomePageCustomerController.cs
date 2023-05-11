using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankManagement.Controllers
{
    public class HomePageCustomerController : Controller
    {
        // GET: HomePageCustomer
        public ActionResult Index()
        {
            return View();
        }
    }
}