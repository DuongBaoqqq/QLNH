namespace BankManagement.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ACCOUNT_NUMBER_BANK
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ACCOUNT_NUMBER_BANK()
        {
            BANK_TRANSFER_HISTORY = new HashSet<BANK_TRANSFER_HISTORY>();
            BANK_RECEIVING_TRANSFER = new HashSet<BANK_RECEIVING_TRANSFER>();
            CASH_RECEIVING_BANK = new HashSet<CASH_RECEIVING_BANK>();
            GET_SAVING = new HashSet<GET_SAVING>();
        }

        [StringLength(50)]
        public string ID { get; set; }

        [Key]
        [StringLength(50)]
        public string AccountNumber { get; set; }

        public long? AccountBalance { get; set; }

        public long? Cash { get; set; }

        public DateTime? AccountIssueDate { get; set; }

        public bool? StateAccount { get; set; }

        public virtual AGENCY AGENCY { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BANK_TRANSFER_HISTORY> BANK_TRANSFER_HISTORY { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BANK_RECEIVING_TRANSFER> BANK_RECEIVING_TRANSFER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CASH_RECEIVING_BANK> CASH_RECEIVING_BANK { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GET_SAVING> GET_SAVING { get; set; }
    }
}
