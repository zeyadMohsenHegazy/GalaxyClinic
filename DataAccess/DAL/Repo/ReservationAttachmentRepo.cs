using Azure.Core;
using DataAccess.DAL.IRepo;
using DataAccess.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Models.API.Request;
using Models.API.Request.TransactionRequest;
using Models.DomainModels;

namespace DataAccess.DAL.Repo
{
    public class ReservationAttachmentRepo : IReservationAttachmentRepo
    {
        private readonly ApplicationDbContext _context;
        public ReservationAttachmentRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(ReservationAttachmentRequest request)
        {
            try
            {
                ReservationAttachment newReservationAttach = new ReservationAttachment();
                newReservationAttach.fileName = request.FileName;
                newReservationAttach.filePath = request.FilePath;
                newReservationAttach.reservationId = request.ReservationID;

                newReservationAttach.CreatedBy = request.UserId;
                newReservationAttach.CreatedAt = DateTime.Now;
                newReservationAttach.IsDeleted = false;
                newReservationAttach.IsEnabled = true;

                _context.ReservationAttachments.Add(newReservationAttach);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<ReservationAttachment> GetAll()
        {
            IEnumerable<ReservationAttachment> reservationAttachs = _context.ReservationAttachments
                .Include(z => z.reservation)
                .Where(x => x.IsDeleted == false && x.IsEnabled == true)
                .ToList();

            return reservationAttachs;
        }

        public ReservationAttachment GetOne(GeneralRequest request)
        {
            var reservationAttach = _context.ReservationAttachments
                           .Include(z => z.reservation)
                           .Where(z => z.IsDeleted == false && z.IsEnabled == true)
                           .FirstOrDefault(z => z.reservationAttachmentId == request.Id);

            if (reservationAttach != null)
            {
                return reservationAttach;
            }
            else
            {
                return null;
            }
        }

        public bool Remove(GeneralRequest request)
        {
            try
            {
                var reservationAttach = _context.ReservationAttachments
                    .FirstOrDefault(z => z.reservationAttachmentId == request.Id);
                reservationAttach.IsDeleted = true;
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(ReservationAttachmentRequest reservationAttachment)
        {
            var updatedReservationAttach = _context.ReservationAttachments.Find(reservationAttachment.Id);
            if (updatedReservationAttach != null)
            {
                try
                {
                    updatedReservationAttach.fileName = reservationAttachment.FileName;
                    updatedReservationAttach.filePath = reservationAttachment.FilePath;

                    updatedReservationAttach.ModifiedAt = DateTime.Now;
                    updatedReservationAttach.ModifiedBy = reservationAttachment.UserId;

                    _context.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
                return false;
        }
    }
}
