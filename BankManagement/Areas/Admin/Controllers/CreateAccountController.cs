using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankManagement.Models.DAO;
namespace BankManagement.Areas.Admin.Controllers
{
    public class CreateAccountController : Controller
    {
        // GET: Admin/CreateAccount
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        // Truyền xuống view danh sách quận huyện dựa vào tỉnh thành cho trước
        public JsonResult GetNameDistrictByProvince(string Province)
        {
            return Json(GetAddressDAO.Instance.GetNameDistrictByProvince(Province),JsonRequestBehavior.AllowGet);
        }
        // Kiểm tra thông tin tạo tài khoản có bị trùng với thông tin ở database hay không
        public JsonResult CheckInfoCustomer(string Name, string CMND, string NS, string SDT, string Tinh, string Huyen, string DiaChi, string Email, int Gender)
        {
            return Json(StaffDAO.Instance.CheckInfoCustomer(Name, CMND, NS, SDT, Tinh, Huyen, DiaChi, Email, Gender), JsonRequestBehavior.AllowGet);
        }
    }
}