using Bongo.Core.Services;
using Bongo.DataAccess.Repository.IRepository;
using Bongo.Models.Model;
using Bongo.Models.Model.VM;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bongo.Core
{
    [TestFixture]
    public class StudyRoomBookingServiceTests
    {
        private StudyRoomBooking _request;
        private List<StudyRoom> _availableStudyRoom;
        private Mock<IStudyRoomBookingRepository> _studyRoomBookingRepoMock;
        private Mock<IStudyRoomRepository> _studyRoomRepoMock;
        private StudyRoomBookingService _bookingService;

        [SetUp]
        public void Setup()
        {
            _request = new StudyRoomBooking()
            {
                FirstName = "Ben",
                LastName = "Spark",
                Email = "ben@gmail.com",
                Date = new DateTime(2022, 1, 1)
            };

            _availableStudyRoom = new List<StudyRoom>
            {
                new StudyRoom
                {
                    Id = 10,
                    RoomName = "Michigan",
                    RoomNumber = "A202"
                }
            };

            _studyRoomBookingRepoMock = new Mock<IStudyRoomBookingRepository>();
            _studyRoomRepoMock = new Mock<IStudyRoomRepository>();
            // Mock the get all method
            _studyRoomRepoMock.Setup(x => x.GetAll()).Returns(_availableStudyRoom);
            _bookingService = new StudyRoomBookingService(_studyRoomBookingRepoMock.Object, _studyRoomRepoMock.Object);
        }

        [Test]
        public void GetAllBooking_InvokedMethod_CheckIfRepoIsCalled()
        {
            _bookingService.GetAllBooking();

            // Verify method is used to check how many times a mocked method is called
            _studyRoomBookingRepoMock.Verify(x => x.GetAll(null), Times.Once);
        }

        [TestCase]
        public void BookingException_NullRequest_ThrowsException()
        {
            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => _bookingService.BookStudyRoom(null));

            // Assert
            // Exception Message
            // ClassicAssert.AreEqual("Value cannot be null. (Parameter 'request')", exception?.Message);
            // Param Name - Better than checking the entire exception message
            ClassicAssert.AreEqual("request", exception?.ParamName);
        }

        [Test]
        public void StudyRoomBooking_SaveBookingWithAvailableRoom_ReturnsResultWithAllValues()
        {
            // Arrange
            StudyRoomBooking? savedStudyRoomBooking = null;
            _studyRoomBookingRepoMock.Setup(x => x.Book(It.IsAny<StudyRoomBooking>()))
                .Callback<StudyRoomBooking>(booking =>
                {
                    savedStudyRoomBooking = booking;
                });

            // Act
            var result = _bookingService.BookStudyRoom(_request);

            // Assert
            _studyRoomBookingRepoMock.Verify(x => x.Book(It.IsAny<StudyRoomBooking>()), Times.Once);

            ClassicAssert.NotNull(savedStudyRoomBooking);
            ClassicAssert.AreEqual(_request.FirstName, savedStudyRoomBooking.FirstName);
            ClassicAssert.AreEqual(_request.LastName, savedStudyRoomBooking.LastName);
            ClassicAssert.AreEqual(_request.Email, savedStudyRoomBooking.Email);
            ClassicAssert.AreEqual(_request.Date, savedStudyRoomBooking.Date);
            ClassicAssert.AreEqual(_availableStudyRoom.First().Id, savedStudyRoomBooking.StudyRoomId);
        }

        [Test]
        public void StudyRoomBookingResultCheck_InputRequest_ValueMatchInResult()
        {
            StudyRoomBookingResult result = _bookingService.BookStudyRoom(_request);

            ClassicAssert.NotNull(result);
            ClassicAssert.AreEqual(_request.FirstName, result.FirstName);
            ClassicAssert.AreEqual(_request.LastName, result.LastName);
            ClassicAssert.AreEqual(_request.Email, result.Email);
            ClassicAssert.AreEqual(_request.Date, result.Date);
        }

        [TestCase(false, ExpectedResult = StudyRoomBookingCode.NoRoomAvailable)]
        [TestCase(true, ExpectedResult = StudyRoomBookingCode.Success)]
        public StudyRoomBookingCode ResultCodeSuccess_RoomAvailability_ReturnSuccessResultCode(bool roomAvailability)
        {
            if (!roomAvailability)
            {
                _availableStudyRoom.Clear();
            }
            return _bookingService.BookStudyRoom(_request).Code;
        }

        //[TestCase(0, false)] // Better to move to separate test case
        [TestCase(55, true)]
        public void StudyRoomBooking_BookRoomWithAvailability_ReturnsBookingId(int expectedBookingId, bool roomAvailability)
        {
            // Arrange

            // Better to move to separate test case. See test case below this one
            //if (!roomAvailability)
            //{
            //    _availableStudyRoom.Clear();
            //}
            _studyRoomBookingRepoMock.Setup(x => x.Book(It.IsAny<StudyRoomBooking>()))
                .Callback<StudyRoomBooking>(booking =>
                {
                    booking.BookingId = expectedBookingId;
                });

            // Act
            var result = _bookingService.BookStudyRoom(_request);

            // Assert
            ClassicAssert.AreEqual(expectedBookingId, result.BookingId);
            // Better to move to separate test case. See test case below this one
            //if (!roomAvailability)
            //{
            //  _studyRoomBookingRepoMock.Verify(x => x.Book(It.IsAny<StudyRoomBooking>()), Times.Never);
            //}
        }

        [Test]
        public void BookNotInvoked_SaveBookingWithoutAvailableRoom_BookMethodNotInvoked()
        {
            // Arrange
            _availableStudyRoom.Clear();

            // Act
            var result = _bookingService.BookStudyRoom(_request);

            // Assert
            _studyRoomBookingRepoMock.Verify(x => x.Book(It.IsAny<StudyRoomBooking>()), Times.Never);
        }
    }
}
