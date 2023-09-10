using Models.API.Request;
using Models.API.Request.ConfigRequest;
using Models.DomainModels;

namespace DataAccess.DAL.IRepo
{
    public interface IUserRepo
    {
      
        //Remove
        bool Remove(GeneralRequest request);

        //Retrieve All Doctors
        IEnumerable<User> GetAll();

        //Retrieve One Doctor
        User GetOne(GeneralRequest request);

        //Update
        bool Update(UserRequest request);
    }
}
