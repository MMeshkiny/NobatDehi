namespace NobatDehi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MedicalCenter")]
    public partial class MedicalCenter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MedicalCenter()
        {
            DoctorMedicalCenters = new HashSet<DoctorMedicalCenter>();
        }

        public int Id { get; set; }
        [Display(Name = "نوع مرکز")]
        public int? TypeId { get; set; }
        [Display(Name = "نام مرکز")]
        public string Name { get; set; }
        [Display(Name = "آدرس مرکز")]
        public string Address { get; set; }
        [Display(Name = "توضیحات")]
        public string Description { get; set; }
        [Display(Name = "آدرس عکس")]
        public string ImageUrl { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DoctorMedicalCenter> DoctorMedicalCenters { get; set; }

        public virtual MedicalCenterType MedicalCenterType { get; set; }
    }
}
