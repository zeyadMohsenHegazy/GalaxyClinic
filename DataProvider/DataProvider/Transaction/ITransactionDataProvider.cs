using DataAccess.DAL.IRepo;

namespace DataProvider.DataProvider.Transaction
{
    public interface ITransactionDataProvider
    {
        IReservationRepo reservationRepo { get; }
        IReservationAttachmentRepo attachmentRepo { get; }
    }
}
