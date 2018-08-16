namespace NobatDehi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Specialty")]
    public partial class Specialty
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Specialty()
        {
            DoctorSpecialties = new HashSet<DoctorSpecialty>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        [Display(Name = "تخصص")]
        public string Title { get; set; }
        [Display(Name = "تخصص مادر")]
        public int? ParentId { get; set; }
        [Display(Name = "توضیحات")] public string Description { get; set; }

        [NotMapped]
        public string DisplayTitle => string.IsNullOrWhiteSpace(Description) ? Title : $"{Title} ({Description})";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DoctorSpecialty> DoctorSpecialties { get; set; }
        public virtual ICollection<VisitRecord> VisitRecords { get; set; }
        public virtual Specialty ParentSpecialty { get; set; }
        public ICollection<Specialty> ChildSpetialities { get; set; }
    }
}
