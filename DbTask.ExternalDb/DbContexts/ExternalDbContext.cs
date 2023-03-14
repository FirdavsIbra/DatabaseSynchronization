using DbTask.ExternalDb.Models;
using Microsoft.EntityFrameworkCore;

namespace DbTask.ExternalDb.DbContexts
{
    public class ExternalDbContext : DbContext
    {
        /// <summary>
        /// Gets or sets table of country.
        /// </summary>
        public virtual DbSet<ExCountry> Countries { get; set; }

        /// <summary>
        /// Gets or sets table of city.
        /// </summary>
        public virtual DbSet<ExCity> Cities { get; set; }

        /// <summary>
        /// Gets or sets table of office.
        /// </summary>
        public virtual DbSet<ExOffice> Offices { get; set; }

        public virtual DbSet<ExDeletedCountry> DeletedCountries { get; set; }
        public virtual DbSet<ExDeletedCity> DeletedCities { get; set; }
        public virtual DbSet<ExDeletedOffice> DeletedOffices { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=NBA-088-01-UZ\\SQLEXPRESS;Database=ExternalDb;Trusted_Connection=True; TrustServerCertificate=True;");
        }   
    }
}
