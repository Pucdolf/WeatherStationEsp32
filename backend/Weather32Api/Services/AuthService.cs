using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Weather32Api.Data;
using Weather32Api.DTO;
using Weather32Api.Models;

namespace Weather32Api.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _dbContext;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public AuthService(AppDbContext dbContext, IMapper mapper, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
        _mapper = mapper;
    }
    public async Task<bool> IsEmailExistsAsync(string email)
    {
        //return await _dbContext.Users.AnyAsync(u => u.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase));
        return await _dbContext.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());

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

    public async Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO loginRequestDTO)
    {
        try
        {

            var user = await _dbContext.Users.FirstOrDefaultAsync(u =>
                u.Email.ToLower() == loginRequestDTO.Email.ToLower());

            if (user == null || user.Password != loginRequestDTO.Password)
            {
                return null;
            }

            //TOKEN
            var token = GenerateJwtToken(user);

            return new LoginResponseDTO()
            {
                UserDTO = _mapper.Map<UserDTO>(user),
                Token = token
            };
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"An unexpected error occurred during user login.", e);
        }
    }

    private string GenerateJwtToken(User user)
    {
        var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JwtSettings")["SecretKey"]);
        var tokenDescripor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescripor);

        return tokenHandler.WriteToken(token);
    }
}