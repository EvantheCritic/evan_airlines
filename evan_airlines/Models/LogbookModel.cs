using System.ComponentModel.DataAnnotations;

namespace evan_airlines.Models
{
    public class LogbookModel
    {
        [Key]
        public int id { get; set; }
        public string pilot { get; set; }
        public string copilot { get; set; }
        public string? flight_attendant { get; set; }
        public string departure { get; set; }
        public string arrival { get; set; }
        public double flight_hours { get; set; }
        public double pay { get; set; }
    }
}
