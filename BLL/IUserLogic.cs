using RatingSystem.Domain;


namespace RatingSystem.BLL
{
    public interface IUserLogic
    {
        Task<User> RegisterUserAsync(string name);
        Task<User?> GetUserByIdAsync(int id);
        Task DeleteUserAsync(int UserId);

    }
}
