namespace Models.API.Response.ConfigResponse
{
    public class DoctorShiftResponse
    {
        public int DoctorShiftCode { get; set; }
        public string Availble_Days { get; set; }
        public int Docrtor_Name { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public string shiftTitle { get; set; }
        public int sessionDuration { get; set; }
        public List<doctorShiftDayResponse> shiftDays { get; set; }
    }
}
