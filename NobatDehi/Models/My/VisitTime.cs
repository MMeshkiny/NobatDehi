namespace NobatDehi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VisitTime")]
    public partial class VisitTime
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VisitTime()
        {
            CancelTimes = new HashSet<CancelTime>();
            Factors = new HashSet<Factor>();
        }

        public long Id { get; set; }

        public int? Order { get; set; }

        public DateTime? Start { get; set; }

        public string PatientUserId { get; set; }

        public bool Removed { get; set; }

        public int? DoctorMedicalCenterId { get; set; }

        public byte? VisitDurationMin { get; set; }

        public bool? Visited { get; set; }

        public int? Fee { get; set; }

        public DateTime? ReserveDateTime { get; set; }

        public DateTime? PayDateTime { get; set; }

        public double? CancelRate { get; set; }

        public virtual ApplicationUser PatientUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CancelTime> CancelTimes { get; set; }

        public virtual DoctorMedicalCenter DoctorMedicalCenter { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Factor> Factors { get; set; }
    }
}
