using Models.API.Request;
using Models.API.Request.ConfigRequest;
using Models.DomainModels;

namespace DataAccess.DAL.IRepo
{
    public interface IUserTypeRepo
    {
        //Create
        bool Add(UserTypeRequest request);

        //Remove
        bool Remove(GeneralRequest request);

        //Retrieve All Doctors
        IEnumerable<UserType> GetAll();

        //Retrieve One Doctor
        UserType GetOne(GeneralRequest request);

        //Update
        bool Update(UserTypeRequest request);
    }
}
