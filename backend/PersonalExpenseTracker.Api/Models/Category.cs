
using System.ComponentModel.DataAnnotations;

namespace PersonalExpenseTracker.Api.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        public bool IsDefault { get; set; }

        public string? UserId { get; set; }

        public ApplicationUser? User { get; set; }
    }
}
