using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IIETA.Models;

namespace IIETA.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<IIETA.Models.Catigorie> Catigorie { get; set; } = default!;
        public DbSet<IIETA.Models.handleEmail> handleEmail { get; set; } = default!;
    }
}