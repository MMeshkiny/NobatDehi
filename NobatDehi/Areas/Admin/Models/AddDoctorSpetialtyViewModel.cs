using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NobatDehi.Models
{
    public class AddDoctorSpetialtyViewModel
    {
        public string Id { get; set; }
        [Display(Name ="تخصص")]
        public int Spetialty { get; set; }
    }
}