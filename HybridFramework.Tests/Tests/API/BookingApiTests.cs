using NUnit.Framework;
using HybridFramework.Tests.API.Endpoints;
using HybridFramework.Tests.API.Models;

namespace HybridFramework.Tests.Tests.API
{
    [TestFixture]
    public class BookingApiTests
    {
        private AuthApi _authApi;
        private BookingApi _bookingApi;
        private string _token;

        [SetUp]
        public async Task SetUp()
        {
            _authApi = new AuthApi();
            _token = await _authApi.GetAccessTokenAsync();
            _bookingApi = new BookingApi(_token);
        }

        [Test, Order(1)]
        public async Task CreateBooking_WithValidData_ReturnsBookingId()
        {
            var booking = new BookingRequest
            {
                FirstName = "Akhil",
                LastName = "Kotte",
                TotalPrice = 500,
                DepositPaid = true,
                BookingDates = new BookingDates
                {
                    CheckIn = "2026-06-01",
                    CheckOut = "2026-06-05"
                },
                AdditionalNeeds = "Breakfast"
            };

            var response = await _bookingApi.CreateBookingAsync(booking);

            Assert.That((int)response.StatusCode, Is.EqualTo(200));
            Assert.That(response.Data.BookingId, Is.GreaterThan(0));
            Console.WriteLine($"[TEST] Created Booking ID: {response.Data.BookingId}");
        }

        [Test, Order(2)]
        public async Task GetAllBookings_ReturnsSuccessStatusCode()
        {
            var response = await _bookingApi.GetAllBookingsAsync();

            Assert.That((int)response.StatusCode, Is.EqualTo(200));
            Console.WriteLine("[TEST] Get all bookings passed");
        }

        [Test, Order(3)]
        public async Task GetBookingById_WithValidId_ReturnsBookingDetails()
        {
            var booking = new BookingRequest
            {
                FirstName = "Test",
                LastName = "User",
                TotalPrice = 300,
                DepositPaid = false,
                BookingDates = new BookingDates
                {
                    CheckIn = "2026-07-01",
                    CheckOut = "2026-07-03"
                },
                AdditionalNeeds = "Lunch"
            };

            var created = await _bookingApi.CreateBookingAsync(booking);
            int bookingId = created.Data.BookingId;

            var response = await _bookingApi.GetBookingByIdAsync(bookingId);

            Assert.That((int)response.StatusCode, Is.EqualTo(200));
            Assert.That(response.Data.FirstName, Is.EqualTo("Test"));
            Console.WriteLine($"[TEST] Got booking: {response.Data.FirstName} {response.Data.LastName}");
        }

        [Test, Order(4)]
        public async Task UpdateBooking_WithValidData_ReturnsUpdatedBooking()
        {
            var original = new BookingRequest
            {
                FirstName = "Original",
                LastName = "Name",
                TotalPrice = 100,
                DepositPaid = true,
                BookingDates = new BookingDates
                {
                    CheckIn = "2026-08-01",
                    CheckOut = "2026-08-03"
                },
                AdditionalNeeds = "None"
            };

            var created = await _bookingApi.CreateBookingAsync(original);
            int bookingId = created.Data.BookingId;

            var updated = new BookingRequest
            {
                FirstName = "Updated",
                LastName = "Name",
                TotalPrice = 200,
                DepositPaid = true,
                BookingDates = new BookingDates
                {
                    CheckIn = "2026-08-01",
                    CheckOut = "2026-08-05"
                },
                AdditionalNeeds = "Dinner"
            };

            var response = await _bookingApi.UpdateBookingAsync(bookingId, updated);

            Assert.That((int)response.StatusCode, Is.EqualTo(200));
            Assert.That(response.Data.FirstName, Is.EqualTo("Updated"));
            Console.WriteLine($"[TEST] Updated booking ID: {bookingId}");
        }

        [Test, Order(5)]
        public async Task DeleteBooking_WithValidId_ReturnsSuccess()
        {
            var booking = new BookingRequest
            {
                FirstName = "Delete",
                LastName = "Me",
                TotalPrice = 100,
                DepositPaid = false,
                BookingDates = new BookingDates
                {
                    CheckIn = "2026-09-01",
                    CheckOut = "2026-09-02"
                },
                AdditionalNeeds = "None"
            };

            var created = await _bookingApi.CreateBookingAsync(booking);
            int bookingId = created.Data.BookingId;

            var response = await _bookingApi.DeleteBookingAsync(bookingId);

            Assert.That((int)response.StatusCode, Is.EqualTo(201));
            Console.WriteLine($"[TEST] Deleted booking ID: {bookingId}");
        }
    }
}