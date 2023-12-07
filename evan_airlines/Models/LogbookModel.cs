using System.ComponentModel.DataAnnotations;

namespace evan_airlines.Models
{
    public class LogbookModel
    {
        [Key]
        public int id { get; set; }
        public string? pilot { get; set; }
        public string? copilot { get; set; }
        public string? fa_1 { get; set; }
        public string? fa_2 { get; set; }
        public string? departure { get; set; }
        public string? arrival { get; set; }
        public double hours { get; set; }
        public double minutes {  get; set; }
        public int pay { get; set; }
    }
}
