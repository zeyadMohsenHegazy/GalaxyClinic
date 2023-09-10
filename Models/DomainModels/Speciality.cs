using System.ComponentModel.DataAnnotations;

namespace Models.DomainModels
{
    public class Speciality : DefaulDomain
    {
        [Key]
        public int specialityId { get; set; }
        [Required]
        [StringLength(30)]
        public string name { get; set; }
    }
}
