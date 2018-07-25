using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NobatDehi.Models
{
    public class VisitTimeSearchViewModel
    {
        public VisitTimeSearchViewModel()
        {
            Start = ( DateTime.Now);
            Duration = 4;
            MedicalCenterId = 0;
            SpetialtyId = 0;
            DoctorId = null;
            Page = 1;
            Results = new List<VisitSearchResultViewModel>();
        }
        [DataType(DataType.Date)]
        [Display(Name ="شروع")]
        public DateTime Start { get; set; }
        [Display(Name = "به مدت")]
        public int Duration { get; set; }
        [Display(Name = "مرکز درمانی")]
        public int MedicalCenterId { get; set; }
        [Display(Name = "تخصص")]
        public int SpetialtyId { get; set; }
        [Display(Name = "پزشک")]
        public string DoctorId { get; set; }
        [Display(Name = "صفحه")]
        public int Page { get; set; }
        [Display(Name = "نتایج")]
        public IEnumerable<VisitSearchResultViewModel> Results { get; set; }
    }
}