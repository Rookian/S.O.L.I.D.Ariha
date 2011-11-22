namespace Core.Domain.Model.ConsumerProtection
{
    public class SalesmanArticleGroupedByMonthAndDescription
    {
        public int Amount { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public int Month { get; set; }
        public decimal Sum 
        {
            get { return Cost*Amount; }
        }
    }
}