using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Models.DomainModels
{
    public class DoctorShift : DefaulDomain
    {
        [Key]
        public int doctorShiftId { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public string availableDaysOfweek { get; set; }
        public DateTime fromTime { get; set; }
        public DateTime toTime { get; set; }
        public int sessionDurationMinutes { get; set; }
        public string shiftTitle { get; set; }

        [ForeignKey("doctor")]
        public int doctorId { get; set; }
        public virtual Doctor doctor { get; set; }

    }
}
