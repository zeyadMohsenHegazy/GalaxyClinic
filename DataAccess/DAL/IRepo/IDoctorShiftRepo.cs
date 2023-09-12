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
        bool removeDoctorShift(GeneralRequest request);

        //Retrieve All Doctors
        List<DoctorShift> getAllShifts();

        //Retrieve One Doctor
        DoctorShift getDoctorShift(GeneralRequest request);
        DoctorShift getDoctorActiveShifts(GeneralRequest request);
        DoctorShift getDoctorAllShifts(GeneralRequest request);
        //cancell Shift
        bool cancellShift(GeneralRequest request);
        //cancell day
        bool cancellShiftDay(GeneralRequest request);
        //cancell time
        bool cancellShiftDayTime(GeneralRequest request);

    }
}
