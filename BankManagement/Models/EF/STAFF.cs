namespace BankManagement.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("STAFF")]
    public partial class STAFF
    {
        [StringLength(50)]
        public string NameStaff { get; set; }

        [StringLength(50)]
        public string IdCard { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }

        public bool? Gender { get; set; }

        [StringLength(100)]
        public string AddressStaff { get; set; }

        public int? ID_District { get; set; }

        public int? ID_Province { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string IdAgency { get; set; }

        [StringLength(50)]
        public string ID { get; set; }

        [StringLength(50)]
        public string position { get; set; }

        public long? Salary { get; set; }

        public DateTime? DateOfEmployment { get; set; }

        public virtual ACCOUNT_STAFF ACCOUNT_STAFF { get; set; }

        public virtual AGENCY AGENCY { get; set; }

        public virtual DISTRICT DISTRICT { get; set; }
    }
}
