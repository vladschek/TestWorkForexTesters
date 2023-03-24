using System.ComponentModel.DataAnnotations;
using Common.DTOs.Subscription;

namespace Common.DTOs
{
    public class UserCreateDTO
    {
        public int UserId { get; set; }

        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public SubscriptionDTO? Subscription { get; set; }
    }
}
