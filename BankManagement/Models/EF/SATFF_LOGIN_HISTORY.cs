namespace BankManagement.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SATFF_LOGIN_HISTORY
    {
        [StringLength(50)]
        public string ID { get; set; }

        [StringLength(50)]
        public string IdStaff { get; set; }

        public DateTime? DateLogin { get; set; }

        public virtual ACCOUNT_STAFF ACCOUNT_STAFF { get; set; }
    }
}
