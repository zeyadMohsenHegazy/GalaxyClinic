using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Models.DomainModels
{
    public class Reservation : DefaulDomain
    {
        [Key]
        public int reservationId { get; set; }

        [ForeignKey("doctor")]
        public int doctorId { get; set; }
        public virtual Doctor doctor { get; set; }

        public DateTime reservationDate { get; set; }
        public DateTime reservationTime { get; set; }

        [ForeignKey("status")]
        public int statusId { get; set; }
        public Status status { get; set; }
    }
}
