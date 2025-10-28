using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.EntityConfigurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.FullName)
                   .IsRequired()
                   .HasColumnType("nvarchar(255)");

            builder.Property(e => e.Phone)
                   .IsRequired()
                   .HasColumnType("varchar(15)");

            builder.Property(e => e.Email)
                   .IsRequired()
                   .HasColumnType("varchar(500)");

            builder.Property(e => e.EmployeeCode)
                   .IsRequired();

            builder.Property(e => e.DateOfBirth).HasColumnType("date");
        }
    }
}
