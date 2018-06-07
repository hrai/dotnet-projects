using System;
using System.IO;
using BookingModuleService;

namespace ConsoleMeetingBooker
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var fileName = Path.Combine(Directory.GetCurrentDirectory(), "meetings.txt");

                if (File.Exists(fileName))
                {
                    // Todo - use IoC library like Ninject to inject an instance
                    var bookingService = new BookingService();
                    var dayBookings = bookingService.CreateBookings(fileName);
                    const string timeFormat = "HH:mm";
                    const string dateFormat = "yyyy-MM-dd";

                    foreach (var key in dayBookings.Keys)
                    {
                        var bookings = dayBookings[key];

                        Console.WriteLine(key.ToString(dateFormat));
                        foreach (var booking in bookings)
                        {
                            var meeting = booking.Meeting;

                            Console.WriteLine($"{meeting.StartTime.ToString(timeFormat)} {meeting.FinishTime.ToString(timeFormat)}");
                            Console.WriteLine(booking.EmployeeId);
                        };
                    }
                }
                else
                {
                    Console.WriteLine("The input file doesn't exist.");
                }
            }
            catch (Exception)
            {
                // Todo - Log error at a global level printing out the stacktrace
                Console.WriteLine("Something went wrong!!");
            }

            Console.ReadKey();
        }
    }
}
