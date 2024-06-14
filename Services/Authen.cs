using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.IdentityModel.Tokens;
using ProjectExample2.AppDBContext;
using ProjectExample2.Model;
using ProjectExample2.ViewModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectExample2.Services
{
    public class Authen : IAuthen
    {
        public UserManager<Register> _user;
        public RoleManager<IdentityRole> _role;
        public IConfiguration _config;

        public Authen(UserManager<Register> user, RoleManager<IdentityRole> role, IConfiguration config)
        {
            _user = user;
            _role = role;
            _config = config;
        }
        public async Task<bool> Register([FromBody] ViewRegister model)
        {
            var check = await _user.FindByNameAsync(model.Username);
            if (check != null) { return false; }
            var user = new Register()

            {
                Email = model.Email,
                GivenName = model.GivenName,
                UserName = model.Username,
                PhoneNumber = model.Phone,
                Surname = model.Surname,
            };
            var create = await _user.CreateAsync(user, model.Password);
            foreach (var role in model.Roles)
            {
                var result = await _role.RoleExistsAsync(role);
                if (!result)
                {
                    await _role.CreateAsync(new IdentityRole(role));
                }
                await _user.AddToRoleAsync(user, role);
            }
            return true;
        }
        public async Task<string> Login([FromBody]ViewLogin model)
        {
            var user = await _user.FindByNameAsync(model.UserName);
            if(user == null)
            {
                return $"ไม่มี {model.UserName} นี้";
            }
            var Login = await _user.CheckPasswordAsync(user, model.password);
            if (!Login)
            {
                return "รหัสผ่านไม่ถูกต้อง";
            }
            return await Token123(model.UserName);
        }
        public async Task<string>Token123(string username)
        {
            var user = await _user.FindByNameAsync(username);
            var claims = new List<Claim>()
            { 
                new Claim("Id",user.Id),
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            var roles = await _user.GetRolesAsync(user);
            foreach(var role in roles)
            {
                claims.Add(new Claim("role",role));
            }
            var sucure = Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value);
            var issuer = _config.GetSection("Jwt:Issuer").Value;
            var audience = _config.GetSection("Jwt:Audience").Value;
            var securityKey = new SymmetricSecurityKey(sucure);
            var credential=new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256Signature);
            var tokenhandler = new JwtSecurityTokenHandler();
            var tokendescritor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = issuer,
                Audience = audience,
                Expires = DateTime.Now.AddMinutes(60),
                SigningCredentials = credential
            };
            var jwttoken = tokenhandler.CreateToken(tokendescritor);
            var token = tokenhandler.WriteToken(jwttoken);
            return token;
        }
    }
}
