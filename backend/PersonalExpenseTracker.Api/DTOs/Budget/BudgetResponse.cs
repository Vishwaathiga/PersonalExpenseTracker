
namespace PersonalExpenseTracker.Api.DTOs.Budget
{
    public class BudgetResponse
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public decimal TotalSpent { get; set; }

        public decimal RemainingAmount { get; set; }

        public decimal PercentageUsed { get; set; }

        public bool IsExceeded { get; set; }
    }
}
