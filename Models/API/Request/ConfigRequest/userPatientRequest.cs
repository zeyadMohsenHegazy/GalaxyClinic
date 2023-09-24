namespace Models.API.Request.ConfigRequest
{
    public class userPatientRequest : UserRequest
    {
        public string pateintName { get; set; }
        public string patientMobileNumber { get; set; }
        public string patientEmail { get; set; }
        public DateTime patientDateOfBirth { get; set; }
    }
}
