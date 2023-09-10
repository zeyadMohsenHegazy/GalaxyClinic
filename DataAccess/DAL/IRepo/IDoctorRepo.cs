using Models.API.Request;
using Models.API.Request.ConfigRequest;
using Models.DomainModels;

namespace DataAccess.DAL.IRepo
{
    public interface IDoctorRepo
    {
        //Create
        bool Add(DoctorRequest doctor);

        //Remove
        bool Remove(GeneralRequest request);

        //Retrieve All Doctors
        IEnumerable<Doctor> GetAll();

        //Retrieve One Doctor
        Doctor GetOne(GeneralRequest request);

        //Update
        bool Update(DoctorRequest doctor);
    }
}
