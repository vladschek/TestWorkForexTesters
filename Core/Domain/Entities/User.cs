using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(150)]
        public string Email { get; set; }

        public int SubscriptionId { get; set; }

        public Subscription Subscription { get; set; }
    }
}
