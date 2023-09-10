using DataAccess.DAL.IRepo;
using DataAccess.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Models.API.Request;
using Models.API.Request.TransactionRequest;
using Models.DomainModels;

namespace DataAccess.DAL.Repo
{
    public class ReservationRepo : IReservationRepo
    {
        private readonly ApplicationDbContext _context;
        public ReservationRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(ReservationRequest request)
        {
            try
            {
                Reservation newReservation = new Reservation();
                newReservation.doctorId = request.DoctorId;
                newReservation.statusId = request.StatusId;
                newReservation.reservationDate = request.ReservationDate;
                newReservation.reservationTime = request.ReservationTime;

                newReservation.CreatedBy = request.UserId;
                newReservation.CreatedAt = DateTime.Now;
                newReservation.IsDeleted = false;
                newReservation.IsEnabled = true;

                _context.Reservations.Add(newReservation);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Reservation> GetAll()
        {
            IEnumerable<Reservation> reservations = _context.Reservations
                .Include(z => z.doctor)
                .Where(x => x.IsDeleted == false && x.IsEnabled == true)
                .ToList();

            return reservations;
        }

        public Reservation GetOne(GeneralRequest request)
        {
            var reservation = _context.Reservations
                .Include (z => z.doctor)
                .Where(z => z.IsDeleted == false && z.IsEnabled == true)
                .FirstOrDefault(z => z.reservationId == request.Id);

            if (reservation != null)
            {
                return reservation;
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
                var reservation = _context.Reservations
                    .FirstOrDefault(z => z.reservationId == request.Id);
                reservation.IsDeleted = true;
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(ReservationRequest request)
        {
            var updatedReservation = _context.Reservations.Find(request.Id);
            if (updatedReservation != null)
            {
                try
                {
                    updatedReservation.statusId = request.StatusId;
                    updatedReservation.reservationDate = request.ReservationDate;
                    updatedReservation.reservationTime = request.ReservationTime;

                    updatedReservation.ModifiedAt = DateTime.Now;
                    updatedReservation.ModifiedBy = request.UserId;

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
