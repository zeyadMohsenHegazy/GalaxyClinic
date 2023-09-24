using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Models.DomainModels
{
    public class Patient : DefaulDomain
    {
        [Key]
        public int patientId { get; set; }

        [StringLength(30)]
        public string name { get; set; }

        [StringLength(11)]
        public string mobileNumber { get; set; }
        public string email { get; set; }
        public DateTime dateOfBirth { get; set; }

        [ForeignKey("user")]
        public int userId { get; set; }
        public virtual User user { get; set; }
    }
}
