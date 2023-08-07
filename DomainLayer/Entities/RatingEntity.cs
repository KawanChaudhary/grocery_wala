namespace GroceryWala.DomainLayer.Entities
{
    public class RatingEntity
    {

        public int Id { get; set; }

        public string UserId { get; set; }

        public string ProductId { get; set; }
        public int Rating { get; set; }
    }
}
