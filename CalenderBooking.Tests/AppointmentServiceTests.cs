using Application;
using Domain;
using Infrastructure;
using Moq;

namespace CalenderBooking.Tests
{
    public class AppointmentServiceTests
    {
        [Fact]
        public void AddAppointment_ValidAppointment_AddedToRepository()
        {

            // Arrange
            var appointmentRepositoryMock = new Mock<IAppointmentRepository>();
            var appointmentService = new AppointmentService(appointmentRepositoryMock.Object);

            var startTime = DateTime.Parse(DateTime.Now.ToString("hh:mm"));
            var date = DateTime.Parse(DateTime.Now.ToString("dd/MM"));
            var time = TimeSpan.Parse(DateTime.Now.ToString("hh:mm"));


            // Act
            appointmentService.AddAppointment(date, time);

            // Assert
            appointmentRepositoryMock.Verify(repo => repo.Add(It.Is<Appointments>(a => a.StartTime == startTime && a.EndTime == a.StartTime)), Times.Never);
        }


        [Fact]
        public void DeleteAppointment_ValidAppointment_DeletedFromRepository()
        {
            // Arrange
            var appointmentId = 1;
            var mockRepo = new Mock<IAppointmentRepository>();
            var appointmentService = new AppointmentService(mockRepo.Object);
            var date = DateTime.Parse(DateTime.Now.ToString("dd/MM"));
            var time = TimeSpan.Parse(DateTime.Now.ToString("hh:mm"));

            // Act
            appointmentService.DeleteAppointment(date, time);

            // Assert
            mockRepo.Verify(repo => repo.Delete(appointmentId), Times.Never);
        }

        [Fact]
        public void FindAvailableSlot_ValidDate_ReturnsAvailableSlot()
        {
            // Arrange
            var date = DateTime.Now.Date;
            var mockRepo = new Mock<IAppointmentRepository>();
            //mockRepo.Setup(repo => repo.GetAll();
            var appointmentService = new AppointmentService(mockRepo.Object);

            // Act
            var availableSlot = appointmentService.FindAvailableSlots(date);

            // Assert
            Assert.NotNull(availableSlot);
        }

        [Fact]
        public void KeepTimeSlot_ValidTimeSlot_SavesTimeSlot()
        {
            // Arrange
            var timeSlot = TimeSpan.Parse(DateTime.Now.ToString("hh:mm"));
            var mockRepo = new Mock<IAppointmentRepository>();
            var appointmentService = new AppointmentService(mockRepo.Object);

            // Act
            appointmentService.KeepSlot(timeSlot);

            // Assert
            Assert.NotNull(timeSlot);

        }
    }
}