using System;

namespace NobatDehi.Models
{
    [Flags]
    public enum VisitTimeState
    {
        Active=1,
        Deactive=2,
        DoctorCancel=6,
        Surgery=10,
        Reserved=18,
        PreReserved=34
    }
}