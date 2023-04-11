using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FoodDesire.Web.API.Services;
public class AccountControllerService : IAccountControllerService {
    private readonly IConfiguration _configuration;
    private readonly IUserService<Customer> _userService;

    public AccountControllerService(IUserService<Customer> userService, IConfiguration configuration) {
        _userService = userService;
        _configuration = configuration;
    }

    public async Task<Customer> CreateAccount(User user) {
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Account!.Password);
        user.Account.Password = passwordHash;
        Customer customer = new() {
            User = user,
        };
        return await _userService.CreateAccount(customer);
    }

    public async Task<bool> DeleteById(int id) => await _userService.DeleteAccountById(id);

    public async Task<Customer> GetByEmail(string customerEmail) => await _userService.GetByEmail(customerEmail);

    public async Task<string> SignIn(string email, string password) {
        Customer customer = await _userService.GetByEmail(email);
        if (customer == null) return null!;

        bool verified = BCrypt.Net.BCrypt.Verify(password, customer.User!.Account!.Password);
        return !verified ? null! : CreateToken(customer);
    }

    public async Task<Customer> UpdateAccount(Customer customer) => await _userService.UpdateAccount(customer);

    private string CreateToken(Customer customer) {
        List<Claim> claims = new() {
            new(ClaimTypes.Name, customer.User!.Account!.Email!),
            new(ClaimTypes.NameIdentifier, customer.Id.ToString()),
        };
        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration["JWT:SignInKey"]!));
        SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256Signature);

        JwtSecurityToken? token = new(claims: claims, signingCredentials: credentials, expires: DateTime.Now.AddDays(7));

        string jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
}
