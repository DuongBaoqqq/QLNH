using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankManagement.Models.EF;
namespace BankManagement.Models.DAO
{
    public class ListTransactionDAO
    {
        public string stk { get; set; }
        public List<Transaction> ls;
        private static ListTransactionDAO _Instance;
        public static ListTransactionDAO Instance
        {
            get { return _Instance ?? (_Instance = new ListTransactionDAO()); }
            set { }
        }
        public void SetSTK(string stk)
        {
            ls = new List<Transaction>();
            this.stk = stk;
            LoadListKHGD();
            LoadListNHCT();
            LoadListNHNT();
            ls.Sort((x, y) => DateTime.Compare(y.NgayChuyen, x.NgayChuyen));
        }
        private ListTransactionDAO()
        {
            ls = new List<Transaction>();
        }
        private void LoadListKHGD()
        {
            var data = BankManagementDbContext.Instance.CUSTOMER_TRANSACTION_HISTORY.ToList();
            SetData(data);
        }
        private void LoadListNHNT()
        {
            Transaction htr = new Transaction();
            var data = BankManagementDbContext.Instance.BANK_RECEIVING_TRANSFER.ToList();
            SetData(data);

        }
        private void LoadListNHCT()
        {
            Transaction htr = new Transaction();
            var data = BankManagementDbContext.Instance.BANK_TRANSFER_HISTORY.ToList();
            SetData(data);
        }
        private void SetData(dynamic data)
        {
            foreach (var item in data)
            {
                if (stk.Trim() == item.AccountNumberTransfers.Trim() || stk.Trim() == item.AccountNumberReceiving.Trim())
                {
                    Transaction htr = new Transaction();
                    htr.ID = item.ID;
                    htr.STKNhan = item.AccountNumberReceiving;
                    htr.STKChuyen = item.AccountNumberTransfers;
                    htr.NgayChuyen = Convert.ToDateTime(item.DateTrading);
                    htr.NoiDung = item.Content;
                    htr.SoTien = Convert.ToInt64(item.AmountOfMoney);
                    ls.Add(htr);
                }
            }
        }

    }
}