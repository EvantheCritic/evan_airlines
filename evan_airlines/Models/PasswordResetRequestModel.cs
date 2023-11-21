using System.ComponentModel.DataAnnotations;

namespace evan_airlines.Models
{
    public class PasswordResetRequestModel
    {
        public string email { get; set; }
        public bool emailSent { get; set; }
    }
}
