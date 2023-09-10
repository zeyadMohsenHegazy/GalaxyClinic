namespace Models.API.Request.TransactionRequest
{
    public class ReservationRequest : GeneralRequest
    {
        public int DoctorId { get; set; }
        public int StatusId { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime ReservationTime { get; set; }
    }
}
