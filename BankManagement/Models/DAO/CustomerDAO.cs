using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankManagement.Models.EF;
using BankManagement.Models.LIB;
namespace BankManagement.Models.DAO
{
    public class CustomerDAO
    {
        public CUSTOMER customer { get; set; }
        private static CustomerDAO _Instance;
        public string STK { get; set; }
        public static CustomerDAO Instance
        {
            get { return _Instance ?? (_Instance = new CustomerDAO()); }
            set { }
        }
        // Lấy số tài khoản chính
        public ACCOUNT_NUMBER_CUSTOMER GetMainCustomer()
        {
            return BankManagementDbContext.Instance.ACCOUNT_NUMBER_CUSTOMER.Where(p => p.ID == customer.ID && p.MainAccount == true).SingleOrDefault();
        }
        public static string ConvertMoney(long? money)
        {
            return Function.ConvertLongToMoney(Convert.ToInt64(money));
        }
        public void SetCustomer(string PhoneNumber)
        {
            customer = BankManagementDbContext.Instance.CUSTOMERs.Where(p => p.PhoneNumber == PhoneNumber).SingleOrDefault();
        }
        public int ChangeInfoCustomer(string Name,string IdCard,string PhoneNumber,string Address,string Birthday,string Email,bool Genger,string NameProvince,string NameDistrict)
        {   
            int IdDistrict = BankManagementDbContext.Instance.DISTRICTs.Where(p => p.NameDistrict.Trim() == NameDistrict).SingleOrDefault().ID_District;
            int IdProvince = BankManagementDbContext.Instance.PROVINCEs.Where(p => p.NameProvince.Trim() == NameProvince).SingleOrDefault().ID;
            if (customer.NameCustomer.Trim() == Name && customer.IdCard.Trim() == IdCard && customer.PhoneNumber.Trim() == PhoneNumber && customer.AddressCustomer.Trim() == Address && customer.Birthday == Convert.ToDateTime(Birthday) && customer.Email.Trim() == Email && customer.Gender == Genger && customer.ID_Province == IdProvince && customer.ID_District == IdDistrict)
            {
                return -1;
            }
            else if (BankManagementDbContext.Instance.CUSTOMERs.Where(p => (p.Email == Email && Email!=customer.Email)||( p.PhoneNumber == PhoneNumber && PhoneNumber != customer.PhoneNumber) || (p.IdCard == IdCard  && IdCard != customer.IdCard)).Count() > 0)
            {
                return 0;
            }
            else
            {
                customer.NameCustomer = Name;
                customer.IdCard = IdCard;
                customer.PhoneNumber = PhoneNumber;
                customer.Birthday = Convert.ToDateTime(Birthday);
                customer.Email = Email;
                customer.Gender = Genger;
                customer.AddressCustomer = Address;
                customer.ID_Province = IdProvince;
                customer.ID_District = IdDistrict;
                customer.State = true;
                BankManagementDbContext.Instance.SaveChanges();
                return 1;
            }
            
        }
        public static string GetNameCustomerByAccountNumberCustomer(string AccountNumberCustomer)
        {
            BankManagementDbContext db = new BankManagementDbContext();
            bool proxyCreation = db.Configuration.ProxyCreationEnabled;
            db.Configuration.ProxyCreationEnabled = false;

            if (db.ACCOUNT_NUMBER_CUSTOMER.Where(p => p.AccountNumber == AccountNumberCustomer).Count() > 0)
            {
                int ID = Convert.ToInt32(db.ACCOUNT_NUMBER_CUSTOMER.Where(p => p.AccountNumber == AccountNumberCustomer).SingleOrDefault().ID);
                return db.CUSTOMERs.Find(ID).NameCustomer;
            }
            else
            {
                return "Error";
            }
        }
        public bool CheckMoney(string Money)
        {
            long money = Function.ConvertMoneyToLong(Money);
            if (money <= GetMainCustomer().AccountBalance)
                return true;
            return false;
        }
        public string Content()
        {
            return customer.NameCustomer.Trim() + " Chuyen tien";
        } 
        public string AccountIssueDate()
        {
            DateTime date = Convert.ToDateTime(GetMainCustomer().AccountIssueDate);
            return date.Day + "/" + date.Month + "/" + date.Year;
        }
        public string StateAccountNumber()
        {
            if (Convert.ToBoolean(GetMainCustomer().StateAccount))
                return "Đang hoạt động";
            return "Đã khóa";
        }
        public void SaveLock(string reason)
        {
            ACCOUNT_LOCK_HISTORY data = new ACCOUNT_LOCK_HISTORY 
            {
                ID = CreateIDLichSuKhoaThe(),
                Reason = reason,
                DateLock = DateTime.Now,
                AccountNumber = GetMainCustomer().AccountNumber,
            };
            BankManagementDbContext.Instance.ACCOUNT_LOCK_HISTORY.Add(data);
            BankManagementDbContext.Instance.ACCOUNT_NUMBER_CUSTOMER.Find(GetMainCustomer().AccountNumber).StateAccount = false;
            BankManagementDbContext.Instance.SaveChanges();
        }
        public void SaveUnlock()
        {
            string AccountNumber = GetMainCustomer().AccountNumber;
            BankManagementDbContext.Instance.ACCOUNT_LOCK_HISTORY.Where(p => p.AccountNumber == AccountNumber && p.DateUnlock == null).SingleOrDefault().DateUnlock = DateTime.Now;
            BankManagementDbContext.Instance.ACCOUNT_NUMBER_CUSTOMER.Find(GetMainCustomer().AccountNumber).StateAccount = true;
            BankManagementDbContext.Instance.SaveChanges();
        }
        private string CreateIDLichSuKhoaThe()
        {
            string ID;
            if (BankManagementDbContext.Instance.ACCOUNT_LOCK_HISTORY.Count() == 0)
            {
                ID = "100000";
            }
            else
            {
                var data = BankManagementDbContext.Instance.ACCOUNT_LOCK_HISTORY.ToList();
                ID = (Convert.ToInt64(data[data.Count() - 1].ID) + 1).ToString();
            }
            return ID;
        }

        public void ChangeMainSTK(string STK)
        {
            var data = BankManagementDbContext.Instance.ACCOUNT_NUMBER_CUSTOMER.Where(p => p.ID == customer.ID).ToList();
            foreach(var i in data)
            {
                i.MainAccount = false;
            }
            BankManagementDbContext.Instance.ACCOUNT_NUMBER_CUSTOMER.Find(STK).MainAccount = true;
            BankManagementDbContext.Instance.SaveChanges();
        }
        public bool CheckSTK(string STK)
        {
            if (BankManagementDbContext.Instance.ACCOUNT_NUMBER_CUSTOMER.Where(p => p.AccountNumber == STK).Count() > 0)
                return false;
            return true;
        }
        public void AddSTK(string STK)
        {
            var data = BankManagementDbContext.Instance.ACCOUNT_NUMBER_CUSTOMER.Where(p => p.ID == customer.ID && p.MainAccount == true).SingleOrDefault();
            var AccountNumberBank = BankManagementDbContext.Instance.ACCOUNT_NUMBER_BANK.Where(p => p.ID == customer.IdAgency).SingleOrDefault();
            data.AccountBalance -= 5000000;
            AccountNumberBank.AccountBalance += 5000000;
            ACCOUNT_NUMBER_CUSTOMER ac = new ACCOUNT_NUMBER_CUSTOMER
            {
                ID = customer.ID,
                AccountIssueDate = DateTime.Now,
                AccountBalance = 0,
                AccountNumber = STK,
                MainAccount = false,
                StateAccount = true,
            };
            this.STK = STK;
            BankManagementDbContext.Instance.ACCOUNT_NUMBER_CUSTOMER.Add(ac);
            BANK_RECEIVING_TRANSFER brt = new BANK_RECEIVING_TRANSFER
            {
                ID = SetId(),
                AccountNumberTransfers = data.AccountNumber,
                AccountNumberReceiving = AccountNumberBank.AccountNumber,
                Content = "Mua tài khoản như ý",
                AmountOfMoney = 5000000,
                DateTrading = DateTime.Now,
            };
            BankManagementDbContext.Instance.BANK_RECEIVING_TRANSFER.Add(brt);
            BankManagementDbContext.Instance.SaveChanges();
        }
        // Tạo số tài khoản 
        public static string SetId()
        {
            string ID;
            if (BankManagementDbContext.Instance.BANK_RECEIVING_TRANSFER.Count() == 0)
            {
                ID = "100000";
            }
            else
            {
                var data = BankManagementDbContext.Instance.BANK_RECEIVING_TRANSFER.ToList();
                ID = (Convert.ToInt64(data[data.Count() - 1].ID.Remove(0, 1)) + 1).ToString();
            }
            return "B" + ID;
        }
        public ACCOUNT_NUMBER_CUSTOMER GetLastAccountNumberCustomer()
        {    
           return BankManagementDbContext.Instance.ACCOUNT_NUMBER_CUSTOMER.Find(STK);
        }
        public List<INTEREST_RATE> GetListInterestRate()
        {
            return BankManagementDbContext.Instance.INTEREST_RATE.ToList();
        }
        public int GetCountAccountNumber()
        {
            return BankManagementDbContext.Instance.ACCOUNT_NUMBER_CUSTOMER.Where(p => p.ID == customer.ID).Count();
        }
        public static string GetDayIssue(string STK)
        {
            DateTime date = Convert.ToDateTime(BankManagementDbContext.Instance.ACCOUNT_NUMBER_CUSTOMER.Find(STK).AccountIssueDate);
            return date.Day + "/" + date.Month + "/" + date.Year;
        }
        public static string GetMoney(string STK)
        {
            return Function.money2(Convert.ToInt64(BankManagementDbContext.Instance.ACCOUNT_NUMBER_CUSTOMER.Find(STK).AccountBalance));
        }
        public static string GetNameAgencyByID(string ID)
        {
            return BankManagementDbContext.Instance.AGENCies.Find(ID).NameAgency;
        }
        public dynamic GetListAccountNumberCustomer()
        {
            BankManagementDbContext db = new BankManagementDbContext();
            bool proxyCreation = db.Configuration.ProxyCreationEnabled;
            db.Configuration.ProxyCreationEnabled = false;
            return db.ACCOUNT_NUMBER_CUSTOMER.Where(p => p.CUSTOMER.PhoneNumber == customer.PhoneNumber).ToList();
        }
        public string NotifyReturnPass()
        {
            return "Quý khách đã được cấp lại mật khẩu thành công. Tên đăng nhập " + customer.PhoneNumber + ", Email nhận OTP " + customer.Email + ". Mật khẩu đăng nhập đã được gửi tới Email đăng ký dịch vụ của Quý khách.";
        }
        public void SaveRandomPassAndSeenEmail()
        {
            string Password = Function.NewPass();
            BankManagementDbContext.Instance.ACCOUNT_CUSTOMER.Find(customer.ID).Pass = Password;
            SeenEmail.Instance.SeenNewPassword(customer.Email, customer.NameCustomer, customer.PhoneNumber, Password);
            BankManagementDbContext.Instance.SaveChanges();
        }
        public bool CheckPass(string Password)
        {
            if (Password == BankManagementDbContext.Instance.ACCOUNT_CUSTOMER.Find(customer.ID).Pass.Trim())
            {
                return true;
            }
            return false;
        }
        public void ChangeNewPassword(string Password)
        {
            BankManagementDbContext.Instance.ACCOUNT_CUSTOMER.Find(customer.ID).Pass = Password;
            BankManagementDbContext.Instance.SaveChanges();
        }
        public int GetCountListSavingAccount()
        {
            string AccountNumber = GetMainCustomer().AccountNumber;
            return BankManagementDbContext.Instance.SAVINGs.Where(p => p.AccountNumber == AccountNumber).Count();
        }
        public List<string> GetDataSavingAccount(int index)
        {
            string AccountNumber = GetMainCustomer().AccountNumber;
            List<string> data = new List<string>();
            var ListSavingAccount = BankManagementDbContext.Instance.SAVINGs.Where(p => p.AccountNumber == AccountNumber).ToList();
            data.Add(ListSavingAccount[index].ID);
            data.Add(ConvertMoney(ListSavingAccount[index].AmountOfMoney));
            DateTime a = Convert.ToDateTime(ListSavingAccount[index].DateTrading);
            string date = a.Day + "/" + a.Month + "/" + a.Year;
            data.Add(date);
            data.Add(BankManagementDbContext.Instance.INTEREST_RATE.Find(ListSavingAccount[index].IdInterestRate).Term);
            data.Add(BankManagementDbContext.Instance.INTEREST_RATE.Find(ListSavingAccount[index].IdInterestRate).InterestRate.ToString() + "%/năm");
            return data;
        }
        public List<SAVING> GetListSavingAccount()
        {
            string AccountNumber = GetMainCustomer().AccountNumber;
            return BankManagementDbContext.Instance.SAVINGs.Where(p => p.AccountNumber == AccountNumber && p.StateSaving == false).ToList();
        }
        public static double GetInterestRateByTerm(string Term)
        {
            BankManagementDbContext db = new BankManagementDbContext();
            bool proxyCreation = db.Configuration.ProxyCreationEnabled;
            db.Configuration.ProxyCreationEnabled = false;
            return Convert.ToDouble(db.INTEREST_RATE.Where(p => p.Term.Trim() == Term.Trim()).SingleOrDefault().InterestRate);
        }
        public static string CalculateInterestRate(string money, string term)
        {
            string kh = "";
            for (int i = 0; i < term.Length; i++)
            {
                if (term[i] == ' ')
                {
                    break;
                }
                else
                {
                    kh += term[i];
                }
            }
            return Function.CalculateInterestRate(money, GetInterestRateByTerm(term), Convert.ToInt32(kh));
        }
    }
}