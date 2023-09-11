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
                //validation Befor the in sersion
                if (validateTheDoctorShift(request))
                {
                    addNewDoctorShift(request);
                    return true;
                }
                else
                {
                    //Set the new period date and time
                    //and add them
                    if (setTheNewPeriodDataAndTime(request))
                        return true;
                    else
                        return false;
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
            newDoctorShift.availableDaysOfweek =
                                        getAvailableDaysOfWeek(request);
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
            //while (currentTime.Hour <= request.toTime.Hour && currentTime.Minute <= request.toTime.Minute)
            while (currentTime.Hour < request.toTime.Hour 
                || (currentTime.Hour == request.toTime.Hour 
                && currentTime.Minute <= request.toTime.Minute))
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

            if (request.fromDate > request.toDate)
            {
                return daysOfWeek;
            }

            for (DateTime date = request.fromDate; date <= request.toDate; date = date.AddDays(1))
            {
                daysOfWeek += date.ToString("dddd") + ", ";
            }
            daysOfWeek = daysOfWeek.TrimEnd(',', ' ');

            return daysOfWeek;
        }
        private bool setTheNewPeriodDataAndTime(DoctorShiftRequest request)
        {
            try
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
                shift.availableDaysOfweek =
                    getAvailableDaysOfWeek(request);
                _context.DoctorShifts.Add(shift);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
           
        }
        #endregion

        public List<DoctorShift> GetAll()
        {

            List<DoctorShift> doctorShifts = _context.DoctorShifts
                .Where(x => x.IsDeleted == false &&
                            x.IsEnabled == true && x.isCancelled == false)
                    .Include(z => z.doctorShiftDays 
                        .Where(z => z.isCancelled == false &&
                                z.IsDeleted == false && z.IsEnabled == true)) 
                        .ThenInclude(z => z.doctorShiftDayTimes
                            .Where(z => z.isCancelled == false &&
                                    z.IsDeleted == false && z.IsEnabled == true))
                                .ToList();

            return doctorShifts;
        }

        public DoctorShift GetOne(GeneralRequest request)
        {
            var doctorShift = _context.DoctorShifts
                .Where(z => z.isCancelled == false &&
                        z.IsDeleted == false && z.IsEnabled == true)
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
            { return false; }
        }

        public bool cancellShift(GeneralRequest request)
        {
            try
            {
                var doctorShift = _context.DoctorShifts.FirstOrDefault(z => z.doctorShiftId == request.Id);
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
                var doctorShiftDay = _context.DoctorShiftDays.FirstOrDefault(z => z.doctorShiftDayId == request.Id);
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
                var doctorShiftDayTime = _context.DoctorShiftDayTimes.FirstOrDefault(z => z.doctorShiftDayTimeId == request.Id);
                doctorShiftDayTime.isCancelled = true;
                _context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

    }
}
