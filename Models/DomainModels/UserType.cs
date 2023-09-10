using System.ComponentModel.DataAnnotations;

namespace Models.DomainModels
{
    public class UserType : DefaulDomain
    {
        [Key]
        public int typeId { get; set; }
        [StringLength(30)]
        public string name { get; set; }
    }
}
