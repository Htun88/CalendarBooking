using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IAppointmentService
    {
        void AddAppointment(DateTime date, TimeSpan startTime);
        void DeleteAppointment(DateTime date, TimeSpan startTime);
        List<DateTime> FindAvailableSlots(DateTime date);
        void KeepSlot(TimeSpan startTime);
    }
}
