namespace Models.API.Request.TransactionRequest
{
    public class ReservationAttachmentRequest : GeneralRequest
    {
        public int ReservationID { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
    }
}
