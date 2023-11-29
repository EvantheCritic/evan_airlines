using System.ComponentModel.DataAnnotations;

namespace evan_airlines.Models
{
    public class EmployeeModel
    {
        [Key]
        public int id { get; set; }
        public string? name { get; set; }
        public string? gender { get; set; }
        public string? job { get; set; }
        public int experience { get; set; }
        public int job_level { get; set; }
        public double hours { get; set; }
        public double pay { get; set; }

    }
}
