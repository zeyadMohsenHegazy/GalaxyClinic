using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Models.DomainModels
{
    public class ReservationAttachment : DefaulDomain
    {
        [Key]
        public int reservationAttachmentId { get; set; }

        [StringLength(30, ErrorMessage = "The File Name should be less than 30 character")]
        public string fileName { get; set; }

        public string filePath { get; set; }

        [ForeignKey("reservation")]
        public int reservationId { get; set; }
        public virtual Reservation reservation { get; set; }
    }
}
