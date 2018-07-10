namespace NobatDehi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DoctorSpecialty")]
    public partial class DoctorSpecialty
    {
        public int Id { get; set; }

        public int? DoctorId { get; set; }

        public int? SpecialtyId { get; set; }

        public virtual Doctor Doctor { get; set; }

        public virtual Specialty Specialty { get; set; }
    }
}