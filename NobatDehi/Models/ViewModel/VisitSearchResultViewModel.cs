using System;
using System.ComponentModel.DataAnnotations;

namespace NobatDehi.Models
{
    public class VisitSearchResultViewModel
    {
        public VisitSearchResultViewModel()
        {

        }
        public VisitSearchResultViewModel(VisitRecord visitRecord)
        {
            this.visitRecord = visitRecord;
        }
        public VisitRecord visitRecord
        {
            get { return null; }
            set
            {
                
                Id = value.Id;
                MedicalCenterName = value.MedicalCenter?.Name;
                MedicalCenterTypeTitle = value.MedicalCenter?.MedicalCenterType?.Title;
                DoctorName = value.Doctor.FirstName + " " + value.Doctor.LastName;
                SpecialtyTitle = value.Specialty?.Title;
                Start = (value.Start);
                End = (value.Start.AddMinutes((value.Count - 1) * (value.Duration + value.Break)));
                Duration = value.Duration;
                Count = value.Count;
                Available = Count - value.Removed - value.Reserved;
            }
        }
        [Display(Name = "کد رکورد ویزیت")]
        public int Id { get; set; }
        [Display(Name = "مرکز درمانی")]
        public string MedicalCenterName { get; set; }
        [Display(Name = "نوع مرکز")]
        public string MedicalCenterTypeTitle { get; set; }
        [Display(Name = "پزشک")]
        public string DoctorName { get; set; }
        [Display(Name = "تخصص")]
        public string SpecialtyTitle { get; set; }
        [Display(Name = "شروع")]
        public DateTime Start { get; set; }
        [Display(Name = "پایان")]
        public DateTime End { get; set; }
        [Display(Name = "زمان ویزیت")]
        public int Duration { get; set; }
        [Display(Name = "تعداد")]
        public int Count { get; set; }
        [Display(Name = "موجود")]
        public int Available { get; set; }
    }

}