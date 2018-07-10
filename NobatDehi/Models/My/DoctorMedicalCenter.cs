namespace NobatDehi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DoctorMedicalCenter")]
    public partial class DoctorMedicalCenter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DoctorMedicalCenter()
        {
            PayFactors = new HashSet<PayFactor>();
            VisitTimes = new HashSet<VisitTime>();
        }

        public int Id { get; set; }

        public int? MedicalCenterId { get; set; }

        public int DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }

        public virtual MedicalCenter MedicalCenter { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PayFactor> PayFactors { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VisitTime> VisitTimes { get; set; }
    }
}
