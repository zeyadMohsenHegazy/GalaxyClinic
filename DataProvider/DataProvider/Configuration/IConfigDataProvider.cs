using DataAccess.DAL.IRepo;

namespace DataProvider.DataProvider.Configuration
{
    public interface IConfigDataProvider
    {
        IDoctorRepo doctorRepo { get; }
        IPatientRepo patientRepo { get; }
        ISpecialityRepo specialityRepo { get; }
        IUserTypeRepo userTypeRepo { get; }
        IUserRepo userRepo { get; }
        IStatusRepo statusRepo { get; }
        IDoctorShiftRepo doctorShiftRepo { get; }
    }
}
