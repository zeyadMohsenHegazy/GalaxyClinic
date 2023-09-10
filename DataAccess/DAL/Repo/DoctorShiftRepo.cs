using DataAccess.DAL.IRepo;
using DataAccess.DatabaseContext;
using Models.API.Request;
using Models.API.Request.ConfigRequest;
using Models.DomainModels;
using System;

namespace DataAccess.DAL.Repo
{
    public class DoctorShiftRepo : IDoctorShiftRepo
    {
        private readonly ApplicationDbContext _context;
        public DoctorShiftRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(DoctorShiftRequest request)
        {
            try
            {
                //validation Befor the in sersion
                if (validateTheDoctorShift(request))
                {
                    DoctorShift newDoctorShift = new DoctorShift();

                    newDoctorShift.doctorId = request.doctorId;
                    newDoctorShift.fromDate = request.fromDate;
                    newDoctorShift.toDate = request.toDate;
                    newDoctorShift.fromTime = request.fromTime;
                    newDoctorShift.toTime = request.toTime;
                    newDoctorShift.sessionDurationMinutes = request.sessionDuration;
                    newDoctorShift.shiftTitle = request.shiftTitle;
                    newDoctorShift.availableDaysOfweek =
                        getAvailableDaysOfWeek(request.fromDate, request.toDate);
                    _context.DoctorShifts.Add(newDoctorShift);
                    _context.SaveChanges();
                    return true;
                }
                //
                else
                {
                    // calculate the diffirance between the two periods.

                    // and add them
                    return false;

                }
            }
            catch
            {
                return false;
            }
        }
        private bool validateTheDoctorShift(DoctorShiftRequest request)
        {
            //to search for the Shift Title for the doctor
            var shift = _context.DoctorShifts
                .Where(z => z.shiftTitle.ToLower() == request.shiftTitle.ToLower())
                .FirstOrDefault(z => z.doctorId == request.doctorId);
            if (shift != null)
                return false;
            else
                return true;
        }
        private string getAvailableDaysOfWeek(DateTime fromDate, DateTime toDate)
        {
            string daysOfWeek = "";

            if (fromDate > toDate)
            {
                return daysOfWeek;
            }

            for (DateTime date = fromDate; date <= toDate; date = date.AddDays(1))
            {
                daysOfWeek += date.ToString("dddd") + ", ";
            }
            daysOfWeek = daysOfWeek.TrimEnd(',', ' ');

            return daysOfWeek;
        }
        private List<DateTime> getDays(DateTime fromDate, DateTime toDate)
        {
            List<DateTime> days = new List<DateTime>();
            if (fromDate > toDate)
            {
                return days;
            }
            for (DateTime date = fromDate; date <= toDate; date = date.AddDays(1))
            {
                days.Add(date);
            }
            return days;
        }
        private List<string> GetShiftDayTime(DateTime fromTime, DateTime toTime, int sessionDuration)
        {
            List<string> dayTimes = new List<string>();

            if (sessionDuration <= 0)
            {
                return dayTimes; 
            }

            DateTime currentTime = fromTime;
            while (currentTime <= toTime)
            {
                dayTimes.Add(currentTime.ToString("hh:mm tt"));
                currentTime = currentTime.AddMinutes(sessionDuration);
            }

            return dayTimes;
        }
        
        
        public IEnumerable<DoctorShift> GetAll()
        {
            IEnumerable<DoctorShift> doctorShifts = _context.DoctorShifts
                           .Where(x => x.IsDeleted == false && x.IsEnabled == true)
                           .ToList();

            return doctorShifts;
        }

        public DoctorShift GetOne(GeneralRequest request)
        {
            var doctorShift = _context.DoctorShifts.FirstOrDefault(z => z.doctorShiftId == request.Id);
            if (doctorShift != null && doctorShift.IsDeleted == false && doctorShift.IsEnabled == true)
            {
                return doctorShift;
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
                var doctorShift = _context.DoctorShifts.FirstOrDefault(z => z.doctorShiftId == request.Id);
                doctorShift.IsDeleted = true;
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool Update(DoctorShiftRequest request)
        {

        }


    }
}
