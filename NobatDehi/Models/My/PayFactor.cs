namespace NobatDehi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PayFactor")]
    public partial class PayFactor
    {
        public int Id { get; set; }

        public int? CancelTimeId { get; set; }

        public int? DoctorMedicalCenterId { get; set; }

        public int? Fee { get; set; }

        public DateTime CreateDateTime { get; set; }

        public DateTime? PayDateTime { get; set; }

        public string TracingCode { get; set; }

        public virtual CancelTime CancelTime { get; set; }

        public virtual DoctorMedicalCenter DoctorMedicalCenter { get; set; }
    }
}
