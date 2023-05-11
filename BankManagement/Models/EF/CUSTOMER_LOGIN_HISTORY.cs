namespace BankManagement.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CUSTOMER_LOGIN_HISTORY
    {
        [StringLength(50)]
        public string ID { get; set; }

        public int? IdCustomer { get; set; }

        public DateTime? DateLogin { get; set; }

        public virtual CUSTOMER CUSTOMER { get; set; }
    }
}
