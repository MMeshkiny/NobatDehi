using System;

namespace NobatDehi.Models
{
    [Flags]
    public enum VisitRecordState
    {
        Active = 1,
        Deactive = 2,
        Surgery = 6,
        DoctorRemoved = 10
    }
}