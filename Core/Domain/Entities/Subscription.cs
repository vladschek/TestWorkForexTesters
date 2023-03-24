using Core.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Entities
{
    public class Subscription
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public SubscriptionType Type { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public User User { get; set; }
    }
}
