using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class CommandParser
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IAppointmentRepository _appointmentRepository;

        public CommandParser(IAppointmentService appointmentService, IAppointmentRepository appointmentRepository)
        {
            _appointmentService = appointmentService;
            _appointmentRepository = appointmentRepository;
        }

        public void Parse(string datetime, string inputCommand)
        {

            var parts = datetime.Split(' ');
            var date = DateTime.Parse(parts[0]);
            var time = TimeSpan.Parse(parts[1]);
            // Parse input and call appropriate service methods
            if (parts.Length != 2)
            {
                Console.WriteLine("Invalid command format. Use ADD DD/MM hh:mm");
            }

            if (string.IsNullOrWhiteSpace(inputCommand)) { return; }

            //var parts = inputCommand.Split(' ');
            var command = inputCommand.ToUpper();
            switch (command)
            {
                case "ADD":

                    _appointmentService.AddAppointment(date, time);
                    break;
                case "DELETE":
                    _appointmentService.DeleteAppointment(date, time);
                    break;
                case "FIND":
                    var findDate = DateTime.ParseExact(parts[0], "dd/MM", null);
                    var availableSlots = _appointmentService.FindAvailableSlots(findDate); ;
                    Console.WriteLine("Available slots for " + findDate.ToString("dd/MM") + ":");
                    foreach (var slot in availableSlots)
                    {
                        Console.WriteLine(slot.ToString("HH:mm"));
                    }

                    break;
                case "KEEP":
                    _appointmentService.KeepSlot(time);
                    break;
                default:
                    Console.WriteLine("Invalid command.");
                    break;
            }

        }
    }
}
