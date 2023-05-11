using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BankManagement.Models.EF;
using BankManagement.Models.DAO;
namespace BankManagement.Areas.Admin.Controllers
{
    public class Find_EditCustomerController : Controller
    {
        // GET: Admin/Find_EditCustomer
        public ActionResult Index()
        {
            return View();
        }
        // Tìm kiếm danh sách khách hàng theo từ khóa đã chọn
        [HttpPost]
        public JsonResult GetListCustomer(string Name, string IdCard, string Email, string PhoneNumber, string NameProvince, string NameDistrict, string NameAgency)
        {
            return Json(StaffDAO.Instance.GetListCustomer(Name,IdCard,Email,PhoneNumber,NameProvince,NameDistrict,NameAgency),JsonRequestBehavior.AllowGet);
        }
        // Lấy thông tin khách hàng khi biết số diện thoại
        [HttpPost]
        public JsonResult GetCustomer(string PhoneNumber)
        {
            var data = StaffDAO.Instance.GetCustomerByPhoneNumber(PhoneNumber);
            var data1 = StaffDAO.Instance.GetNameProvinceByIdProvince(data.ID_Province);
            var data2 = StaffDAO.Instance.GetNameDistrictById(data.ID_Province, data.ID_District);
            var data3 = StaffDAO.Instance.GetNameAgencyByID(data.IdAgency);
            return Json(new { data,data1,data2,data3}, JsonRequestBehavior.AllowGet);
        }
        // Lấy danh sách số tài khoản bằng số điện thoại
        [HttpPost]
        public JsonResult GetSTK(string PhoneNumber)
        {
            return Json(StaffDAO.Instance.GetListAccountNumberCustomer(PhoneNumber), JsonRequestBehavior.AllowGet);
        }
        // tạo đối tượng khách hàng dựa vào số điện thoaij
        [HttpPost]
        public JsonResult SetCustomer(string PhoneNumber)
        {
            CustomerDAO.Instance.SetCustomer(PhoneNumber);
            return Json("success", JsonRequestBehavior.AllowGet);
        }
        // thay đổi thông tin khách hàng
        public  JsonResult ChangeInfoCustomer(string Name,string IdCard,string PhoneNumber,string Address,string Birthday,string Email,bool Gender,string NameProvince,string NameDistrict)
        {
            int check = CustomerDAO.Instance.ChangeInfoCustomer(Name, IdCard, PhoneNumber, Address, Birthday, Email, Gender, NameProvince, NameDistrict);
            return Json(check, JsonRequestBehavior.AllowGet);
        }
    }
}