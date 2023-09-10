using DataAccess.DAL.IRepo;
using DataAccess.DAL.Repo;
using DataAccess.DatabaseContext;

namespace DataProvider.DataProvider.Transaction
{
    public class TransactionDataProvider : ITransactionDataProvider
    {
        private readonly ApplicationDbContext _context;
        public TransactionDataProvider(ApplicationDbContext context)
        {
            _context = context;
            reservationRepo = new ReservationRepo(context);
            attachmentRepo = new ReservationAttachmentRepo(context);
        }

        public IReservationRepo reservationRepo { get; private set; }

        public IReservationAttachmentRepo attachmentRepo { get; private set; }
    }
}
