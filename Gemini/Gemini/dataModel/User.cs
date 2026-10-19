namespace Gemini.dataModel
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public int RoleId { get; set; }
        public Role? Role { get; set; }

        public int? PharmacyId { get; set; }
        public Pharmacy? Pharmacy { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;



        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}