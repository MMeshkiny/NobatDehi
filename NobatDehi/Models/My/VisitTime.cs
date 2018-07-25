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
        [Display(Name ="ترتیب")]
        public int? Order { get; set; }
        [Display(Name = "شروع")]
        public DateTime? Start { get; set; }
        [Display(Name = "کد بیمار")]
        public string PatientUserId { get; set; }
        [Display(Name = "حذف شده")]
        public bool Removed { get; set; }
        [Display(Name = "کد مرکز درمانی")]
        public int? DoctorMedicalCenterId { get; set; }
        [Display(Name = "مدت ویزیت")]
        public byte? VisitDurationMin { get; set; }
        [Display(Name = "ویزیت شده")]
        public bool? Visited { get; set; }
        [Display(Name = "مبلغ")]
        public int? Fee { get; set; }
        [Display(Name = "تاریخ رزرو")]
        public DateTime? ReserveDateTime { get; set; }
        [Display(Name = "تاریخ پرداخت")]
        public DateTime? PayDateTime { get; set; }
        [Display(Name = "ضریب کنسلی")]
        public double? CancelRate { get; set; }
        [Display(Name = "کد رکورد ویزیت")]
        public int? VisitRecordId { get; set; }



        [Display(Name = "بیمار")]
        public virtual ApplicationUser PatientUser { get; set; }
        [Display(Name = "رکورد ویزیت")]
        public virtual VisitRecord VisitRecord { get; set; }
        [Display(Name = "زمان های کنسل شده")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CancelTime> CancelTimes { get; set; }
        [Display(Name = "مرکز درمانی")]
        public virtual DoctorMedicalCenter DoctorMedicalCenter { get; set; }
        [Display(Name = "فاکتور ها")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Factor> Factors { get; set; }
    }
}
