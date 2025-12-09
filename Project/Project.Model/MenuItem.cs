namespace RestaurantManagement.Models
{
    public class MenuItem
    {

        public int Id { get; set; } // PK dla EF
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required float Price { get; set; }

    }
}