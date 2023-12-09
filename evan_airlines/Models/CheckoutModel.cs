namespace evan_airlines.Models
{
    public class CheckoutModel
    {
        public int Id { get; set; }
        public string? User { get; set; }
        public string? Departure { get; set; }
        public string? Arrival { get; set; }
        public string? Number { get; set; }
        public int Cost { get; set; }

    }
}
