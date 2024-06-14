using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectExample2.AppDBContext;
using ProjectExample2.Model;
using ProjectExample2.ViewModel;

namespace ProjectExample2.Services
{
    public class Customer1 : ICustomer1
    {
        public AppDbContext _context;
        public Customer1(AppDbContext context)
        {
            _context = context;
        }


        public async Task<bool> register([FromBody] ViewCustomer model)
        {
            var check = await _context.Customers.AnyAsync(x => x.Name == model.Name);
            if (check)
            {
                return false;
            }
            var Customer = new Customer()
            {
                Name = model.Name,
                Email = model.Email,
                Address = model.Address,
                Phone = model.Phone,
            };
            _context.Customers.Add(Customer);
            _context.SaveChanges();
            return true;
        }
           public async Task<Customer> getbyID(int id)
           {
             var result= await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
               return result;
           }
      

    }
}
