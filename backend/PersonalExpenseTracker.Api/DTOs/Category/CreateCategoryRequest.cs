
using System.ComponentModel.DataAnnotations;

namespace PersonalExpenseTracker.Api.DTOs.Category
{
    public class CreateCategoryRequest
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
    }
}
