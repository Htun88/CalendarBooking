using System;
using Xunit;
using Moq;
using Services;
using Models;
using DataAccess;
using System.Linq;

namespace Tests
{
    public class AppointmentServiceTests
    {
        [Fact]
        public void AddAppointment_ValidAppointment_AddedToRepository()
        {
            // Arrange
            var mockRepo = new Mock<AppointmentRepository>();
            var appointmentService = new AppointmentService(mockRepo.Object);
            var startTime = DateTime.Now;

            // Act
            appointmentService.AddAppointment(startTime);

            // Assert
            mockRepo.Verify(repo => repo.AddAppointment(It.Is<Appointment>(a => a.StartTime == startTime)), Times.Once);
        }

        
    }
}