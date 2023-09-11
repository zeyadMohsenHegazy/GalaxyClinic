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
        List<DoctorShift> GetAll();

        //Retrieve One Doctor
        DoctorShift GetOne(GeneralRequest request);

        //cancell Shift
        bool cancellSift(GeneralRequest request);
        //cancell day
        bool cancellSiftDay(GeneralRequest request);
        //cancell time
        bool cancellSiftDayTime(GeneralRequest request);

    }
}
