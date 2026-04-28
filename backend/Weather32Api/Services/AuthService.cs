using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Weather32Api.Data;
using Weather32Api.DTO;
using Weather32Api.Models;

namespace Weather32Api.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public AuthService(AppDbContext dbContext, IMapper mapper, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<bool> IsEmailExistsAsync(string email)
    {
        return await _dbContext.Users.AnyAsync(u => u.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase));
    }

    public async Task<UserDTO?> RegisterAsync(RegistrationRequestDTO registrationRequestDTO)
    {
        try
        {
            if (await IsEmailExistsAsync(registrationRequestDTO.Email))
            {
                throw new InvalidOperationException($"User with email {registrationRequestDTO.Email} already exists.");
            }

            User user = new()
            {
                Email = registrationRequestDTO.Email,
                Username = registrationRequestDTO.Username,
                Password = registrationRequestDTO.Password,
                Role = string.IsNullOrEmpty(registrationRequestDTO.Role) ? "User" : registrationRequestDTO.Role,
                CreatedAt = DateTime.UtcNow
            };

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<UserDTO>(user);

        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"An unexpected error occurred during user registration.", e);
        }
    }

    public Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO loginRequestDTO)
    {
        throw new NotImplementedException();
    }

}