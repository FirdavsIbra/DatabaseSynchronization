using DbTask.InternalDb.Models;
using Microsoft.EntityFrameworkCore;

namespace DbTask.InternalDb.DbContexts
{
    public class InternalDbContext : DbContext
    {
        /// <summary>
        /// Gets or sets table of country.
        /// </summary>
        public virtual DbSet<InCountry> Countries { get; set; }

        /// <summary>
        /// Gets or sets table of city.
        /// </summary>
        public virtual DbSet<InCity> Cities { get; set; }

        /// <summary>
        /// Gets or sets table of office.
        /// </summary>
        public virtual DbSet<InOffice> Offices { get; set; }

        /// <summary>
        /// Gets or sets table of deleted countries.
        /// </summary>
        public virtual DbSet<InDeletedCountry> DeletedCountries { get; set; }

        /// <summary>
        /// Gets or sets table of deleted cities.
        /// </summary>
        public virtual DbSet<InDeletedCity> DeletedCities { get; set; }

        /// <summary>
        /// Gets or sets table of deleted offices.
        /// </summary>
        public virtual DbSet<InDeletedOffice> DeletedOffices { get; set; }
       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=NBA-088-01-UZ\\SQLEXPRESS;Database=InternalDb;Trusted_Connection=True; TrustServerCertificate=True;");
        }
    }
}
