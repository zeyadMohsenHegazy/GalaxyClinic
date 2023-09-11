using System.ComponentModel.DataAnnotations.Schema;

namespace Models.DomainModels
{
    public class doctorShiftDay : DefaulDomain
    {
        public int doctorShiftDayId { get; set; }
        public DateTime days { get; set; }
        public string weekDays { get; set; }
        public bool isCancelled { get; set; }

        [ForeignKey("doctorShift")]
        public int doctorShiftId { get; set; }
        public virtual DoctorShift doctorShift { get; set; }
        public virtual ICollection<doctorShiftDayTime> doctorShiftDayTimes { get; set; }

    }
}
