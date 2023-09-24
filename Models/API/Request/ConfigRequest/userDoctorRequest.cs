namespace Models.API.Request.ConfigRequest
{
    public class userDoctorRequest : UserRequest
    {
        public string doctorName { get; set; }
        public string doctorMobileNumber { get; set; }
        public string doctorEmail { get; set; }
        public int doctorSpeciality { get; set; }

    }
}
