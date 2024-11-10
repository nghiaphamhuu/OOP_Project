namespace WebApplication2.Entity
{
    public class StoreEntity
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public string typeCd { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }
        public int total { get; set; }

        public StoreEntity() { }
    }
}
