using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankManagement.Models.EF;
using BankManagement.Models.LIB;
namespace BankManagement.Models.DAO
{
    public class CustomerTransfersDAO
    {
        public CUSTOMER_TRANSACTION_HISTORY data;
        private static CustomerTransfersDAO _Instance;
        public static CustomerTransfersDAO Instance
        {
            get { return _Instance ?? (_Instance = new CustomerTransfersDAO()); }
            set { }
        }
        private CustomerTransfersDAO()
        {
            data = new CUSTOMER_TRANSACTION_HISTORY();
        }
        public void SetAccountNumber(string AccountNumberTransfers, string AccountNumberReceiving)
        {
            data.AccountNumberTransfers = AccountNumberTransfers;
            data.AccountNumberReceiving = AccountNumberReceiving;
        }
        public void SetDataCustomerTransfers(string Money,string Content)
        {
            data.AmountOfMoney = Function.ConvertMoneyToLong(Money);
            data.Content = Content;
            data.DateTrading = DateTime.Now;
        }
        public void Transfer()
        {
            BankManagementDbContext.Instance.ACCOUNT_NUMBER_CUSTOMER.Find(data.AccountNumberTransfers).AccountBalance -= data.AmountOfMoney;
            BankManagementDbContext.Instance.ACCOUNT_NUMBER_CUSTOMER.Find(data.AccountNumberReceiving).AccountBalance += data.AmountOfMoney;
            data.ID = SetId();
            CUSTOMER_TRANSACTION_HISTORY a = new CUSTOMER_TRANSACTION_HISTORY
            {
                ID = data.ID,
                AmountOfMoney = data.AmountOfMoney,
                DateTrading = data.DateTrading,
                Content = data.Content,
                AccountNumberReceiving = data.AccountNumberReceiving,
                AccountNumberTransfers = data.AccountNumberTransfers,
            };
            BankManagementDbContext.Instance.CUSTOMER_TRANSACTION_HISTORY.Add(a);
            BankManagementDbContext.Instance.SaveChanges();
        }
        private string SetId()
        {
            string ID;
            if (BankManagementDbContext.Instance.CUSTOMER_TRANSACTION_HISTORY.Count() == 0)
            {
                ID = "100000";
            }
            else
            {
                var data = BankManagementDbContext.Instance.CUSTOMER_TRANSACTION_HISTORY.ToList();
                ID = (Convert.ToInt64(data[data.Count() - 1].ID.Remove(0, 1)) + 1).ToString();
            }
            return "A" + ID;
        }
    }
}