using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Models.DomainModels
{
    public class User :DefaulDomain
    {
        [Key]
        public int userId { get; set; }

        [Required]
        [StringLength(30)]
        public string userName { get; set; }

        [Required]
        public string password { get; set; }

        [ForeignKey("userType")]
        public int userTypeId { get; set; }
        public virtual UserType userType { get; set; }
    }
}
