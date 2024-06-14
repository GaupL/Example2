using Microsoft.AspNetCore.Mvc;
using ProjectExample2.Model;
using ProjectExample2.ViewModel;

namespace ProjectExample2.Services
{
    public interface ICustomer1
    {
        Task<bool> register([FromBody] ViewCustomer model);
        Task<Customer> getbyID(int id);
    }
}