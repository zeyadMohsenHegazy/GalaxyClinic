using Azure.Core;
using DataAccess.DAL.IRepo;
using DataAccess.DatabaseContext;
using Microsoft.EntityFrameworkCore;
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
                //validation Befor the in sertion
                if (validateTheDoctorShift(request))
                {
                    addNewDoctorShift(request);
                    return true;
                }
                else
                {
                    //Set the new period date and time
                    //and add them
                    setTheNewPeriodDataAndTime(request);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        //Adds New Doctor Shift
        private void addNewDoctorShift(DoctorShiftRequest request)
        {
            DoctorShift newDoctorShift = new DoctorShift();

            newDoctorShift.doctorId = request.doctorId;
            newDoctorShift.fromDate = request.fromDate;
            newDoctorShift.toDate = request.toDate;
            newDoctorShift.fromTime = request.fromTime;
            newDoctorShift.toTime = request.toTime;
            newDoctorShift.sessionDurationMinutes = request.sessionDuration;
            newDoctorShift.shiftTitle = request.shiftTitle;
            newDoctorShift.availableDaysOfweek = getAvailableDaysOfWeek(request);
            newDoctorShift.CreatedAt = DateTime.Now;
            newDoctorShift.CreatedBy = request.UserId;
            newDoctorShift.IsEnabled = true;
            newDoctorShift.isCancelled = false;
            newDoctorShift.IsDeleted = false;
            _context.DoctorShifts.Add(newDoctorShift);
            _context.SaveChanges();
            request.Id = newDoctorShift.doctorShiftId;
            addNewDoctorShiftDays(request);
        }
        //Adds New Doctor Shift Days 
        private void addNewDoctorShiftDays(DoctorShiftRequest request)
        {

            for (DateTime date = request.fromDate; date <= request.toDate; date = date.AddDays(1))
            {
                doctorShiftDay doctorShiftDays = new doctorShiftDay();

                doctorShiftDays.days = date;
                doctorShiftDays.doctorShiftId = request.Id;
                doctorShiftDays.weekDays = date.ToString("dddd");

                doctorShiftDays.isCancelled = false;
                doctorShiftDays.IsDeleted = false;
                doctorShiftDays.IsEnabled = true;
                doctorShiftDays.CreatedBy = request.UserId;
                doctorShiftDays.CreatedAt = DateTime.Now;

                _context.DoctorShiftDays.Add(doctorShiftDays);
                _context.SaveChanges();

                request.doctorShiftDayId = doctorShiftDays.doctorShiftDayId;
                addNewDoctorShiftDayTimes(request);
            }
        }
        //Adds New Doctor Shift Day Times 
        private void addNewDoctorShiftDayTimes(DoctorShiftRequest request)
        {
            
            DateTime currentTime = request.fromTime;
            while (currentTime.Ticks < request.toTime.Ticks)
            {
                doctorShiftDayTime shiftDayTime = new doctorShiftDayTime();
                shiftDayTime.doctorShiftDayId = request.doctorShiftDayId;
                shiftDayTime.fromTime = currentTime;
                shiftDayTime.toTime = currentTime.AddMinutes(request.sessionDuration);
                shiftDayTime.isCancelled = false;
                shiftDayTime.IsEnabled = true;
                shiftDayTime.IsDeleted = false;
                shiftDayTime.CreatedAt = DateTime.Now;
                shiftDayTime.CreatedBy = request.UserId;
                _context.DoctorShiftDayTimes.Add(shiftDayTime);
                _context.SaveChanges();
                //the counter
                currentTime = currentTime.AddMinutes(request.sessionDuration);
            }

        }

        #region Doctor Shift Table Helpers
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
        private string getAvailableDaysOfWeek(DoctorShiftRequest request)
        {
            string daysOfWeek = "";
            List<string> uniqueDays = new List<string>();

            if (request.fromDate > request.toDate)
            {
                return daysOfWeek;
            }

            for (DateTime date = request.fromDate; date <= request.toDate; date = date.AddDays(1))
            {
                string weekday = date.ToString("dddd");
                if (!uniqueDays.Contains(weekday))
                {
                    uniqueDays.Add(weekday);
                    daysOfWeek += weekday + ", ";
                }
            }

            daysOfWeek = daysOfWeek.TrimEnd(',', ' ');

            return daysOfWeek;
        }
        private void setTheNewPeriodDataAndTime(DoctorShiftRequest request)
        {
             DoctorShift shift = _context.DoctorShifts
                .Where(z => z.shiftTitle.ToLower() == request.shiftTitle.ToLower())
                .FirstOrDefault(z => z.doctorId == request.doctorId);

             shift.fromDate = request.fromDate;
             shift.toDate = request.toDate;
             shift.fromTime = request.fromTime;
             shift.toTime = request.toTime;
             shift.sessionDurationMinutes = request.sessionDuration;
             shift.shiftTitle = request.shiftTitle;
             shift.availableDaysOfweek = getAvailableDaysOfWeek(request);
             _context.DoctorShifts.Add(shift);
             _context.SaveChanges();

            //cancell the old values in the doctor shift days
            //And doctor shift day times
            List<int> Ids = cacellOldValuesForDoctorShiftDay(request);
            cacellOldValuesForDoctorShiftDayTimes(Ids);

            //Now Creates the values from the begining
            //To create a new days and day times 
            addNewDoctorShiftDays(request);
        }
        private List<int> cacellOldValuesForDoctorShiftDay(DoctorShiftRequest request)
        {
            List<int> docShiftDayIds = new List<int>();
            List<doctorShiftDay> _doctorShiftDays =
                _context.DoctorShiftDays.Where(z => z.doctorShiftId == request.Id).ToList();
            foreach (var shiftDay in _doctorShiftDays)
            {
                shiftDay.isCancelled = true;
                docShiftDayIds.Add(shiftDay.doctorShiftDayId);
            }
            return docShiftDayIds;
        }

        private void cacellOldValuesForDoctorShiftDayTimes(List<int> ids)
        {
            foreach (var item in ids)
            {
                _context.DoctorShiftDayTimes
                    .Where(z => z.doctorShiftDayId == item)
                    .FirstOrDefault().isCancelled = true;

            }
        }        
        #endregion

        public List<DoctorShift> getAllShifts()
        {
            List<DoctorShift> doctorShifts = _context.DoctorShifts
                .Where(x => x.IsDeleted == false &&
                            x.IsEnabled == true && x.isCancelled == false)
                    .Include(z => z.doctor)
                    .Include(z => z.doctorShiftDays 
                        .Where(z => z.isCancelled == false &&
                                z.IsDeleted == false && z.IsEnabled == true)) 
                        .ThenInclude(z => z.doctorShiftDayTimes
                            .Where(z => z.isCancelled == false &&
                                    z.IsDeleted == false && z.IsEnabled == true))
                                .ToList();

            return doctorShifts;
        }

        public DoctorShift getDoctorActiveShifts(GeneralRequest request)
        {
            var doctorShifts = _context.DoctorShifts
                .Where(z => z.isCancelled == false &&
                        z.IsDeleted == false && z.IsEnabled == true)
                 .Include(z => z.doctorShiftDays
                        .Where(z => z.isCancelled == false &&
                                z.IsDeleted == false && z.IsEnabled == true))
                        .ThenInclude(z => z.doctorShiftDayTimes
                            .Where(z => z.isCancelled == false &&
                                    z.IsDeleted == false && z.IsEnabled == true))
                .FirstOrDefault(z => z.doctorId == request.Id);
          
            if (doctorShifts != null)
            {
                return doctorShifts;
            }
            else
            {
                return null;
            }
        }
        //get with the doctor id
        public DoctorShift getDoctorAllShifts(GeneralRequest request)
        {
            var doctorShifts = _context.DoctorShifts
                .Where(z => z.IsDeleted == false && z.IsEnabled == true)
                    .Include(z => z.doctorShiftDays
                        .Where(z => z.isCancelled == false &&
                                z.IsDeleted == false && z.IsEnabled == true))
                        .ThenInclude(z => z.doctorShiftDayTimes
                            .Where(z => z.isCancelled == false &&
                                    z.IsDeleted == false && z.IsEnabled == true))
                .FirstOrDefault(z => z.doctorId == request.Id);

            if (doctorShifts != null)
            {
                return doctorShifts;
            }
            else
            {
                return null;
            }
        }

        //gets by the shift id 
        public DoctorShift getDoctorShift(GeneralRequest request)
        {
            var doctorShift = _context.DoctorShifts
                .Where(z => z.isCancelled == false &&
                        z.IsDeleted == false && z.IsEnabled == true)
                 .Include(z => z.doctor)
                 .Include(z => z.doctorShiftDays
                        .Where(z => z.isCancelled == false &&
                                z.IsDeleted == false && z.IsEnabled == true))
                        .ThenInclude(z => z.doctorShiftDayTimes
                            .Where(z => z.isCancelled == false &&
                                    z.IsDeleted == false && z.IsEnabled == true))
                .FirstOrDefault(z => z.doctorShiftId == request.Id);
            if (doctorShift != null)
            {
                return doctorShift;
            }
            else
            {
                return null;
            }
        }

        public bool removeDoctorShift(GeneralRequest request)
        {
            try
            {
                var doctorShift = _context.DoctorShifts
                    .FirstOrDefault(z => z.doctorShiftId == request.Id);
                doctorShift.IsDeleted = true;
                _context.SaveChanges();
                return true;
            }
            catch
            { return false; }
        }

        public bool cancellShift(GeneralRequest request)
        {
            try
            {
                var doctorShift = _context.DoctorShifts
                    .FirstOrDefault(z => z.doctorShiftId == request.Id);
                doctorShift.isCancelled = true;
                _context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool cancellShiftDay(GeneralRequest request)
        {
            try
            {
                var doctorShiftDay = _context.DoctorShiftDays
                    .FirstOrDefault(z => z.doctorShiftDayId == request.Id);
                doctorShiftDay.isCancelled = true;
                _context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool cancellShiftDayTime(GeneralRequest request)
        {
            try
            {
                var doctorShiftDayTime = _context.DoctorShiftDayTimes
                    .FirstOrDefault(z => z.doctorShiftDayTimeId == request.Id);
                doctorShiftDayTime.isCancelled = true;
                _context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public List<doctorShiftDayTime> getAlltheShiftDayTimes(GeneralRequest request)
        {
            var shiftDayTimes = _context.DoctorShiftDayTimes
                .Where(z => z.doctorShiftDayId == request.Id)
                .ToList();
            if(shiftDayTimes.Count != 0)
            {
                return shiftDayTimes;
            }
            else
            {
                return null;
            }

        }

    }
}
