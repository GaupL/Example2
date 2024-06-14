using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjectExample2.Model.Config
{
    public class OrderdetialConfig : IEntityTypeConfiguration<Orderdetial>
    {
        public void Configure(EntityTypeBuilder<Orderdetial> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.datetime).HasColumnType("date");
        }
    }
}
