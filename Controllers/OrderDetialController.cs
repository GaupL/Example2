using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectExample2.AppDBContext;
using ProjectExample2.Model;
using ProjectExample2.ViewModel;

namespace ProjectExample2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetialController : ControllerBase
    {
        private readonly AppDbContext _context;
        public OrderDetialController(AppDbContext context)
        {
            _context = context;   
        }
        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(_context.Orderdetials.ToList());
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetbyId(int id)
        {
            var result = await _context.Orderdetials.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("CreateOrderdetial")]
        public async Task<IActionResult> Register([FromBody] ViewOrderDetial model)
        {
            var result=await _context.Orders.AnyAsync(a=>a.Id==model.OrderId) && await _context.Products.AnyAsync(b => b.Id == model.ProductId);
            if(!result)
            {
                return NotFound();
            }
            var Orderdetial = new Orderdetial()
            {
                OrderId = model.OrderId,
                ProductId = model.ProductId,
                datetime=model.datetime,
                Quanlity=model.Quanlity,
            };
            var qwe = await _context.Orderdetials.AnyAsync(x => x.OrderId == Orderdetial.OrderId);
            if (qwe)
            {
                return Conflict();
            }
            var create=_context.Orderdetials.Add(Orderdetial);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Orderdetials.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) 
            {
                return NotFound(); 
            }
            _context.Orderdetials.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
