using Models.API.Request;
using Models.API.Request.ConfigRequest;
using Models.DomainModels;

namespace DataAccess.DAL.IRepo
{
    public interface IStatusRepo
    {
        //Create
        bool Add(StatusRequest request);

        //Remove
        bool Remove(GeneralRequest request);

        //Retrieve All Doctors
        IEnumerable<Status> GetAll();

        //Retrieve One Doctor
        Status GetOne(GeneralRequest request);

        //Update
        bool Update(StatusRequest request);
    }
}
