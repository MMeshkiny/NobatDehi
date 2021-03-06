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

        public string DoctorId { get; set; }

        public int? SpecialtyId { get; set; }
        [Display(Name = "پزشک")]
        public virtual ApplicationUser Doctor { get; set; }
        [Display(Name = "تخصص")]
        public virtual Specialty Specialty { get; set; }
    }
}
