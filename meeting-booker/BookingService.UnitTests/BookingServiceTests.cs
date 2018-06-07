using System;
using Entities;
using FluentAssertions;
using NUnit.Framework;

namespace BookingModuleService.UnitTests
{
    [TestFixture]
    public class BookingServiceTests
    {
        private Office _office;
        private BookingModuleService.BookingService _bookingService;

        [SetUp]
        public void Setup()
        {
            // Todo - use mocking framework like Moq to use mocks instead of 
            // actual instance of classes
            _bookingService = new BookingModuleService.BookingService();

            _office = Helper.CreateOffice("0900 1800");
        }

        [Test]
        public void IsMeetingWithinOfficeHours_ReturnsTrue_WhenTheMeetingIsWithinOfficeHours()
        {
            var meeting = Helper.CreateMeeting("2015-04-12 09:00:00", "2.5");

            _bookingService.IsMeetingWithinOfficeHours(_office, meeting).Should().BeTrue();
        }

        [Test]
        public void IsMeetingWithinOfficeHours_ReturnsTrue_WhenTheMeetingIsBeforeOfficeHours()
        {
            var meeting = GetMeetingWithStartTimeBeforeOfficeHours();

            _bookingService.IsMeetingWithinOfficeHours(_office, meeting).Should().BeFalse();
        }

        [Test]
        public void IsMeetingWithinOfficeHours_ReturnsTrue_WhenTheMeetingIsAfterOfficeHours()
        {
            var meeting = GetMeetingWithFinishTimeAfterOfficeHours();

            _bookingService.IsMeetingWithinOfficeHours(_office, meeting).Should().BeFalse();
        }

        [Test]
        public void MeetingOverlaps_ReturnsFalse_WhenTheMeetingDoesntOverlap()
        {
            AddMeetingsToService();

            var meeting = Helper.CreateMeeting("2015-04-12 09:00", "1.5");
            _bookingService.MeetingOverlaps(meeting).Should().BeFalse();
        }

        [Test]
        public void MeetingOverlaps_ReturnsTrue_WhenTheMeetingFinishTimeOverlaps()
        {
            AddMeetingsToService();

            var meeting = Helper.CreateMeeting("2015-04-12 09:30", "3.5");
            _bookingService.MeetingOverlaps(meeting).Should().BeTrue();
        }

        [Test]
        public void MeetingOverlaps_ReturnsTrue_WhenTheMeetingStartTimeOverlaps()
        {
            AddMeetingsToService();

            var meeting = Helper.CreateMeeting("2015-04-12 10:40", "1.5");
            _bookingService.MeetingOverlaps(meeting).Should().BeTrue();
        }

        [Test]
        public void AddBooking_AddsBooking_WhenTheSubmissionTimeIsUnique()
        {
            AddMeetingsToService();
            var totalBookings = _bookingService.TotalBookings;

            var booking = new BookingRequest
            {
                SubmissionTime = new DateTime().AddHours(.01),
                EmployeeId = "EMP02",
                Office = _office,
                Meeting = Helper.CreateMeeting("2015-04-11 10:40", "1.5")
            };

            _bookingService.AddBooking(booking);

            totalBookings.Should().Be(_bookingService.TotalBookings - 1);
        }

        [Test]
        public void AddBooking_DoesntAddBooking_WhenTheSubmissionTimeIsNotUnique()
        {
            AddMeetingsToService();
            var totalBookings = _bookingService.TotalBookings;

            var booking = new BookingRequest
            {
                SubmissionTime = DateTime.MinValue,
                EmployeeId = "EMP02",
                Office = _office,
                Meeting = Helper.CreateMeeting("2015-04-11 10:40", "1.5")
            };

            _bookingService.AddBooking(booking);

            totalBookings.Should().Be(_bookingService.TotalBookings);
        }

        [Test]
        public void AddBooking_ReplacesExistingBooking_IfCurrentBookingHasSameStartAndFinishTime()
        {
            AddMeetingsToService();
            var totalBookings = _bookingService.TotalBookings;

            var booking = new BookingRequest
            {
                SubmissionTime = DateTime.Now,
                EmployeeId = "EMP02",
                Office = _office,
                Meeting = Helper.CreateMeeting("2015-04-12 12:40", "0.5")
            };

            var bookingSuccessful = _bookingService.AddBooking(booking);

            totalBookings.Should().Be(_bookingService.TotalBookings);
            bookingSuccessful.Should().BeTrue();
        }

        private void AddMeetingsToService()
        {
            _bookingService.AddBooking(new BookingRequest { SubmissionTime = DateTime.MinValue, EmployeeId = "EMP01", Office = _office, Meeting = Helper.CreateMeeting("2015-04-12 10:40", "1.5") });
            _bookingService.AddBooking(new BookingRequest { SubmissionTime = DateTime.Now.AddSeconds(1), EmployeeId = "EMP01", Office = _office, Meeting = Helper.CreateMeeting("2015-04-12 12:40", "0.5") });
            _bookingService.AddBooking(new BookingRequest { SubmissionTime = DateTime.Now.AddSeconds(2), EmployeeId = "EMP01", Office = _office, Meeting = Helper.CreateMeeting("2015-04-12 13:30", "1") });
            _bookingService.AddBooking(new BookingRequest { SubmissionTime = DateTime.Now.AddSeconds(3), EmployeeId = "EMP01", Office = _office, Meeting = Helper.CreateMeeting("2015-04-12 14:30", "1.15") });
        }

        private static Meeting GetMeetingWithFinishTimeAfterOfficeHours()
        {
            var meeting = Helper.CreateMeeting("2015-04-12 09:40", "11");
            return meeting;
        }

        private Meeting GetMeetingWithStartTimeBeforeOfficeHours()
        {
            var meeting = Helper.CreateMeeting("2015-04-12 06:40", "1.5");
            return meeting;
        }
    }
}
