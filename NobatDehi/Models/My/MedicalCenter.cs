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

        public int? TypeId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DoctorMedicalCenter> DoctorMedicalCenters { get; set; }

        public virtual MedicalCenterType MedicalCenterType { get; set; }
    }
}
