using Bongo.Core.Services.IServices;
using Bongo.Models.Model;
using Bongo.Models.Model.VM;
using Bongo.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bongo.Web
{
    [TestFixture]
    public class RoomBookingControllerTests
    {
        private Mock<IStudyRoomBookingService> _studyRoomBookingService;
        private RoomBookingController _bookingController;

        [SetUp]
        public void Setup()
        {
            _studyRoomBookingService = new Mock<IStudyRoomBookingService>();
            _bookingController = new RoomBookingController(_studyRoomBookingService.Object);
        }

        [Test]
        public void IndexPage_CallRequest_VerifyGetAllInvoked()
        {
            // Act
            _bookingController.Index();

            // Assert
            _studyRoomBookingService.Verify(x => x.GetAllBooking(), Times.Once);
        }

        // Test to check the details of view returned by the controller
        [Test]
        public void BookRoomCheck_ModelStateInvalid_ReturnView()
        {
            // Arrange
            _bookingController.ModelState.AddModelError("test", "test");

            // Act
            var result = _bookingController.Book(new StudyRoomBooking());
            ViewResult viewResult = result as ViewResult;

            // Assert
            ClassicAssert.AreEqual("Book", viewResult.ViewName);
        }

        [Test]
        public void BookRoomCheck_NotSuccessful_NoRoomCode()
        {
            // Arrange
            _studyRoomBookingService.Setup(x => x.BookStudyRoom(It.IsAny<StudyRoomBooking>()))
                .Returns(new StudyRoomBookingResult()
                {
                    Code = StudyRoomBookingCode.NoRoomAvailable
                });

            // Act
            var result = _bookingController.Book(new StudyRoomBooking());
            // Check that the retured result is type of view result
            ClassicAssert.IsInstanceOf<ViewResult>(result);
            ViewResult viewResult = result as ViewResult;

            // Assert

            ClassicAssert.AreEqual("No Study Room available for selected date", viewResult.ViewData["Error"]);
        }

        [Test]
        public void BookRoomCheck_Successful_SuccessCodeAndRedirect()
        {
            // Arrange
            _studyRoomBookingService.Setup(x => x.BookStudyRoom(It.IsAny<StudyRoomBooking>()))
                .Returns((StudyRoomBooking booking) => new StudyRoomBookingResult()
                {
                    Code = StudyRoomBookingCode.Success,
                    FirstName = booking.FirstName,
                    LastName = booking.LastName,
                    Date = booking.Date,
                    Email = booking.Email
                });

            // Act
            var result = _bookingController.Book(new StudyRoomBooking()
            {
                Date = DateTime.Now,
                Email = "hello@dotnetmastery.com",
                FirstName = "Hello",
                LastName = "DotNetMastery",
                StudyRoomId = 1
            });

            // Assert
            // Check that the retured result is type of view result
            ClassicAssert.IsInstanceOf<RedirectToActionResult>(result);
            RedirectToActionResult actionResult = result as RedirectToActionResult;
            ClassicAssert.AreEqual("Hello", actionResult.RouteValues["FirstName"]);
            ClassicAssert.AreEqual(StudyRoomBookingCode.Success, actionResult.RouteValues["Code"]);
        }
    }
}
