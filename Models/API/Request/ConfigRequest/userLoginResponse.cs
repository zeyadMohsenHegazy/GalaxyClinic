﻿namespace Models.API.Request.ConfigRequest
{
    public class userLoginResponse
    {
        public int userId {  get; set; }
        public string userType { get; set; }
        public string userName { get; set; }
        public string userToken { get; set; }
    }
}
