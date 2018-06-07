using System;
using Entities;

namespace BookingModuleService
{
    public class BookingRequestBuilder : IBookingRequestBuilder
    {
        public string MeetingData { get; set; }
        public string RequestData { get; set; }
        public Office Office { get; set; }

        public IBookingRequestBuilder AddMeetingData(string data)
        {
            MeetingData = data;
            return this;
        }

        public IBookingRequestBuilder AddRequestData(string data)
        {
            RequestData = data;
            return this;
        }

        public IBookingRequestBuilder AddOffice(Office office)
        {
            Office = office;
            return this;
        }

        public BookingRequest Build()
        {
            var meetingDataArray = MeetingData.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            var meeting = Helper.CreateMeeting($"{meetingDataArray[0]} {meetingDataArray[1]}", meetingDataArray[2]);

            var requestDataArray = RequestData.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            return new BookingRequest
            {
                SubmissionTime = DateTime.Parse($"{requestDataArray[0]} {requestDataArray[1]}"),
                EmployeeId = requestDataArray[2],
                Meeting = meeting,
                Office = Office
            };
        }

    }
}
