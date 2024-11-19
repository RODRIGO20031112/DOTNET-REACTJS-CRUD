using rodrigo_server;
using Microsoft.EntityFrameworkCore;

public class EmailAlreadyExistsException : Exception
{
    public EmailAlreadyExistsException(string message) : base(message) { }
}

public class EmptyNameException : Exception
{
    public EmptyNameException(string message) : base(message) { }
}

public class EmptyEmailException : Exception
{
    public EmptyEmailException(string message) : base(message) { }
}

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _userRepository;

    public UserRepository(DatabaseContext userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> GetByIdAsync(int id)
    {
        var findUser = await _userRepository.Users
            .FirstOrDefaultAsync(u => u.Id == id) 
            ?? throw new Exception("User not found");
        return findUser;
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        return await _userRepository.Users
            .Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Phone = u.Phone,
                CreatedAt = u.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<User> AddAsync(User user)
    {
        var existingUser = await _userRepository.Users
            .FirstOrDefaultAsync(u => u.Email == user.Email);

        if (string.IsNullOrWhiteSpace(user.Name))
        {
            throw new EmptyNameException("O campo 'Name' é obrigatório.");
        }

        if (string.IsNullOrWhiteSpace(user.Email))
        {
            throw new EmptyEmailException("O campo 'Email' é obrigatório.");
        }

        if (existingUser != null)
        {
            throw new EmailAlreadyExistsException("O e-mail já está em uso.");
        }

        // Se o e-mail não existir, adicione o novo usuário
        user.CreatedAt = DateTime.Now;
        await _userRepository.Users.AddAsync(user);
        await _userRepository.SaveChangesAsync();
        return user;
    }

    public async Task UpdateAsync(User user, int id)
    {
        var existingUser = await _userRepository.Users.FindAsync(id);

        if (existingUser == null)
        {
            throw new KeyNotFoundException($"Usuário com o ID {id} não foi encontrado");
        }

        // Verificar se o e-mail já está em uso por outro usuário
        var emailInUse = await _userRepository.Users
            .AnyAsync(u => u.Email == user.Email && u.Id != id);

        if (emailInUse)
        {
            throw new EmailAlreadyExistsException("O e-mail fornecido já está em uso por outro usuário.");
        }

        if (string.IsNullOrWhiteSpace(user.Name))
        {
            throw new EmptyNameException("O campo 'Name' é obrigatório.");
        }

        if (string.IsNullOrWhiteSpace(user.Email))
        {
            throw new EmptyEmailException("O campo 'Email' é obrigatório.");
        }

        // Atualizar os dados do usuário
        existingUser.Name = user.Name;
        existingUser.Email = user.Email;
        existingUser.Phone = user.Phone;

        _userRepository.Entry(existingUser).State = EntityState.Modified;

        await _userRepository.SaveChangesAsync();
    }


    public async Task DeleteAsync(int id)
    {
        var user = await _userRepository.Users.FindAsync(id);

        if (user == null)
        {
            throw new KeyNotFoundException($"Usuário com o ID {id} não foi encontrado.");
        }

        _userRepository.Users.Remove(user);
        await _userRepository.SaveChangesAsync();
    }
}
