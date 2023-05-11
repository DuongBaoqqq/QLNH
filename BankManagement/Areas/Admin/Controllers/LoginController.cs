using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankManagement.Models.DAO;
namespace BankManagement.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult CheckAccount(string UserName,string Password)
        {
            AccountStaffDAO ac = new AccountStaffDAO(UserName, Password);
            int res = ac.CheckAccountStaff(); // -1 => tài khoản bị khóa | 0 => không tìm thấy tài khoản | 1 => tài khoản hợp lệ
            if (res == 1)
            {
                StaffDAO.Instance.SetStaff(ac.GetIDStaff());
            }
            return Json(ac.CheckAccountStaff(),JsonRequestBehavior.AllowGet);
        }
    }
}