namespace Models.API.Request.ConfigRequest
{
    public class StatusRequest : BaseRequest
    {
        public int Id { get; set; }
        public string StatusName { get; set; }

    }
}
