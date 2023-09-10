using Models.API.Request;
using Models.API.Request.ConfigRequest;
using Models.DomainModels;

namespace DataAccess.DAL.IRepo
{
    public interface ISpecialityRepo
    {
        //Create
        bool Add(SpecialityRequest speciality);

        //Remove
        bool Remove(GeneralRequest request);

        //Retrieve All Doctors
        IEnumerable<Speciality> GetAll();

        //Retrieve One Doctor
        Speciality GetOne(GeneralRequest speciality);

        //Update
        bool Update(SpecialityRequest request);
    }
}
