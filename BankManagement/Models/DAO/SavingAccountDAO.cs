using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankManagement.Models.EF;
using BankManagement.Models.LIB;
namespace BankManagement.Models.DAO
{
    public class SavingAccountDAO
    {
        public SAVING ls;
        private static SavingAccountDAO _Instance;
        public static SavingAccountDAO Instance
        {
            get { return _Instance ?? (_Instance = new SavingAccountDAO()); }
            set { }
        }
        private SavingAccountDAO()
        {
            ls = new SAVING();
        }
        public string Money()
        {
            return Function.ConvertLongToMoney(Convert.ToInt64(ls.AmountOfMoney));
        }
        public void CreateSavingAccount(string Money, string Term)
        {
            ls.IdInterestRate = BankManagementDbContext.Instance.INTEREST_RATE.Where(p => p.Term == Term).SingleOrDefault().ID;
            string AccountNumber = CustomerDAO.Instance.GetMainCustomer().AccountNumber;
            ls.AccountNumber = AccountNumber;
            ls.AmountOfMoney = Function.ConvertMoneyToLong(Money);
            ls.DateTrading = DateTime.Now;
            ls.StateSaving = false;
        }
        public INTEREST_RATE InterestRate()
        {
            return BankManagementDbContext.Instance.INTEREST_RATE.Find(ls.IdInterestRate);
        }
        public void SaveLS()
        {
            ls.ID = CreateIDSavingAccount();
            SAVING data = new SAVING
            {
                ID = ls.ID,
                AccountNumber = ls.AccountNumber,
                DateTrading = ls.DateTrading,
                IdInterestRate = ls.IdInterestRate,
                StateSaving = ls.StateSaving,
                AmountOfMoney = ls.AmountOfMoney
            };
            BankManagementDbContext.Instance.SAVINGs.Add(data);
            BankManagementDbContext.Instance.ACCOUNT_NUMBER_CUSTOMER.Find(data.AccountNumber).AccountBalance -= ls.AmountOfMoney;
            BankManagementDbContext.Instance.ACCOUNT_NUMBER_BANK.Where(p => p.ID == CustomerDAO.Instance.customer.IdAgency).SingleOrDefault().AccountBalance += ls.AmountOfMoney;
            string AccountNumberBank = BankManagementDbContext.Instance.ACCOUNT_NUMBER_BANK.Where(p => p.ID == CustomerDAO.Instance.customer.IdAgency).SingleOrDefault().AccountNumber;
            BANK_RECEIVING_TRANSFER brt = new BANK_RECEIVING_TRANSFER
            {
                ID = CustomerDAO.SetId(),
                AccountNumberTransfers = data.AccountNumber,
                AccountNumberReceiving = AccountNumberBank,
                Content = "Tiền gửi tiết kiệm",
                AmountOfMoney = ls.AmountOfMoney,
                DateTrading = DateTime.Now,
            };
            BankManagementDbContext.Instance.BANK_RECEIVING_TRANSFER.Add(brt);
            BankManagementDbContext.Instance.SaveChanges();
        }
        private static string CreateIDSavingAccount()
        {
            string ID;
            if (BankManagementDbContext.Instance.SAVINGs.Count() == 0)
            {
                ID = "100000";
            }
            else
            {
                var data = BankManagementDbContext.Instance.SAVINGs.ToList();
                ID = (Convert.ToInt64((data[data.Count() - 1].ID)) + 1).ToString();
            }
            return ID;
        }
    }
}