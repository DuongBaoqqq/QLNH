namespace BankManagement.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SAVING")]
    public partial class SAVING
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SAVING()
        {
            GET_SAVING = new HashSet<GET_SAVING>();
        }

        [StringLength(50)]
        public string ID { get; set; }

        [StringLength(50)]
        public string AccountNumber { get; set; }

        public int? IdInterestRate { get; set; }

        public long? AmountOfMoney { get; set; }

        public DateTime? DateTrading { get; set; }

        public bool? StateSaving { get; set; }

        public virtual ACCOUNT_NUMBER_CUSTOMER ACCOUNT_NUMBER_CUSTOMER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GET_SAVING> GET_SAVING { get; set; }

        public virtual INTEREST_RATE INTEREST_RATE { get; set; }
    }
}
