namespace GroceryCRUDWebApp.Models
{
    public class OrderDetailsDataModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

    }
}
