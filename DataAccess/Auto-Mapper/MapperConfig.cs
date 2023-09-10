using AutoMapper;
using Models.API.Request.ConfigRequest;
using Models.API.Response.ConfigResponse;
using Models.DomainModels;

namespace DataAccess.Auto_Mapper
{
    public class MapperConfig
    {
        public static Mapper InitializeAutoMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<DoctorRequest, Doctor>()
                    .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Doctor_Name))
                    .ForMember(dest => dest.doctorId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.mobileNumber, opt => opt.MapFrom(src => src.mobileNumber))
                    .ForPath(dest => dest.speciality.specialityId, opt => opt.MapFrom(src => src.Speciality_Code));
                cfg.CreateMap<Doctor, DoctorResponse>()
                .ForMember(dest => dest.Doctor_Name, opt => opt.MapFrom(src => src.name))
                    .ForMember(dest => dest.Doctor_Code, opt => opt.MapFrom(src => src.doctorId))
                    .ForMember(dest => dest.mobileNumber, opt => opt.MapFrom(src => src.mobileNumber))
                    .ForPath(dest => dest.Speciality_Name, opt => opt.MapFrom(src => src.speciality.name));
                cfg.CreateMap<PatientRequest, Patient>()
                    .ForMember(dest => dest.patientId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.mobileNumber, opt => opt.MapFrom(src => src.mobileNumber))
                    .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Patient_Name));
                cfg.CreateMap<Patient, PatientResponse>()
                    .ForMember(dest => dest.patient_Code, opt => opt.MapFrom(src => src.patientId))
                    .ForMember(dest => dest.mobileNumber, opt => opt.MapFrom(src => src.mobileNumber))
                    .ForMember(dest => dest.Patient_Name, opt => opt.MapFrom(src => src.name));
                cfg.CreateMap<UserRequest, User>()
                    .ForMember(dest => dest.userId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.userName, opt => opt.MapFrom(src => src.User_Name))
                    .ForMember(dest => dest.password, opt => opt.MapFrom(src => src.User_Password));
                cfg.CreateMap<User, UserResponse>()
                    .ForMember(dest => dest.User_Code, opt => opt.MapFrom(src => src.userId))
                    .ForMember(dest => dest.User_Name, opt => opt.MapFrom(src => src.userName))
                    .ForMember(dest => dest.User_Password, opt => opt.MapFrom(src => src.password));
                cfg.CreateMap<Speciality, SpecialityResponse>()
                    .ForMember(dest => dest.Speciality_Code, opt => opt.MapFrom(src => src.specialityId))
                    .ForMember(dest => dest.Speciality_Name, opt => opt.MapFrom(src => src.name));
                cfg.CreateMap<SpecialityRequest, Speciality>()
                    .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.specialityName))
                    .ForMember(dest => dest.specialityId, opt => opt.MapFrom(src => src.Id));

            });

            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
