using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankManagement.Models.EF;
using BankManagement.Models.LIB;
namespace BankManagement.Models.DAO
{
    public class AccountStaffDAO
    {
        private string UserName { get; set; }
        private string Password { get; set; }
        public AccountStaffDAO(string UserName, string Password)
        {
            this.UserName = UserName;
            this.Password = Password;
        }
        // Kiểm tra xem tài khoản có hợp lệ
        public int CheckAccountStaff()
        {
            if (BankManagementDbContext.Instance.ACCOUNT_STAFF.Where(p => p.UserName == this.UserName && p.Pass == this.Password).Count() > 0)
            {
                if (Convert.ToBoolean(BankManagementDbContext.Instance.ACCOUNT_STAFF.Where(p => p.UserName == this.UserName).SingleOrDefault().StateAccount))
                    return 1;
                else
                {
                    return -1;
                }
            }
            else
            {
                return 0;
            }
        }
        public int CheckAccountCustomer()
        {
            if (BankManagementDbContext.Instance.ACCOUNT_CUSTOMER.Where(p => p.CUSTOMER.PhoneNumber == UserName && p.Pass == Password).Count() > 0)
            {
                if (Convert.ToBoolean(BankManagementDbContext.Instance.CUSTOMERs.Where(p => p.PhoneNumber == UserName).SingleOrDefault().State))
                {
                    return 1;
                }
                return -1;
            }
            return 0;
        }
        public string GetIDStaff()
        {
            return BankManagementDbContext.Instance.ACCOUNT_STAFF.Where(p => p.UserName == this.UserName).SingleOrDefault().ID;
        }
        public static bool CheckInfoCustomer(string PhoneNumber, string Email, string Name, string Birthday, string IdCard)
        {
            DateTime date = Convert.ToDateTime(Birthday);
            if (BankManagementDbContext.Instance.CUSTOMERs.Where(p => p.PhoneNumber == PhoneNumber && p.Email == Email && p.NameCustomer == Name && p.Birthday == date && p.IdCard == IdCard).Count() > 0)
            {
                SeenEmail.Instance.SeenOTPForgetPass(Email, Name, PhoneNumber);
                CustomerDAO.Instance.SetCustomer(PhoneNumber);
                return true;
            }
            return false;
        }
    }
}