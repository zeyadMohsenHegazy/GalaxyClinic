namespace Models.API.Response
{
    public class BaseResponse 
    {
        public bool Success { get; set; }
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public Object Result { get; set; }
    }
}
