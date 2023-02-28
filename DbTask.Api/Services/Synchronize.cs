using DbTask.ExternalDb.DbContexts;
using DbTask.ExternalDb.Models;
using DbTask.InternalDb.DbContexts;
using DbTask.InternalDb.Models;
using Microsoft.EntityFrameworkCore;

namespace DbTask.Api.Services
{
    public class Synchronize : BackgroundService, ISynchronize
    {
        /// <summary>
        /// Synchronize countries from internal database to external database.
        /// </summary>
        public async Task SyncCountriesAsync()
        {
            await using var externalDbContext = new ExternalDbContext();
            await using var internalDbContext = new InternalDbContext();

            var internalCountries = await internalDbContext.Countries.ToListAsync();
            var externalCountries = await externalDbContext.Countries.ToListAsync();

            foreach (var internalCountry in internalCountries)
            {
                var externalCountry = externalCountries.FirstOrDefault(c => c.Id == internalCountry.Id);

                if (externalCountry == null)
                {
                    externalCountry = new ExCountry { Name = internalCountry.Name };
                    await externalDbContext.Countries.AddAsync(externalCountry);
                }
                else
                {
                    externalDbContext.Countries.Update(externalCountry);
                }
            }
            foreach (var externalCountry in externalCountries)
            {
                var internalCity = internalCountries.FirstOrDefault(c => c.Id == externalCountry.Id);

                if (internalCity == null)
                {
                    externalDbContext.Countries.Remove(externalCountry);
                }
            }
            await externalDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Synchronize cities from external database to internal database.
        /// </summary>
        public async Task SyncCitiesAsync()
        {
            await using var externalDbContext = new ExternalDbContext();
            await using var internalDbContext = new InternalDbContext();

            var internalCities = await internalDbContext.Cities.ToListAsync();
            var externalCities = await externalDbContext.Cities.ToListAsync();

            foreach (var externalCity in externalCities)
            {
                var internalCity = internalCities.FirstOrDefault(c => c.Id == externalCity.Id);

                if (internalCity == null)
                {
                    internalCity = new InCity { Name = externalCity.Name };
                    await internalDbContext.Cities.AddAsync(internalCity);
                }
                else
                {
                    internalDbContext.Cities.Update(internalCity);
                }
            }

            foreach (var internalCity in internalCities)
            {
                var externalCity = externalCities.FirstOrDefault(c => c.Id == internalCity.Id);

                if (externalCity == null)
                {
                    internalDbContext.Cities.Remove(internalCity);
                }
            }
            await internalDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Synchronize offices from external database to internal database.
        /// </summary>
        public async Task SyncOfficesAsync()
        {
            await using var externalDbContext = new ExternalDbContext();
            await using var internalDbContext = new InternalDbContext();

            var internalOffices = await internalDbContext.Offices.ToListAsync();
            var externalOffices = await externalDbContext.Offices.ToListAsync();

            foreach (var externalOffice in externalOffices)
            {
                var internalOffice = internalOffices.FirstOrDefault(c => c.Id == externalOffice.Id);

                if (internalOffice == null)
                {
                    internalOffice = new InOffice { Name = externalOffice.Name };
                    await internalDbContext.Offices.AddAsync(internalOffice);
                }
                else
                {
                    internalDbContext.Offices.Update(internalOffice);
                }
            }

            foreach (var internalOffice in internalOffices)
            {
                var externalOffice = externalOffices.FirstOrDefault(c => c.Id == internalOffice.Id);

                if (externalOffice == null)
                {
                    internalDbContext.Offices.Remove(internalOffice);
                }
            }
            await internalDbContext.SaveChangesAsync();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    Console.WriteLine("Synchronization started");
                    await SyncCitiesAsync();
                    //await SyncCountriesAsync();
                    await SyncOfficesAsync();
                    Console.WriteLine("Synchronization completed");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine("Syncronization failed");
                }
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }

        public async Task StartSyncAsync(CancellationToken cancellationToken)
        {
            await StartAsync(cancellationToken);
        }
    }
}
