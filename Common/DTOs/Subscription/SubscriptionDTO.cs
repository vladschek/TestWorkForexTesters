using System.ComponentModel.DataAnnotations;

namespace Common.DTOs.Subscription
{
    public class SubscriptionDTO
    {
        [Required]
        public string Type { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}
