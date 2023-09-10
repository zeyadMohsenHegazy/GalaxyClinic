namespace Models.API.Request.ConfigRequest
{
    public class PatientRequest : GeneralRequest
    {
        public string Patient_Name { get; set; }
        public string mobileNumber { get; set; }
    }
}
