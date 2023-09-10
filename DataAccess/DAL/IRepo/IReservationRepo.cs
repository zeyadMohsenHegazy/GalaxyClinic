using Models.API.Request;
using Models.API.Request.ConfigRequest;
using Models.API.Request.TransactionRequest;
using Models.API.Response.TransactionResponse;
using Models.DomainModels;

namespace DataAccess.DAL.IRepo
{
    public interface IReservationRepo
    {
        //Create
        bool Add(ReservationRequest request);

        //Remove
        bool Remove(GeneralRequest request);

        //Retrieve All Doctors
        IEnumerable<Reservation> GetAll();

        //Retrieve One Doctor
        Reservation GetOne(GeneralRequest request);

        //Update
        bool Update(ReservationRequest request);
    }
}
