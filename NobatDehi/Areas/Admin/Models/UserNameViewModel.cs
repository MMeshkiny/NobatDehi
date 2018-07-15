using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NobatDehi.Models
{
    public class UserNameViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Required]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }
    }
}