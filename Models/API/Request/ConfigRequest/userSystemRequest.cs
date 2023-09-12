namespace Models.API.Request.ConfigRequest
{
    public class userSystemRequest : UserRequest
    {
        public string fullName { get; set; }
        public string mobileNumber { get; set; }
        public string email { get; set; }
    }
}
