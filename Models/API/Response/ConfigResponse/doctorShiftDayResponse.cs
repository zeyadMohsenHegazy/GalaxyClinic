namespace Models.API.Response.ConfigResponse
{
    public class doctorShiftDayResponse
    {
        public string Date_WeekDay { get; set; }
        public List<doctorShiftDayTimeResponse> sessionTimes { get; set; }
    }
}
