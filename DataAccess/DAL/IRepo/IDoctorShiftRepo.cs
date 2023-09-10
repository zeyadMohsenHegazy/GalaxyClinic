using Models.API.Request;
using Models.API.Request.ConfigRequest;
using Models.DomainModels;

namespace DataAccess.DAL.IRepo
{
    public interface IDoctorShiftRepo
    {
        //Create
        bool Add(DoctorShiftRequest request);

        //Remove
        bool Remove(GeneralRequest request);

        //Retrieve All Doctors
        IEnumerable<DoctorShift> GetAll();

        //Retrieve One Doctor
        DoctorShift GetOne(GeneralRequest request);

        //Update
        bool Update(DoctorShiftRequest request);
    }
}
