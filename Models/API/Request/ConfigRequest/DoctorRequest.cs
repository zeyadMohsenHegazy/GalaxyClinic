namespace Models.API.Request.ConfigRequest
{
    public class DoctorRequest : BaseRequest
    {
        public int Id { get; set; }
        public string Doctor_Name { get; set; }
        public int Speciality_Code { get; set; }
        public string mobileNumber { get; set; }
        public int User_Code { get; set; }
    }
}
