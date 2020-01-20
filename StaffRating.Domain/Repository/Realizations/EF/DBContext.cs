using StaffRating.Domain.Entities;
using System.Data.Entity;

namespace StaffRating.Domain.Repository.Realizations.EF
{
    public class DBContext : DbContext
    {
        public DbSet<CATEGORY> CATEGORIES { get; set; }
        public DbSet<TEST> TESTS { get; set; }    
    }
}

