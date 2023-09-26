namespace Models.API.Request.ConfigRequest
{
    public class SpecialityRequest : BaseRequest
    {
        public int Speciality_Code { get; set; }
        public string Speciality_Name { get; set; }
    }
}
