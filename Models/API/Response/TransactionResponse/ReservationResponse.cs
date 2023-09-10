namespace Models.API.Response.TransactionResponse
{
    public class ReservationResponse
    {
        public int Reservation_Code { get; set; }
        public int DoctorCode { get; set; }
        public int StatusCode { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime ReservationTime { get; set; }
    }
}
