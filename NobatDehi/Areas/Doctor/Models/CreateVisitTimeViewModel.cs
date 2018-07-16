using NobatDehi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NobatDehi.Models
{
    public class CreateVisitTimeViewModel
    {
        [Required,DisplayName("مرکز درمانی")]
        public int MedicalCenter { get; set; }
        [Required, DataType(DataType.DateTime), DisplayName("شروع")]
        public DateTime StartDateTime { get; set; }
        [DisplayName("پزشک")]
        public string Doctor { get; set; }
        [Required, DataType(DataType.Duration), DisplayName("مدت هر ویزیت")]
        public int Duration { get; set; }
        [Required, DefaultValue(0), DataType(DataType.Duration), DisplayName("فاصله بین ویزیت")]
        public int Break { get; set; }
        [Required, DisplayName("تعداد ویزیت")]
        public int Count { get; set; }
        [Required,DataType(DataType.Currency), DisplayName("هزینه ویزیت")]
        public int Fee { get; set; }
        [Required, DefaultValue(0), DisplayName("درصد کنسلی (صفر به معنای بازگشت کل مبلغ است)")]
        public int CancelRate { get; set; }
    }
}