namespace GroceryWala.DomainLayer.Entities
{
    public class OrderEntity
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public string Name{ get; set; }
        public int ProductQuantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
    }
}
