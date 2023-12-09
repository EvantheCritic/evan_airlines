namespace evan_airlines.Models
{
    public class ConfirmationModel
    {
        public int Id { get; set; }
        public string? ConfirmationCode { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Route { get; set; }
        public string? Number { get; set; }
    }
}
