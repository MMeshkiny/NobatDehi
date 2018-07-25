namespace NobatDehi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class VisitRecord
    {
        public int Id { get; set; }
        [Display(Name ="کد پزشک")]
        public string DoctorId { get; set; }
        [Display(Name = "کد مرکز درمانی")]
        public int MedicalCenterId { get; set; }
        [Display(Name = "شروع ویزیت")]
        public DateTime Start { get; set; }
        [Display(Name = "تعداد ویزیت")]
        public int Count { get; set; }
        [Display(Name = "مدت ویزیت")]
        public int Duration { get; set; }
        [Display(Name = "فاصله بین ویزیت")]
        public int Break { get; set; }
        [Display(Name = "رزرو شده")]
        [DefaultValue(0)]
        public int Reserved { get; set; }
        [Display(Name ="حذف شده")]
        public int Removed { get; set; }
        [Display(Name = "کد تخصص")]
        public int? SpecialtyId { get; set; }
        [Display(Name = "مبلغ ویزیت")]
        public int Fee { get; set; }
        [Display(Name = "ضریب کنسلی")]
        public int CancelRate { get; set; }

        [Display(Name = "پزشک")]
        public virtual ApplicationUser Doctor { get; set; }
        [Display(Name = "مرکز درمانی")]
        public virtual MedicalCenter MedicalCenter { get; set; }
        [Display(Name = "تخصص")]
        public virtual Specialty Specialty { get; set; }
        [Display(Name ="ویزیت ها")]
        public virtual ICollection<VisitTime> VisitTimes { get; set; }
    }
}
