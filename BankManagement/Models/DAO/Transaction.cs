using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankManagement.Models.DAO
{
    public class Transaction
    {
        public string ID { get; set; }
        public string STKChuyen { get; set; }
        public string STKNhan { get; set; }
        public long SoTien { get; set; }
        public string NoiDung { get; set; }
        public DateTime NgayChuyen { get; set; }
    }
}