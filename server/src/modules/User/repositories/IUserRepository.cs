namespace rodrigo_server;

public interface IUserRepository {
    Task<User> GetByIdAsync(int id);
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<User> AddAsync(User user);
    Task UpdateAsync(User user, int id);
    Task DeleteAsync(int id);
}