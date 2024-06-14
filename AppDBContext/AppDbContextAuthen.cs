using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectExample2.Model;

namespace ProjectExample2.AppDBContext
{
    public class AppDbContextAuthen:IdentityDbContext<Register>
    {
        public AppDbContextAuthen(DbContextOptions<AppDbContextAuthen>options):base(options)
        {
            
        }
    }
}
