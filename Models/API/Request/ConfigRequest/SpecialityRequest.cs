namespace Models.API.Request.ConfigRequest
{
    public class SpecialityRequest : BaseRequest
    {
        public int Id { get; set; }
        public string specialityName { get; set; }
    }
}
