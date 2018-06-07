using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Entities;

namespace BookingModuleService
{
    public class BookingService
    {
        private readonly IList<BookingRequest> _bookingRequests;

        // Todo - This should implement a service IBookingService
        public BookingService()
        {
            _bookingRequests = new List<BookingRequest>();
        }

        public int TotalBookings => _bookingRequests.Count;
        public IEnumerable<Meeting> Meetings => _bookingRequests.Select(request => request.Meeting);

        public bool AddBooking(BookingRequest bookingRequest)
        {
            var requestMeeting = bookingRequest.Meeting;

            foreach (var existingBookingRequest in _bookingRequests)
            {
                if (existingBookingRequest.SubmissionTime == bookingRequest.SubmissionTime)
                    return  false;

                // Todo - Replace the meeting equality condition in if block 
                // can be replaced with .Equals() after overriding Equals()
                // GetHashCode() methods in Meeting class
                if (existingBookingRequest.Meeting.StartTime == requestMeeting.StartTime &&
                existingBookingRequest.Meeting.FinishTime == requestMeeting.FinishTime)
                {
                    var index = _bookingRequests.IndexOf(existingBookingRequest);
                    _bookingRequests[index] = bookingRequest;
                    return true;
                }
            }

            if (IsMeetingWithinOfficeHours(bookingRequest.Office, bookingRequest.Meeting) &&
            !MeetingOverlaps(bookingRequest.Meeting))
            {
                _bookingRequests.Add(bookingRequest);
                return true;
            }

            return false;
        }

        public IDictionary<DateTime, IEnumerable<BookingRequest>> CreateBookings(string fileName)
        {
            var textLines = File.ReadAllLines(fileName);
            if (textLines.Any())
            {
                var officeHours = textLines[0];
                var office = Helper.CreateOffice(officeHours);

                for (int i = 1; i < textLines.Length; i++)
                {
                    var requestData = textLines[i];
                    var meetingData = textLines[++i];

                    // Todo - Ideally, this should be replaced with IoC containers like Ninject 
                    // so the dependency can be injected
                    var builder = new BookingRequestBuilder();
                    builder.AddMeetingData(meetingData);
                    builder.AddRequestData(requestData);
                    builder.AddOffice(office);

                    var bookingRequest = builder.Build();
                    AddBooking(bookingRequest);
                }

                return _bookingRequests.GroupBy(bookingRequest => bookingRequest.Meeting.StartTime.Date)
                .ToDictionary(group => group.Key,
                 group => GetOrderedMeetings(group.ToList()));
            }

            return new Dictionary<DateTime, IEnumerable<BookingRequest>>();
        }

        private static IEnumerable<BookingRequest> GetOrderedMeetings(IEnumerable<BookingRequest> bookingRequests)
        {
            return bookingRequests.OrderBy(booking => booking.Meeting.StartTime)
                .ThenBy(booking => booking.Meeting.FinishTime)
                .ToList();
        }

        public bool IsMeetingWithinOfficeHours(Office office, Meeting meeting)
        {
            return office.StartTime.TimeOfDay <= meeting.StartTime.TimeOfDay && office.FinishTime.TimeOfDay >= meeting.FinishTime.TimeOfDay;
        }

        public bool MeetingOverlaps(Meeting newMeeting)
        {
            var meetings = _bookingRequests.Select(req => req.Meeting);

            if (meetings.Any(existingMeeting => NewMeetingStartTimeOverlaps(existingMeeting, newMeeting) ||
            NewMeetingFinishTimeOverlaps(existingMeeting, newMeeting)))
                return true;

            return false;
        }

        private bool NewMeetingFinishTimeOverlaps(Meeting existingMeeting, Meeting newMeeting)
        {
            return MeetingsAreOnSameDate(existingMeeting, newMeeting) &&
            existingMeeting.StartTime >= newMeeting.StartTime &&
            existingMeeting.StartTime < newMeeting.FinishTime;
        }

        private bool MeetingsAreOnSameDate(Meeting existingMeeting, Meeting newMeeting)
        {
            return existingMeeting.StartTime.Date == newMeeting.StartTime.Date;
        }

        private bool NewMeetingStartTimeOverlaps(Meeting existingMeeting, Meeting newMeeting)
        {
            return MeetingsAreOnSameDate(existingMeeting, newMeeting) &&
            existingMeeting.StartTime <= newMeeting.StartTime &&
            existingMeeting.FinishTime > newMeeting.StartTime;
        }
    }
}
