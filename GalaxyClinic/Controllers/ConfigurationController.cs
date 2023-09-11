using DataAccess.Auto_Mapper;
using DataProvider.DataProvider.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.API.Request.ConfigRequest;
using Models.API.Request;
using Models.API.Response.ConfigResponse;
using Models.API.Response;
using Models.DomainModels;
using Azure.Core;

namespace GalaxyClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigDataProvider _dataProvider;
        public ConfigurationController(IConfigDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        #region Doctor

        [Route("~/Doctor/GetOneDoctor")]
        [HttpPost]
        public BaseResponse GetDoctor(GeneralRequest request)
        {
            BaseResponse response = new BaseResponse();

            try
            {
                var doctor = _dataProvider.doctorRepo.GetOne(request);

                if (doctor != null)
                {
                    var mapper = MapperConfig.InitializeAutoMapper();
                    var doctorResponse = mapper.Map<Doctor, DoctorResponse>(doctor);

                    response.Success = true;
                    response.StatusCode = "200";
                    response.Message = "Success";
                    response.Result = doctorResponse;
                }
                else
                {
                    response.Success = false;
                    response.StatusCode = "404";
                    response.Message = "Doctor Not Found";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = "500";
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }


        [Route("~/Doctor/GetAllDoctors")]
        [HttpGet]
        public BaseResponse GetAllDoctors()
        {
            BaseResponse response = new BaseResponse();
            try
            {
                IEnumerable<Doctor> doctors = _dataProvider.doctorRepo.GetAll();
                if (doctors != null)
                {
                    var mapper = MapperConfig.InitializeAutoMapper();
                    var doctorResponses = mapper.Map<IEnumerable<Doctor>, IEnumerable<DoctorResponse>>(doctors);
                    response.Success = true;
                    response.Message = "Retrieved Successfully";
                    response.StatusCode = "200";
                    response.Result = doctorResponses;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Doctors Not Found";
                    response.StatusCode = "404";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = "500";
                response.Message = $"An error occurred: {ex.Message}";
            }
            return response;
        }


        [Route("~/Doctor/AddDoctor")]
        [HttpPost]
        public BaseResponse AddDoctor(DoctorRequest doctor)
        {
            BaseResponse response = new BaseResponse();

            try
            {
                if (_dataProvider.doctorRepo.Add(doctor))
                {
                    response.Success = true;
                    response.Message = "Added";
                    response.StatusCode = "200";
                    response.Result = String.Empty;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Not Added";
                    response.StatusCode = "417";
                    response.Result = String.Empty;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = "500";
                response.Message = $"An error occurred: {ex.Message}";
            }
            return response;
        }


        [Route("~/Doctor/DeleteDoctor")]
        [HttpDelete]
        public BaseResponse DeleteDoctor(GeneralRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                if (_dataProvider.doctorRepo.Remove(request))
                {
                    response.Success = true;
                    response.Message = "Deleted Successfully";
                    response.StatusCode = "200";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Already Deleted";
                    response.StatusCode = "400";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = "500";
                response.Message = $"An error occurred: {ex.Message}";
            }
            return response;
        }


        [Route("~/Doctor/EditDoctor")]
        [HttpPost]
        public BaseResponse UpdateDoctor(DoctorRequest doctor)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                if (_dataProvider.doctorRepo.Update(doctor))
                {
                    response.Success = true;
                    response.Message = "Edited Successfully";
                    response.StatusCode = "200";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Not Found";
                    response.StatusCode = "404";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = "500";
                response.Message = $"An error occurred: {ex.Message}";
            }
            return response;
        }

        #endregion


        #region Doctor Shifts

        [Route("~/Doctor/AddDoctorShift")]
        [HttpPost]
        public BaseResponse AddDoctorSift([FromBody]DoctorShiftRequest request)
        {
            BaseResponse response = new BaseResponse();

            try
            {
                if (_dataProvider.doctorShiftRepo.Add(request))
                {
                    response.Success = true;
                    response.Message = "Added";
                    response.StatusCode = "200";
                    response.Result = String.Empty;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Not Added";
                    response.StatusCode = "417";
                    response.Result = String.Empty;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = "500";
                response.Message = $"An error occurred: {ex.Message}";
            }
            return response;
        }


        [Route("~/doctorShifts/getAllShifts")]
        [HttpPost]
        public BaseResponse getAllDoctorsShifts()
        {
            BaseResponse response = new BaseResponse();
            try
            {
                List<DoctorShift> shifts = _dataProvider.doctorShiftRepo.GetAll();
                if (shifts != null)
                {
                    var mapper = MapperConfig.InitializeAutoMapper();
                    List<DoctorShiftResponse> specialityResponses = mapper.Map<List<DoctorShift>, List<DoctorShiftResponse>>(shifts);
                    
                    response.Success = true;
                    response.Message = "Retrieved Successfully";
                    response.StatusCode = "200";
                    response.Result = specialityResponses;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Specialities Not Found";
                    response.StatusCode = "404";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = "500";
                response.Message = $"An error occurred: {ex.Message}";
            }
            return response;
        }


        [Route("~/doctorShifts/removeShift")]
        [HttpDelete]
        public BaseResponse removeShift(GeneralRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                if (_dataProvider.doctorShiftRepo.Remove(request))
                {
                    response.Success = true;
                    response.Message = "Deleted Successfully";
                    response.StatusCode = "200";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Already Deleted";
                    response.StatusCode = "400";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = "500";
                response.Message = $"An error occurred: {ex.Message}";
            }
            return response;
        }


        [Route("~/doctorShifts/cancellShift")]
        [HttpDelete]
        public BaseResponse cancelShift(GeneralRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                if (_dataProvider.doctorShiftRepo.cancellShift(request))
                {
                    response.Success = true;
                    response.Message = "Deleted Successfully";
                    response.StatusCode = "200";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Already Deleted";
                    response.StatusCode = "400";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = "500";
                response.Message = $"An error occurred: {ex.Message}";
            }
            return response;
        }


        [Route("~/doctorShifts/cancellShiftDay")]
        [HttpDelete]
        public BaseResponse cancelShiftDay(GeneralRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                if (_dataProvider.doctorShiftRepo.cancellShiftDay(request))
                {
                    response.Success = true;
                    response.Message = "Deleted Successfully";
                    response.StatusCode = "200";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Already Deleted";
                    response.StatusCode = "400";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = "500";
                response.Message = $"An error occurred: {ex.Message}";
            }
            return response;
        }


        [Route("~/doctorShifts/cancellShiftDayTime")]
        [HttpDelete]
        public BaseResponse cancelShiftDayTime(GeneralRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                if (_dataProvider.doctorShiftRepo.cancellShiftDayTime(request))
                {
                    response.Success = true;
                    response.Message = "Deleted Successfully";
                    response.StatusCode = "200";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Already Deleted";
                    response.StatusCode = "400";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = "500";
                response.Message = $"An error occurred: {ex.Message}";
            }
            return response;
        }

        #endregion


        #region Patient
        [Route("~/Patient/GetOnePatient")]
        [HttpPost]
        public BaseResponse GetOnePatient(GeneralRequest request)
        {
            BaseResponse response = new BaseResponse();

            try
            {
                var patient = _dataProvider.patientRepo.GetOne(request);

                if (patient != null)
                {
                    var mapper = MapperConfig.InitializeAutoMapper();
                    var patientResponse = mapper.Map<Patient, PatientResponse>(patient);

                    response.Success = true;
                    response.StatusCode = "200";
                    response.Message = "Success";
                    response.Result = patientResponse;
                }
                else
                {
                    response.Success = false;
                    response.StatusCode = "404";
                    response.Message = "Patient Not Found";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = "500";
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }


        [Route("~/Patient/GetAllPatients")]
        [HttpPost]
        public BaseResponse GetAll(GeneralRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                IEnumerable<Patient> patients = _dataProvider.patientRepo.GetAll();
                if (patients != null)
                {
                    var mapper = MapperConfig.InitializeAutoMapper();
                    var patientsResponses = mapper.Map<IEnumerable<Patient>, IEnumerable<PatientResponse>>(patients);
                    response.Success = true;
                    response.Message = "Retrieved Successfully";
                    response.StatusCode = "200";
                    response.Result = patientsResponses;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Patients Not Found";
                    response.StatusCode = "404";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = "500";
                response.Message = $"An error occurred: {ex.Message}";
            }
            return response;
        }


        [Route("~/Patient/DeletePatient")]
        [HttpDelete]
        public BaseResponse Delete(GeneralRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                if (_dataProvider.patientRepo.Remove(request))
                {
                    response.Success = true;
                    response.Message = "Deleted Successfully";
                    response.StatusCode = "200";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Already Deleted";
                    response.StatusCode = "400";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = "500";
                response.Message = $"An error occurred: {ex.Message}";
            }
            return response;
        }


        [Route("~/Patient/UpdatePatient")]
        [HttpPost]
        public BaseResponse EditPatient(PatientRequest patient)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                if (_dataProvider.patientRepo.Update(patient))
                {
                    response.Success = true;
                    response.Message = "Edited Successfully";
                    response.StatusCode = "200";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Not Found";
                    response.StatusCode = "404";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = "500";
                response.Message = $"An error occurred: {ex.Message}";
            }
            return response;
        }


        [Route("~/Patient/AddNewPatient")]
        [HttpPost]
        public BaseResponse AddDoctor(PatientRequest patient)
        {
            BaseResponse response = new BaseResponse();

            try
            {
                if (_dataProvider.patientRepo.Add(patient))
                {
                    response.Success = true;
                    response.Message = "Added";
                    response.StatusCode = "200";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Not Added";
                    response.StatusCode = "417";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = "500";
                response.Message = $"An error occurred: {ex.Message}";
            }
            return response;
        }
        #endregion


        #region Speciality

        [Route("~/Speciality/GetOne")]
        [HttpPost]
        public BaseResponse GetSpecilaity(SpecialityRequest request)
        {
            BaseResponse response = new BaseResponse();

            try
            {
                var speciality = _dataProvider.specialityRepo.GetOne(request);

                if (speciality != null)
                {
                    var mapper = MapperConfig.InitializeAutoMapper();
                    var specialityResponse = mapper.Map<Speciality, SpecialityResponse>(speciality);

                    response.Success = true;
                    response.StatusCode = "200";
                    response.Message = "Success";
                    response.Result = specialityResponse;
                }
                else
                {
                    response.Success = false;
                    response.StatusCode = "404";
                    response.Message = "Speciality Not Found";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = "500";
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }


        [Route("~/Speciality/GetAllSpecialities")]
        [HttpGet]
        public BaseResponse GetAllSpecialities()
        {
            BaseResponse response = new BaseResponse();
            try
            {
                IEnumerable<Speciality> specialities = _dataProvider.specialityRepo.GetAll();
                if (specialities != null)
                {
                    var mapper = MapperConfig.InitializeAutoMapper();
                    var specialityResponses = mapper.Map<IEnumerable<Speciality>, IEnumerable<SpecialityResponse>>(specialities);
                    response.Success = true;
                    response.Message = "Retrieved Successfully";
                    response.StatusCode = "200";
                    response.Result = specialityResponses;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Specialities Not Found";
                    response.StatusCode = "404";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = "500";
                response.Message = $"An error occurred: {ex.Message}";
            }
            return response;
        }


        [Route("~/Speciality/AddSpeciality")]
        [HttpPost]
        public BaseResponse AddDoctor(SpecialityRequest request)
        {
            BaseResponse response = new BaseResponse();

            try
            {
                if (_dataProvider.specialityRepo.Add(request))
                {
                    response.Success = true;
                    response.Message = "Added";
                    response.StatusCode = "200";
                    response.Result = String.Empty;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Not Added";
                    response.StatusCode = "417";
                    response.Result = String.Empty;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = "500";
                response.Message = $"An error occurred: {ex.Message}";
            }
            return response;
        }


        [Route("~/Speciality/deleteSpeciality")]
        [HttpDelete]
        public BaseResponse DeleteSpeciality(GeneralRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                if (_dataProvider.doctorRepo.Remove(request))
                {
                    response.Success = true;
                    response.Message = "Deleted Successfully";
                    response.StatusCode = "200";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Already Deleted";
                    response.StatusCode = "400";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = "500";
                response.Message = $"An error occurred: {ex.Message}";
            }
            return response;
        }


        [Route("~/Speciality/editSpeciality")]
        [HttpPost]
        public BaseResponse editSpeciality(SpecialityRequest speciality)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                if (_dataProvider.specialityRepo.Update(speciality))
                {
                    response.Success = true;
                    response.Message = "Edited Successfully";
                    response.StatusCode = "200";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Not Found";
                    response.StatusCode = "404";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = "500";
                response.Message = $"An error occurred: {ex.Message}";
            }
            return response;
        }
        #endregion


        #region Status

        #endregion
    }
}
