using System.ComponentModel.DataAnnotations;

namespace NobatDehi.Models
{
    public class PayViewModel
    {
        [DataType("GUID")]
        [Key]
        public string Tracking { get; set; }
        public int? Fee { get; set; }
        public PayStatus? PayStatus { get; set; }
    }

    public enum PayStatus
    {
        Success,
        Unsuccess
    }
}