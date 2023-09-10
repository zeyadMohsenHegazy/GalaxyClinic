namespace Models.API.Request.ConfigRequest
{
    public class DoctorShiftRequest : GeneralRequest
    {
        public DateTime fromDate { get; set; }
        public DateTime toDate   { get; set; }
        public DateTime fromTime { get; set; }
        public DateTime toTime { get; set; }
        public int doctorId { get; set; }
        public int sessionDuration { get; set; }
        public string shiftTitle { get; set; }
    }
}
