namespace Models.API.Request.ConfigRequest
{
    public class userPatientRequest : UserRequest
    {
        public string fullName { get; set; }
        public string mobileNumber { get; set; }
        public string email { get; set; }
    }
}
