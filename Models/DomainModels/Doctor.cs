using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Models.DomainModels
{
    public class Doctor : DefaulDomain
    {
        [Key]
        public int doctorId { get; set; }
        [StringLength(30)]
        public string name { get; set; }
        public string mobileNumber { get; set; }

        [ForeignKey("speciality")]
        public int specialityId { get; set; }
        public virtual Speciality speciality { get; set; }

        [ForeignKey("user")]
        public int userId { get; set; }
        public virtual User user { get; set; }
    }
}
