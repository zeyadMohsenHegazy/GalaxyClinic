namespace Models.API.Request.ConfigRequest
{
    public class UserRequest : BaseRequest
    {
        public int Id { get; set; }
        public string userName { get; set; }
        public string userPassword { get; set; }
        public int userTypeId { get; set; }
    }
}
