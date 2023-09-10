namespace Models.API.Response.ConfigResponse
{
    public class DoctorShiftResponse
    {
        public int DoctorShiftCode { get; set; }
        public string Availble_Days { get; set; }
        public int Docrtor_Code { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
    }
}
