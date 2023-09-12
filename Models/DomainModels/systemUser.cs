using System.ComponentModel.DataAnnotations.Schema;

namespace Models.DomainModels
{
    public class systemUser : DefaulDomain
    {
        public int systemUserId { get; set; }
        public string name { get; set; }
        public string mobileNumber { get; set; }
        public string email { get; set; }

        [ForeignKey("user")]
        public int userId { get; set; }
        public virtual User user { get; set; }
    }
}
