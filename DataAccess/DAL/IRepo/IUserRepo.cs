using Models.API.Request;
using Models.API.Request.ConfigRequest;
using Models.API.Response.ConfigResponse;
using Models.DomainModels;

namespace DataAccess.DAL.IRepo
{
    public interface IUserRepo
    {
        //Create user for register
        bool createUserDoctor(userDoctorRequest request);
        bool createUserSystem(userSystemRequest request);
        bool createUserPatient(userPatientRequest request);

        //Login
        userLoginResponse userLogin(userLoginRequest request);

        //forget password
        forgetPasswordResponse forgetPassword(forgetPasswordRequest request);
        //reset password 
        bool resetPassword(resertPasswordRequest request);


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
