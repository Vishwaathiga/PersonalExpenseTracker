
using System.ComponentModel.DataAnnotations;

namespace PersonalExpenseTracker.Api.DTOs.Budget
{
    public class UpdateBudgetRequest
    {
        [Range(0.01, 999999999)]
        public decimal Amount { get; set; }
    }
}
