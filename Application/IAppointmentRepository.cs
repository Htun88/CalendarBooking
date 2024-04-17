using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IAppointmentRepository
    {
        void Add(Appointments appointment);
        void Delete(int appointmentId);
        Appointments GetById(int appointmentId);
        IQueryable<Appointments> GetAll();
    }
}
