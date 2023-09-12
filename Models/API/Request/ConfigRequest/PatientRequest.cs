namespace Models.API.Request.ConfigRequest
{
    public class PatientRequest : BaseRequest
    {
        public int Id { get; set; }
        public string Patient_Name { get; set; }
        public string mobileNumber { get; set; }
    }
}
