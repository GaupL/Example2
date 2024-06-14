using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectExample2.Model;
using ProjectExample2.Services;
using ProjectExample2.ViewModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectExample2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        private readonly IAuthen _Iau;
        private readonly UserManager<Register> _User;
        private readonly RoleManager<IdentityRole> _Role;
        public AuthenController(IAuthen Iau, UserManager<Register> User, RoleManager<IdentityRole> Role)
        {
            _Iau = Iau;
            _User = User;
            _Role = Role;
        }
        [HttpPost("Regis")]
        public async Task<IActionResult> Register([FromBody]ViewRegister model)
        {
            var result=await _Iau.Register(model);
            if (!result)
            {
                return NotFound();
            }

           return Ok();
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login1([FromBody] ViewLogin model)
        {
           var result=await _Iau.Login(model);
            if (result == $"ไม่มี {model.UserName} นี้")
            {
                return NotFound(result);
            }
            else if(result == "รหัสผ่านไม่ถูกต้อง")
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpGet]
        public IActionResult get()
        {
            return Ok(_User.Users.ToList());
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> getbyId(string id)
        {
            var result = await _User.Users.FirstOrDefaultAsync(x=>x.Id==id);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("getrole")]
        public async Task<IActionResult> getrole(string username)
        {
            var find=await _User.FindByNameAsync(username);
            if (find == null)
            {
                return NotFound();
            }
           var result=await _User.GetRolesAsync(find);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
