using System.ComponentModel.DataAnnotations.Schema;

namespace Models.DomainModels
{
    public class doctorShiftDayTime : DefaulDomain
    {
        public int doctorShiftDayTimeId { get; set; }
        public DateTime fromTime { get; set; }
        public DateTime toTime { get; set; }
        public bool isCancelled { get; set; }

        [ForeignKey("doctorShiftDay")]
        public int doctorShiftDayId { get; set; }
        public virtual doctorShiftDay doctorShiftDay { get; set; }
    }
}
