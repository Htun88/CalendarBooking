using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application
{
    public class AppointmentService: IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public void AddAppointment(DateTime date, TimeSpan startTime)
        {
            // Check if the appointment time is within acceptable hours
            if (!IsWithinWorkingHours(startTime))
            {
                Console.WriteLine("Appointments can only be scheduled between 9AM and 5PM.");
                return;
            }

            // Check if the slot is available
            if (!IsSlotAvailable(date, startTime))
            {
                Console.WriteLine("The specified time slot is not available.");
                return;
            }

            // Create new appointment
            var appointment = new Appointments
            {
                StartTime = new DateTime(date.Year, date.Month, date.Day, startTime.Hours, startTime.Minutes, 0),
                EndTime = new DateTime(date.Year, date.Month, date.Day, startTime.Hours, startTime.Minutes, 0).AddMinutes(30)
            };

            _appointmentRepository.Add(appointment);
            Console.WriteLine("Appointment added successfully.");
        }

        public void DeleteAppointment(DateTime date, TimeSpan startTime)
        {
            var appointmentToDelete = _appointmentRepository.GetAll()
                .FirstOrDefault(a => a.StartTime.Date == date.Date && a.StartTime.TimeOfDay == startTime);

            if (appointmentToDelete == null)
            {
                Console.WriteLine("No appointment found at the specified time.");
                return;
            }

            _appointmentRepository.Delete(appointmentToDelete.Id);
            Console.WriteLine("Appointment deleted successfully.");
        }


        public List<DateTime> FindAvailableSlots(DateTime date)
        {
            var workingHoursStart = new TimeSpan(9, 0, 0);
            var workingHoursEnd = new TimeSpan(17, 0, 0);
            var availableSlots = new List<DateTime>();

            var currentTime = date.Date + workingHoursStart;

            // Check availability between 9AM and 5PM
            for (int hour = 9; hour <= 16; hour++)
            {
                for (int minute = 0; minute < 60; minute += 30)
                {
                    var slot = new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
                    if (IsTimeValid(slot) && IsSlotAvailable(date, currentTime.TimeOfDay))
                    {
                        availableSlots.Add(slot);
                    }
                }
            }

            return availableSlots;
        }

        private bool IsTimeValid(DateTime date)
        {
            // Time must be between 9AM and 5PM
            return (date.Hour >= 9 && date.Hour < 17);
        }

        public void KeepSlot(TimeSpan startTime)
        {
            // Mark the specified time slot as unavailable for any day
            var today = DateTime.Today;
            var keepDate = today.Add(startTime);

            var appointment = new Appointments
            {
                StartTime = new DateTime(keepDate.Year, keepDate.Month, keepDate.Day, startTime.Hours, startTime.Minutes, 0),
                EndTime = new DateTime(keepDate.Year, keepDate.Month, keepDate.Day, startTime.Hours, startTime.Minutes, 0).AddMinutes(30)
            };

            _appointmentRepository.Add(appointment);
            Console.WriteLine($"The timeslot {startTime} has been kept.");
        }

        private bool IsSlotAvailable(DateTime date, TimeSpan startTime)
        {
            var existingAppointments = _appointmentRepository.GetAll()
                .Where(a => a.StartTime.Date == date.Date)
                .ToList();

            // Check if the specified time slot is between 9AM and 5PM
            if (!IsWithinWorkingHours(startTime))
                return false;

            // Check if the specified time slot is not reserved
            if (IsReservedTime(date, startTime))
                return false;

            // Check if there's already an appointment in the specified time slot
            return !existingAppointments.Any(a => a.StartTime.TimeOfDay == startTime);
        }

        private bool IsReservedTime(DateTime date, TimeSpan startTime)
        {
            // Check if the specified time slot falls between 4PM and 5PM on every second day of the third week
            if (date.DayOfWeek == DayOfWeek.Wednesday && (date.Day / 7) % 2 == 0 && startTime >= new TimeSpan(16, 0, 0))
                return true;

            return false;
        }

        private bool IsWithinWorkingHours(TimeSpan time)
        {
            var workingHoursStart = new TimeSpan(9, 0, 0);
            var workingHoursEnd = new TimeSpan(17, 0, 0);
            return time >= workingHoursStart && time <= workingHoursEnd;
        }
    }
}
