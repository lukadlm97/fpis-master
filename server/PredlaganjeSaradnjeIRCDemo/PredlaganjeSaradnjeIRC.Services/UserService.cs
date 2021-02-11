using PredlaganjeSaradnjeIRC.Data;
using PredlaganjeSaradnjeIRC.Data.Model;
using PredlaganjeSaradnjeIRC.Data.Service;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace PredlaganjeSaradnjeIRC.Services
{
    public class UserService : IUser
    {
        private readonly ApplicationContext _context;
        private readonly AppSettings _appSettings;

        public UserService(ApplicationContext context, IOptions<AppSettings> option)
        {
            _context = context;
            _appSettings = option.Value;
        }

       public async Task<string> LogIn(string username, string password)
        {
            var user =  _context.Users.FirstOrDefault(x => x.Username == username);

            if (user == null)
                return null;

            if(user.Password != password)
                return null;

            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(key);
            var claims = new[] { new Claim(ClaimTypes.Role, user.Role) };
            var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken("ExampleServer", "ExampleClients", claims, expires: DateTime.Now.AddDays(5), signingCredentials: credentials);
            var tokenHandler = new JwtSecurityTokenHandler();


            return tokenHandler.WriteToken(token);
            /*
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);*/
        }

        public async Task<bool> Register(string username, string password, string firstName, string lastName)
        {
            User user = new User
            {
                Username = username,
                Password = password,
                FirstName = firstName,
                LastName = lastName,
                Role = "Admin"
            };
            var isExist = _context.Users.Where(x => x.Username == username).Any();

            if (isExist)
                return false;

            try
            {
                await _context.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}