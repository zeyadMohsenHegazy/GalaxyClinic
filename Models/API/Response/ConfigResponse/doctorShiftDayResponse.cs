﻿namespace Models.API.Response.ConfigResponse
{
    public class doctorShiftDayResponse
    {
        public int shiftDayCode { get; set; }
        public DateTime dayWeek { get; set; }
        public List<doctorShiftDayTimeResponse> sessionTimes { get; set; }
    }
}
