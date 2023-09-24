namespace Models.API.Request.ConfigRequest
{
    public class systemUserRequest : UserRequest
    {
        public string systemUserName { get; set; }
        public string systemUserMobileNumber { get; set; }
        public string systemUserEmail { get; set; }
    }
}
