namespace Models.API.Request.ConfigRequest
{
    public class UserRequest : GeneralRequest
    {
        public string User_Name { get; set; }
        public string User_Password { get; set; }
    }
}
