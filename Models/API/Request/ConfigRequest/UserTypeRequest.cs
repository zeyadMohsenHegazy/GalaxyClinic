namespace Models.API.Request.ConfigRequest
{
    public class UserTypeRequest : BaseRequest
    {
        public int Id { get; set; }
        public string UserTypeName { get; set; }

    }
}
