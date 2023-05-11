namespace BankManagement.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GET_SAVING
    {
        [StringLength(50)]
        public string ID { get; set; }

        [StringLength(50)]
        public string AccountNumber { get; set; }

        public DateTime? DateReceived { get; set; }

        [StringLength(50)]
        public string IdSaving { get; set; }

        public virtual ACCOUNT_NUMBER_BANK ACCOUNT_NUMBER_BANK { get; set; }

        public virtual SAVING SAVING { get; set; }
    }
}
