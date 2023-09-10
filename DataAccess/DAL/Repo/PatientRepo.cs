using DataAccess.Auto_Mapper;
using DataAccess.DAL.IRepo;
using DataAccess.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Models.API.Request;
using Models.API.Request.ConfigRequest;
using Models.DomainModels;
using System.Numerics;

namespace DataAccess.DAL.Repo
{
    public class PatientRepo : IPatientRepo
    {
        private readonly ApplicationDbContext _context;
        public PatientRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(PatientRequest patient)
        {
            try
            {
                Patient newPatient = CreatePatientProps(patient);
                CreateUser(patient);
                _context.Patients.Add(newPatient);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        private Patient CreatePatientProps(PatientRequest patient)
        {
            Patient newPatient = new Patient();

            newPatient.name = patient.Patient_Name;
            newPatient.mobileNumber = patient.mobileNumber;
            
            newPatient.CreatedBy = patient.UserId;
            newPatient.CreatedAt = DateTime.Now;
            newPatient.IsDeleted = false;
            newPatient.IsEnabled = true;

            int userID = _context.Users.SingleOrDefault(z => z.password == patient.mobileNumber)?.userId ?? 0;
            if (userID != 0)
            {
                newPatient.userId = userID;
                newPatient.user = _context.Users.Find(userID);
            }

            return newPatient;
        }
        private void CreateUser(PatientRequest patient)
        {
            User user = new User();
            user.userName = patient.Patient_Name;
            user.password = patient.mobileNumber;
            int userTypeId = _context.UserTypes.FirstOrDefault(z => z.name == "patient").typeId;
            user.userTypeId = userTypeId;
            user.CreatedBy = patient.UserId;
            user.CreatedAt = DateTime.Now;
            user.IsDeleted = false;
            user.IsEnabled = true;
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public IEnumerable<Patient> GetAll()
        {
            IEnumerable<Patient> patients = _context.Patients
                .Where(x => x.IsDeleted == false && x.IsEnabled == true)
                .ToList();

            return patients;
        }

        public Patient GetOne(GeneralRequest request)
        {
            var newPatient = _context.Patients.FirstOrDefault(z => z.patientId == request.Id);
            if (newPatient != null && newPatient.IsDeleted == false && newPatient.IsEnabled == true)
            {
                return newPatient;
            }
            else
            {
                return null;
            }
        }

        public bool Remove(GeneralRequest request)
        {
            try
            {
                var patient = _context.Patients.FirstOrDefault(z => z.patientId == request.Id);
                patient.IsDeleted = true;
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(PatientRequest patient)
        {
            var updatedPatient = _context.Doctors.Find(patient.Id);
            if (updatedPatient != null)
            {
                updatedPatient.name = patient.Patient_Name;
                updatedPatient.mobileNumber = patient.mobileNumber;

                updatedPatient.ModifiedAt = DateTime.Now;
                updatedPatient.ModifiedBy = patient.UserId;

                try
                {
                    _context.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
                return false;
        }
    }
}
