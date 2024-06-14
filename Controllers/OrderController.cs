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
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;
        public OrderController(AppDbContext context)
        {
            _context=context;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(_context.Orders.ToList());
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetbyId(int id)
        {
            var result = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("Include")]
        public IActionResult Getinclude(int id)
        {
            var result =  _context.Orders.Include(x => x.customer).ThenInclude(x=>x.Orders)
                                         .Include(z => z.Orderdetials).ThenInclude(z => z.Products)
                                         .FirstOrDefault(o=>o.Id==id);
            return Ok(result);
        }
        [HttpGet("select")]
        public async Task<IActionResult> GetSelect(int id)
        {
            var result = _context.Orders.Select(x => new
            {
                Id = x.Id,
                Date = x.DateTime,
                CustomerID = x.CustomerId,
                Name = x.customer.Name,
                Orderdetial = x.Orderdetials.Select(z => new
                {
                    Id = z.Id,
                    OrderId = z.OrderId,
                    Customer = z.Orders.customer.Name,
                    ProductId = z.ProductId,
                    Product = z.Products.ProductName,
                    Quanlity = z.Quanlity,
                })

            }).FirstOrDefault(o=>o.Id==id) ;
            return Ok(result);
        }
        [HttpGet("date")]
        public async Task<IActionResult> Get(DateTime start,DateTime end)
        {
            var result =  _context.Orders.Where(x => x.DateTime >= start && x.DateTime<=end)
                                         .OrderBy(z=>z.DateTime);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost("CreateOrder")]
        public async Task<IActionResult> Register([FromBody] ViewOrder model)
        {
            var result = await _context.Customers.AnyAsync(c => c.Id == model.CustomerId);
            if (!result)
            {
                return NotFound();
            }
            var Order = new Order()
            {
                DateTime = model.DateTime,
                CustomerId = model.CustomerId,
            };
            _context.Orders.Add(Order);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) 
            {
                return NotFound(); 
            }
            _context.Orders.Remove(user);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
