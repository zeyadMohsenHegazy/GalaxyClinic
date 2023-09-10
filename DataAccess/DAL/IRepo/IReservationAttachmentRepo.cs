using Models.API.Request;
using Models.API.Request.ConfigRequest;
using Models.API.Request.TransactionRequest;
using Models.DomainModels;

namespace DataAccess.DAL.IRepo
{
    public interface IReservationAttachmentRepo
    {
        //Create
        bool Add(ReservationAttachmentRequest request);

        //Remove
        bool Remove(GeneralRequest request);

        //Retrieve All Doctors
        IEnumerable<ReservationAttachment> GetAll();

        //Retrieve One Doctor
        ReservationAttachment GetOne(GeneralRequest request);

        //Update
        bool Update(ReservationAttachmentRequest reservationAttachment);
    }
}
