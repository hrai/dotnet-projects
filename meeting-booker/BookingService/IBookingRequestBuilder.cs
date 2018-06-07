using Entities;

namespace BookingModuleService
{
    public interface IBookingRequestBuilder
    {
        IBookingRequestBuilder AddMeetingData(string data);
        IBookingRequestBuilder AddRequestData(string data);
        IBookingRequestBuilder AddOffice(Office office);
        BookingRequest Build();
    }
}