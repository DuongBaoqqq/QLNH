namespace BankManagement.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BANK_TRANSFER_HISTORY
    {
        [StringLength(50)]
        public string ID { get; set; }

        [StringLength(50)]
        public string AccountNumberTransfers { get; set; }

        [StringLength(50)]
        public string AccountNumberReceiving { get; set; }

        public long? AmountOfMoney { get; set; }

        [StringLength(50)]
        public string Content { get; set; }

        public DateTime? DateTrading { get; set; }

        public virtual ACCOUNT_NUMBER_BANK ACCOUNT_NUMBER_BANK { get; set; }

        public virtual ACCOUNT_NUMBER_CUSTOMER ACCOUNT_NUMBER_CUSTOMER { get; set; }
    }
}
