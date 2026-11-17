
namespace PersonalExpenseTracker.Api.DTOs.Dashboard
{
    public class CategorySpendingResponse
    {
        public string CategoryName { get; set; } = string.Empty;

        public decimal TotalAmount { get; set; }
    }
}
