namespace Models.API.Response.TransactionResponse
{
    public class ReservationAttachmentResponse
    {
        public int ReservationAttach_Code { get; set; }
        public int Reservation_Code { get; set; }
        public string File_Name { get; set; }
        public string File_Path { get; set;}
    }
}
