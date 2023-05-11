using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using BankManagement.Models.EF;
using BankManagement.Models.LIB;
namespace BankManagement.Models.DAO
{
    public class StaffDAO
    {
        private static StaffDAO _Instance;
        public static StaffDAO Instance
        {
            get { return _Instance ?? (_Instance = new StaffDAO()); }
            set { }
        }
        public STAFF Staff { get; set; }
        public AGENCY Agency { get; set; }
        // Khởi tạo thông tin nhân viên và chi nhánh
        public void SetStaff(string ID)
        {
            Staff = BankManagementDbContext.Instance.STAFFs.Find(ID);
            Agency = BankManagementDbContext.Instance.AGENCies.Find(Staff.IdAgency);
        }
        // Lấy số lượng khách hàng trong chi nhánh
        public int GetCountCustomerOfAgency()
        {
            return BankManagementDbContext.Instance.CUSTOMERs.Where(p => p.IdAgency == Agency.ID).Count();
        }
        // Lấy số lượng nhân viên trong chi nhánh
        public int GetCountStaffOfAgency()
        {
            return BankManagementDbContext.Instance.STAFFs.Where(p => p.IdAgency == Agency.ID).Count();
        }
        // Lấy số dư trong chi nhánh
        public string GetAccountBalance()
        {
            return Function.ConvertLongToMoney(Convert.ToInt64(BankManagementDbContext.Instance.ACCOUNT_NUMBER_BANK.Where(p => p.ID == Agency.ID).SingleOrDefault().AccountBalance));
        }
        // Lấy tiền mặt trong chi nhánh
        public string GetCash()
        {
            return Function.ConvertLongToMoney(Convert.ToInt64(BankManagementDbContext.Instance.ACCOUNT_NUMBER_BANK.Where(p => p.ID == Agency.ID).SingleOrDefault().Cash));
        }

        // kiểm tra thông tin cá nhân của khách hàng nếu hợp lệ lưu vào database
        public bool CheckInfoCustomer(string Name, string CMND, string NS, string PhoneNumber, string Tinh, string Huyen, string DiaChi, string Email, int Gender)
        {
            bool check = true;
            if (BankManagementDbContext.Instance.CUSTOMERs.Where(p => p.PhoneNumber.Trim() == PhoneNumber || p.Email.Trim() == Email || p.IdCard.Trim() == CMND).Count() > 0)
            {
                check = false;
            }
            if (check == true)
            {
                CUSTOMER customer = new CUSTOMER
                {
                    NameCustomer = Name,
                    IdCard = CMND,
                    Email = Email,
                    AddressCustomer = DiaChi,
                    Gender = Convert.ToBoolean(Gender),
                    ID_District = BankManagementDbContext.Instance.DISTRICTs.Where(p => p.NameDistrict == Huyen).SingleOrDefault().ID_District,
                    ID_Province = BankManagementDbContext.Instance.PROVINCEs.Where(p => p.NameProvince == Tinh).SingleOrDefault().ID,
                    Birthday = Convert.ToDateTime(NS),
                    PhoneNumber = PhoneNumber,
                    IdAgency = Agency.ID,
                    ID = CreateIdForCustomer(),
                    State = true,
                };
                BankManagementDbContext.Instance.CUSTOMERs.Add(customer);
                BankManagementDbContext.Instance.SaveChanges();
                CreateAccountCustomer(customer.ID, Email, Name, PhoneNumber);
                CreateAccountNumberCustomer(customer.ID);
            }
            return check;
        }
        // Tạo ID khách hàng
        private int CreateIdForCustomer()
        {
            int ID = 1;
            ID += BankManagementDbContext.Instance.CUSTOMERs.Count();
            return ID;
        }
        // Tạo tài khoản đăng nhập cho khách hàng
        private void CreateAccountCustomer(int ID, string Email, string Name, string PhoneNumber)
        {
            string Pass = Function.NewPass();
            string Pincode = Function.OTPForgetPass();
            ACCOUNT_CUSTOMER ac = new ACCOUNT_CUSTOMER
            {
                ID = ID,
                Pass = Pass,
                PinCode = Pincode,
            };
            BankManagementDbContext.Instance.ACCOUNT_CUSTOMER.Add(ac);
            BankManagementDbContext.Instance.SaveChanges();
            SeenEmail.Instance.SeenNewCreateAccount(Email, Name, PhoneNumber, Pass, Pincode);
        }
        // Tạo số tài khoản cho khách hàng
        private void CreateAccountNumberCustomer(int ID)
        {
            ACCOUNT_NUMBER_CUSTOMER ac = new ACCOUNT_NUMBER_CUSTOMER
            {
                AccountNumber = CreateAccountNumber(),
                AccountBalance = 0,
                AccountIssueDate = DateTime.Now,
                ID = ID,
                StateAccount = true,
                MainAccount = true,
            };
            BankManagementDbContext.Instance.ACCOUNT_NUMBER_CUSTOMER.Add(ac);
            BankManagementDbContext.Instance.SaveChanges();
        }
        // Tạo số tài khoản 
        private string CreateAccountNumber()
        {
            long Number = 56010001585849;
            Number += BankManagementDbContext.Instance.ACCOUNT_NUMBER_CUSTOMER.Where(p => p.MainAccount == true).Count();
            while (CheckAccountNumberCustomer(Number.ToString()))
            {
                Number++;
            }
            return Number.ToString();

        }
        // kiểm tra số tài khoản có tồn tại
        private bool CheckAccountNumberCustomer(string AccountNumber)
        {
            if (BankManagementDbContext.Instance.ACCOUNT_NUMBER_CUSTOMER.Where(p => p.AccountNumber == AccountNumber).Count() > 0)
                return true;
            return false;
        }
        // Lấy chuỗi tên chi nhánh
        public dynamic ListNameCN()
        {
            return BankManagementDbContext.Instance.AGENCies.Select(p => p.NameAgency).ToList();
        }
        // Lấy thông tin khách hàng bằng số điện thoại
        public dynamic GetCustomerByPhoneNumber(string PhoneNumber)
        {
            BankManagementDbContext db = new BankManagementDbContext();
            bool proxyCreation = db.Configuration.ProxyCreationEnabled;
            db.Configuration.ProxyCreationEnabled = false;
            return db.CUSTOMERs.Where(p => p.PhoneNumber == PhoneNumber).SingleOrDefault();
        }
        // Lấy tên tỉnh thành khi biết Id tỉnh
        public string GetNameProvinceByIdProvince(int ID)
        {
            BankManagementDbContext db = new BankManagementDbContext();
            bool proxyCreation = db.Configuration.ProxyCreationEnabled;
            db.Configuration.ProxyCreationEnabled = false;
            return db.PROVINCEs.Find(ID).NameProvince;
        }
        // Lấy tên quận huyện khi biết id tỉnh và id huyện
        public string GetNameDistrictById(int IdProvince,int IdDistrict)
        {
            BankManagementDbContext db = new BankManagementDbContext();
            bool proxyCreation = db.Configuration.ProxyCreationEnabled;
            db.Configuration.ProxyCreationEnabled = false;
            return db.DISTRICTs.Where(p => p.ID_District == IdDistrict && p.ID_Province == IdProvince).SingleOrDefault().NameDistrict;
        }
        // Lấy tên chi nhánh của khách hàng
        public string GetNameAgencyByID(string ID)
        {
            BankManagementDbContext db = new BankManagementDbContext();
            bool proxyCreation = db.Configuration.ProxyCreationEnabled;
            db.Configuration.ProxyCreationEnabled = false;
            return db.AGENCies.Find(ID).NameAgency;
        }
        // lấy danh sách tài khoản bằng số điện thoại
        public dynamic GetListAccountNumberCustomer(string PhoneNumber)
        {
            BankManagementDbContext db = new BankManagementDbContext();
            bool proxyCreation = db.Configuration.ProxyCreationEnabled;
            db.Configuration.ProxyCreationEnabled = false;
            return db.ACCOUNT_NUMBER_CUSTOMER.Where(p => p.CUSTOMER.PhoneNumber == PhoneNumber).ToList();
        }
        public dynamic GetListCustomer(string Name, string IdCard, string Email, string PhoneNumber, string NameProvince, string NameDistrict, string NameAgency)
        {
            BankManagementDbContext db = new BankManagementDbContext();
            bool proxyCreation = db.Configuration.ProxyCreationEnabled;
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.CUSTOMERs.ToList();
            if (Name != "")
            {
                for (int i = data.Count - 1; i >= 0; i--)
                {
                    if (!data[i].NameCustomer.ToUpper().Contains(Name.ToUpper()))
                    {
                        data.RemoveAt(i);
                    }
                }
            }
            if (IdCard != "")
            {
                for (int i = data.Count - 1; i >= 0; i--)
                {
                    if (!data[i].IdCard.Contains(IdCard))
                    {
                        data.RemoveAt(i);
                    }
                }
            }
            if (Email != "")
            {
                for (int i = data.Count - 1; i >= 0; i--)
                {
                    if (!data[i].Email.Contains(Email))
                    {
                        data.RemoveAt(i);
                    }
                }
            }
            if (PhoneNumber != "")
            {
                for (int i = data.Count - 1; i >= 0; i--)
                {
                    if (!data[i].PhoneNumber.Contains(PhoneNumber))
                    {
                        data.RemoveAt(i);
                    }
                }
            }
            if (NameProvince.Trim() != "Tỉnh/Thành phố")
            {
                for (int i = data.Count - 1; i >= 0; i--)
                {
                    if (BankManagementDbContext.Instance.PROVINCEs.Where(p => p.NameProvince == NameProvince).SingleOrDefault().ID != data[i].ID_Province)
                    {
                        data.RemoveAt(i);
                    }
                }
            }
            if (NameDistrict.Trim() != "Quận/Huyện")
            {
                for (int i = data.Count - 1; i >= 0; i--)
                {
                    if (BankManagementDbContext.Instance.DISTRICTs.Where(p => p.NameDistrict == NameDistrict).SingleOrDefault().ID_District != data[i].ID_District
                        )
                    {
                        data.RemoveAt(i);
                    }
                }
            }
            if (NameAgency.Trim() != "Chi nhánh")
            {
                for (int i = data.Count - 1; i >= 0; i--)
                {
                    if (BankManagementDbContext.Instance.AGENCies.Where(p => p.NameAgency == NameAgency).SingleOrDefault().ID != data[i].IdAgency)
                    {
                        data.RemoveAt(i);
                    }
                }
            }
            return data;
        }

        // Ngân hàng nhận tiền mặt và chuyển khoản cho số tài khoản nhận
        public void CustomerReceiveMoney(string Money,string AccountNumber)
        {
            long money = Function.ConvertMoneyToLong(Money);
            BankManagementDbContext.Instance.ACCOUNT_NUMBER_CUSTOMER.Find(AccountNumber).AccountBalance += money;
            BankManagementDbContext.Instance.ACCOUNT_NUMBER_BANK.Where(p => p.ID == Staff.IdAgency).SingleOrDefault().AccountBalance -= money;
            BankManagementDbContext.Instance.ACCOUNT_NUMBER_BANK.Where(p => p.ID == Staff.IdAgency).SingleOrDefault().Cash += money;
            BankManagementDbContext.Instance.SaveChanges();
        }
        // lưu lịch sử ngân hàng chuyển tiền
        public void MoneyTransferBank(string AccountNumber,string Money,string content)
        {
            BANK_TRANSFER_HISTORY data = new BANK_TRANSFER_HISTORY
            {
                ID = CreateIdBankTransferHistory(),
                AccountNumberTransfers = BankManagementDbContext.Instance.ACCOUNT_NUMBER_BANK.Where(p => p.ID == Staff.IdAgency).SingleOrDefault().AccountNumber,
                AccountNumberReceiving = AccountNumber,
                Content = content,
                DateTrading = DateTime.Now,
                AmountOfMoney = Function.ConvertMoneyToLong(Money),
            };
            BankManagementDbContext.Instance.BANK_TRANSFER_HISTORY.Add(data);
            BankManagementDbContext.Instance.SaveChanges();
        }
        // lưu lịch sử ngân hàng nhân tiền mặt
        public void CashReceivingBank(string Money)
        {
            long money = Function.ConvertMoneyToLong(Money);
            CASH_RECEIVING_BANK data = new CASH_RECEIVING_BANK
            {
                AccountNumberReceiving = BankManagementDbContext.Instance.ACCOUNT_NUMBER_BANK.Where(p => p.ID == Staff.IdAgency).SingleOrDefault().AccountNumber,
                AmountOfMoney = money,
                Content = "Tiền chuyển khoản",
                DateTrading = DateTime.Now,
                ID = CreateIdCashReceivingBank(),
            };
            BankManagementDbContext.Instance.CASH_RECEIVING_BANK.Add(data);
            BankManagementDbContext.Instance.SaveChanges();
        }
        // tạo id ngân hàng nhận tiền mặt
        private string CreateIdCashReceivingBank()
        {
            string ID;
            if (BankManagementDbContext.Instance.CASH_RECEIVING_BANK.Count() == 0)
            {
                ID = "100000";
            }
            else
            {
                var data = BankManagementDbContext.Instance.CASH_RECEIVING_BANK.ToList();
                ID = (Convert.ToInt64(data[data.Count() - 1].ID.Remove(0, 1)) + 1).ToString();
            }
            return "F" + ID;
        }
        // tạo id ngân hàng nhận tiền mặt
        private string CreateIdBankTransferHistory()
        {
            string ID;
            if (BankManagementDbContext.Instance.BANK_TRANSFER_HISTORY.Count() == 0)
            {
                ID = "100000";
            }
            else
            {
                var data = BankManagementDbContext.Instance.BANK_TRANSFER_HISTORY.ToList();
                ID = (Convert.ToInt64(data[data.Count() - 1].ID.Remove(0, 1)) + 1).ToString();
            }
            return "C" + ID;
        }
    }
}