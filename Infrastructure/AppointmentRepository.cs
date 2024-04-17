using Application;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppointmentContext _appointmentContext;

        public AppointmentRepository(AppointmentContext appointmentContext)
        {
           _appointmentContext = appointmentContext;
        }

        public void Add(Appointments appointment)
        {

            _appointmentContext.Appointments.Add(appointment);
            _appointmentContext.SaveChanges();
        }

        public void Delete(int appointmentId)
        {

                var appointment = _appointmentContext.Appointments.Find(appointmentId);
                if (appointment != null)
                {
                    _appointmentContext.Appointments.Remove(appointment);
                    _appointmentContext.SaveChanges();
                }
        }

        public Appointments GetById(int appointmentId)
        {
            return _appointmentContext.Appointments.Find(appointmentId);
        }

        public IQueryable<Appointments> GetAll()
        {
           return _appointmentContext.Appointments.AsQueryable();
        }

    

        // Additional methods as needed
    }
}
