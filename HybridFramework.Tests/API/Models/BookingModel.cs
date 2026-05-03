using Newtonsoft.Json;

namespace HybridFramework.Tests.API.Models
{
    public class BookingDates
    {
        [JsonProperty("checkin")]
        public string? CheckIn { get; set; }

        [JsonProperty("checkout")]
        public string? CheckOut { get; set; }
    }

    public class BookingRequest
    {
        [JsonProperty("firstname")]
        public string? FirstName { get; set; }

        [JsonProperty("lastname")]
        public string? LastName { get; set; }

        [JsonProperty("totalprice")]
        public int TotalPrice { get; set; }

        [JsonProperty("depositpaid")]
        public bool DepositPaid { get; set; }

        [JsonProperty("bookingdates")]
        public BookingDates? BookingDates { get; set; }

        [JsonProperty("additionalneeds")]
        public string? AdditionalNeeds { get; set; }
    }

    public class BookingResponse
    {
        [JsonProperty("bookingid")]
        public int BookingId { get; set; }

        [JsonProperty("booking")]
        public BookingRequest? Booking { get; set; }
    }
}