using System.ComponentModel.DataAnnotations;

namespace Models.DomainModels
{
    public class Status : DefaulDomain
    {
        [Key]
        public int statusId { get; set; }

        [StringLength(30)]
        public string name { get; set; }
    }
}
