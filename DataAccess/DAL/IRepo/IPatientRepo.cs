using Models.API.Request;
using Models.API.Request.ConfigRequest;
using Models.DomainModels;

namespace DataAccess.DAL.IRepo
{
    public interface IPatientRepo
    {
        //Create
        bool Add(PatientRequest patient);

        //Remove
        bool Remove(GeneralRequest request);

        //Retrieve All Doctors
        IEnumerable<Patient> GetAll();

        //Retrieve One Doctor
        Patient GetOne(GeneralRequest request);

        //Update
        bool Update(PatientRequest patient);
    }
}
