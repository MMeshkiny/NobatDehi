namespace NobatDehi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CancelTime")]
    public partial class CancelTime
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CancelTime()
        {
            PayFactors = new HashSet<PayFactor>();
        }

        public int Id { get; set; }

        public long? VisitTimeId { get; set; }

        public string PatientId { get; set; }

        public DateTime? ReserveDateTime { get; set; }

        public DateTime? CancelDatetime { get; set; }

        public int? Fee { get; set; }

        public int? CancellationCost { get; set; }

        public virtual ApplicationUser User{ get; set; }

        public virtual VisitTime VisitTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PayFactor> PayFactors { get; set; }
    }
}
