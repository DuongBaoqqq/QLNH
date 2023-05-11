namespace BankManagement.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ACCOUNT_NUMBER_CUSTOMER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ACCOUNT_NUMBER_CUSTOMER()
        {
            ACCOUNT_LOCK_HISTORY = new HashSet<ACCOUNT_LOCK_HISTORY>();
            CUSTOMER_TRANSACTION_HISTORY = new HashSet<CUSTOMER_TRANSACTION_HISTORY>();
            BANK_RECEIVING_TRANSFER = new HashSet<BANK_RECEIVING_TRANSFER>();
            CUSTOMER_TRANSACTION_HISTORY1 = new HashSet<CUSTOMER_TRANSACTION_HISTORY>();
            BANK_TRANSFER_HISTORY = new HashSet<BANK_TRANSFER_HISTORY>();
            SAVINGs = new HashSet<SAVING>();
        }

        [Key]
        [StringLength(50)]
        public string AccountNumber { get; set; }

        public long? AccountBalance { get; set; }

        public DateTime? AccountIssueDate { get; set; }

        public int? ID { get; set; }

        public bool? StateAccount { get; set; }

        public bool? MainAccount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACCOUNT_LOCK_HISTORY> ACCOUNT_LOCK_HISTORY { get; set; }

        public virtual CUSTOMER CUSTOMER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CUSTOMER_TRANSACTION_HISTORY> CUSTOMER_TRANSACTION_HISTORY { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BANK_RECEIVING_TRANSFER> BANK_RECEIVING_TRANSFER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CUSTOMER_TRANSACTION_HISTORY> CUSTOMER_TRANSACTION_HISTORY1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BANK_TRANSFER_HISTORY> BANK_TRANSFER_HISTORY { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SAVING> SAVINGs { get; set; }
    }
}
