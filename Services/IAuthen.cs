using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectExample2.Model;
using ProjectExample2.ViewModel;

namespace ProjectExample2.Services
{
    public interface IAuthen
    {
        Task<bool> Register([FromBody] ViewRegister model);
        Task<string> Login([FromBody] ViewLogin model);
    }
   
}