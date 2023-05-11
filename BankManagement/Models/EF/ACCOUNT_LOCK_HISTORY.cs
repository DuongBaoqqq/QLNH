namespace BankManagement.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ACCOUNT_LOCK_HISTORY
    {
        [StringLength(50)]
        public string ID { get; set; }

        [StringLength(50)]
        public string AccountNumber { get; set; }

        public DateTime? DateLock { get; set; }

        public DateTime? DateUnlock { get; set; }

        [StringLength(50)]
        public string Reason { get; set; }

        public virtual ACCOUNT_NUMBER_CUSTOMER ACCOUNT_NUMBER_CUSTOMER { get; set; }
    }
}
