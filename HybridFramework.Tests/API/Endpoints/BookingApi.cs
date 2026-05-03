using RestSharp;
using Newtonsoft.Json;
using HybridFramework.Tests.API.Client;
using HybridFramework.Tests.API.Models;

namespace HybridFramework.Tests.API.Endpoints
{
    public class BookingApi
    {
        private readonly ApiClient _apiClient;
        private readonly string _token;

        public BookingApi(string token)
        {
            _apiClient = new ApiClient();
            _token = token;
        }

        public async Task<RestResponse> GetAllBookingsAsync()
        {
            var request = new RestRequest("/booking", Method.Get);
            request.AddHeader("Accept", "application/json");
            Console.WriteLine("[BOOKING] Getting all bookings");
            return await _apiClient.ExecuteAsync<object>(request);
        }

        public async Task<RestResponse<BookingResponse>> CreateBookingAsync(BookingRequest booking)
        {
            var request = new RestRequest("/booking", Method.Post);
            request.AddHeader("Accept", "application/json");
            request.AddJsonBody(JsonConvert.SerializeObject(booking));
            Console.WriteLine($"[BOOKING] Creating booking for {booking.FirstName} {booking.LastName}");
            return await _apiClient.ExecuteAsync<BookingResponse>(request);
        }

        

        public async Task<RestResponse<BookingRequest>> GetBookingByIdAsync(int bookingId)
        {
            var request = new RestRequest($"/booking/{bookingId}", Method.Get);
            request.AddHeader("Accept", "application/json");
            _apiClient.AddAuthToken(request, _token);
            Console.WriteLine($"[BOOKING] Getting booking ID: {bookingId}");
            return await _apiClient.ExecuteAsync<BookingRequest>(request);
        }

        

        public async Task<RestResponse<BookingRequest>> UpdateBookingAsync(int bookingId, BookingRequest booking)
        {
            var request = new RestRequest($"/booking/{bookingId}", Method.Put);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", "Basic YWRtaW46cGFzc3dvcmQxMjM=");
            request.AddJsonBody(JsonConvert.SerializeObject(booking));
            Console.WriteLine($"[BOOKING] Updating booking ID: {bookingId}");
            return await _apiClient.ExecuteAsync<BookingRequest>(request);
        }

        public async Task<RestResponse> DeleteBookingAsync(int bookingId)
        {
            var request = new RestRequest($"/booking/{bookingId}", Method.Delete);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", "Basic YWRtaW46cGFzc3dvcmQxMjM=");
            Console.WriteLine($"[BOOKING] Deleting booking ID: {bookingId}");
            return await _apiClient.ExecuteAsync<object>(request);
        }
    }
}