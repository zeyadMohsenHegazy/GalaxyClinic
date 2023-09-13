namespace Models.API.Request.ConfigRequest
{
    public class forgetPasswordRequest
    {
        public int userTypeId { get; set; }
        public string userEmailOrMobile { get; set; }
    }
}
