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
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(_context.Products.ToList());
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult>GetbyId(int id)
        {
            var result= await _context.Products.FirstOrDefaultAsync(x=>x.Id == id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("containProductName")]
        public async Task<IActionResult> containName(string name)
        {
            var result = _context.Products.Where(x => x.ProductName
                                          .Contains(name))
                                          .OrderBy(a=>a.ProductName)
                                          .ThenBy(q=>q.Price)
                                          .ToList();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> Register([FromBody]ViewProduct model)
        {
            var check = await _context.Products.AnyAsync(x=>x.ProductName == model.ProductName);
            if (check)
            {
                return Conflict();
            }
            var product = new Product()
            {
                ProductName = model.ProductName,
                Price = model.Price,
            };
            _context.Products.Add(product);
           await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> EditCustomer(int id, ViewProduct model)
        {
            var user = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            user.ProductName = model.ProductName;
            user.Price = model.Price;
            _context.Products.Update(user);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            _context.Products.Remove(user);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
