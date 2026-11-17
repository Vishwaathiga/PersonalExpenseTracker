
using System.ComponentModel.DataAnnotations;

namespace PersonalExpenseTracker.Api.DTOs.Budget
{
    public class CreateBudgetRequest
    {
        [Range(0.01, 999999999)]
        public decimal Amount { get; set; }

        [Range(2000, 2100)]
        public int Year { get; set; }

        [Range(1, 12)]
        public int Month { get; set; }
    }
}
