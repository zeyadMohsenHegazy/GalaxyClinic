namespace Models.API.Request.ConfigRequest
{
    public class resertPasswordRequest
    {
        public int userId { get; set; }
        public string userPassword { get; set; }
        public string confirmPassword { get; set; }
    }
}
