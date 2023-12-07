using System.ComponentModel.DataAnnotations;

namespace evan_airlines.Models
{
    public class EmployeeModel
    {
        [Key]
        public int id { get; set; }
        public string? name { get; set; }
        public int experience { get; set; }
        public int job_level { get; set; }
        public double pilot_hours { get; set; }
        public double fa_hours { get; set; }
        public string? gender {  get; set; }

    }
}
