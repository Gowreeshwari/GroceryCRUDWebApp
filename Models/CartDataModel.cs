namespace GroceryCRUDWebApp.Models
{
    public class CartDataModel
    {
        public int CartId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int? CouponId { get; set; }
        public int Cost { get; set; }
        public int NoofProducts { get; set; }
        public int TotalAmount { get; set; }

    }
}
