namespace BankManagement.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AGENCY")]
    public partial class AGENCY
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AGENCY()
        {
            ACCOUNT_NUMBER_BANK = new HashSet<ACCOUNT_NUMBER_BANK>();
            CUSTOMERs = new HashSet<CUSTOMER>();
            STAFFs = new HashSet<STAFF>();
        }

        [StringLength(50)]
        public string ID { get; set; }

        [StringLength(50)]
        public string NameAgency { get; set; }

        [StringLength(50)]
        public string AddressAgenCy { get; set; }

        public int? ID_District { get; set; }

        public int? ID_Province { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACCOUNT_NUMBER_BANK> ACCOUNT_NUMBER_BANK { get; set; }

        public virtual DISTRICT DISTRICT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CUSTOMER> CUSTOMERs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<STAFF> STAFFs { get; set; }
    }
}
