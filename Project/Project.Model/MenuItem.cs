namespace RestaurantManagement.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; } // FK
        public Restaurant Restaurant { get; set; } = null!;

        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public float Price { get; set; }
    }

}
