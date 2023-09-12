namespace Models.API.Request.ConfigRequest
{
    public class userDoctorRequest : UserRequest
    {
        public string fullName { get; set; }
        public string mobileNumber { get; set; }
        public string email { get; set; }
        public int doctorSpeciality { get; set; }

    }
}
