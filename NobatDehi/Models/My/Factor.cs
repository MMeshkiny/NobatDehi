namespace NobatDehi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Factor")]
    public partial class Factor
    {
        public int Id { get; set; }

        public long? VisitTimeId { get; set; }

        public DateTime? PreFactorDateTime { get; set; }

        public int? Fee { get; set; }

        public DateTime? PayedDateTime { get; set; }

        public string PatientUserId { get; set; }

        [StringLength(50)]
        public string TrackingCode { get; set; }

        public virtual ApplicationUser PatientUser{ get; set; }

        public virtual VisitTime VisitTime { get; set; }
    }
}
