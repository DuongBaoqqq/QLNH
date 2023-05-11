namespace BankManagement.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CUSTOMER")]
    public partial class CUSTOMER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CUSTOMER()
        {
            ACCOUNT_NUMBER_CUSTOMER = new HashSet<ACCOUNT_NUMBER_CUSTOMER>();
            CUSTOMER_LOGIN_HISTORY = new HashSet<CUSTOMER_LOGIN_HISTORY>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(50)]
        public string NameCustomer { get; set; }

        [StringLength(50)]
        public string IdCard { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }

        public bool? Gender { get; set; }

        [StringLength(100)]
        public string AddressCustomer { get; set; }

        public int? ID_District { get; set; }

        public int? ID_Province { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string PhoneNumber { get; set; }

        [StringLength(50)]
        public string IdAgency { get; set; }

        public bool? State { get; set; }

        public virtual ACCOUNT_CUSTOMER ACCOUNT_CUSTOMER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACCOUNT_NUMBER_CUSTOMER> ACCOUNT_NUMBER_CUSTOMER { get; set; }

        public virtual AGENCY AGENCY { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CUSTOMER_LOGIN_HISTORY> CUSTOMER_LOGIN_HISTORY { get; set; }

        public virtual DISTRICT DISTRICT { get; set; }
    }
}
