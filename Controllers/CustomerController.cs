using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectExample2.AppDBContext;
using ProjectExample2.Model;
using ProjectExample2.Services;
using ProjectExample2.ViewModel;
using System.ComponentModel.Design;

namespace ProjectExample2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ICustomer1 Icustomer;
        public CustomerController(AppDbContext context, ICustomer1 customer1)
        {
            _context = context;
            Icustomer = customer1;
        }
        [Authorize(Roles ="User,Admin")]
        [HttpGet]
        public async Task<IActionResult> get()
        {
            return Ok(await _context.Customers.ToListAsync());
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> getbyId(int id)
        {
            var result = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("containName")]
        public IActionResult containName(string name)
        {
          var result= _context.Customers.Where(x=>x.Name.Contains(name)).ToList();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("containAddress")]
        public IActionResult containAddress(string address)
        {
            var result = _context.Customers.Where(x => x.Address.Contains(address))
                                           .Take(5).ToList();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Regis([FromBody]ViewCustomer model)
        {
            var check = await _context.Customers.AnyAsync(x=>x.Id==model.Id);
            if (check)
            {
                return Conflict();
            }
            var Customer = new Customer()
            {
                Id= model.Id,
                Name = model.Name,
                Email = model.Email,
                Address = model.Address,
                Phone = model.Phone,
            };
            _context.Customers.Add(Customer);
            await _context.SaveChangesAsync();
             return Ok();
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult>EditCustomer(int id,ViewCustomer model)
        {
            var user = await _context.Customers.FirstOrDefaultAsync(x=>x.Id==id);
            if (user == null) 
            {
                return NotFound(); 
            }
            user.Email = model.Email;
            user.Address = model.Address;
            user.Phone = model.Phone;
            user.Name = model.Name;
            _context.Customers.Update(user);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if(user == null)
            {
                return NotFound(); 
            }
            _context.Customers.Remove(user);
            await _context.SaveChangesAsync();
            return Ok();
        }
           
    }
}
