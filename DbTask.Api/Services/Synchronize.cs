using DbTask.ExternalDb.DbContexts;
using DbTask.ExternalDb.Models;
using DbTask.InternalDb.DbContexts;
using DbTask.InternalDb.Models;
using Microsoft.EntityFrameworkCore;

namespace DbTask.Api.Services
{
    public class Synchronize : ISynchronize
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
                    externalCountry = new ExCountry 
                    { 
                        Id = internalCountry.Id, 
                        Name = internalCountry.Name 
                    };
                    await externalDbContext.Countries.AddAsync(externalCountry);
                }
                else
                {
                    externalDbContext.Countries.Update(externalCountry);
                }
            }

            foreach (var externalCountry in externalCountries)
            {
                var internalCountry = internalCountries.FirstOrDefault(c => c.Id == externalCountry.Id);

                if (internalCountry == null)
                {
                    var exDeletedCountry = new ExDeletedCountry
                    {
                        Id = externalCountry.Id,
                        Name = externalCountry.Name,
                        DeletedAt = DateTime.UtcNow
                    };
                    var inDeletedCountry = new InDeletedCountry
                    {
                        Id = externalCountry.Id,
                        Name = externalCountry.Name,
                        DeletedAt = DateTime.UtcNow
                    };
                    await internalDbContext.DeletedCountries.AddAsync(inDeletedCountry);
                    await externalDbContext.DeletedCountries.AddAsync(exDeletedCountry);

                    var citiesToDelete = await externalDbContext.Cities
                                            .Where(c => c.CountryId == externalCountry.Id)
                                            .ToListAsync();

                    foreach (var city in citiesToDelete)
                    {
                        var inDeletedCity = new InDeletedCity
                        {
                            Id = city.Id,
                            Name = city.Name,
                            CountryId = city.CountryId,
                            DeletedAt = DateTime.UtcNow
                        };
                        var exDeletedCity = new ExDeletedCity()
                        {
                            Id = city.Id,
                            Name = city.Name,
                            CountryId = city.CountryId,
                            DeletedAt = DateTime.UtcNow
                        };
                        await externalDbContext.DeletedCities.AddAsync(exDeletedCity);
                        await internalDbContext.DeletedCities.AddAsync(inDeletedCity);

                        var officesToDelete = await externalDbContext.Offices
                                                                .Where(o => o.CityId == city.Id)
                                                                .ToListAsync();
                        foreach (var office in officesToDelete)
                        {
                            var inDeletedOffice = new InDeletedOffice
                            {
                                Id = office.Id,
                                Name = office.Name,
                                CityId = office.CityId,
                                DeletedAt = DateTime.UtcNow
                            };
                            var exDeletedOffice = new ExDeletedOffice
                            {
                                Id = office.Id,
                                Name = office.Name,
                                CityId = office.CityId,
                                DeletedAt = DateTime.UtcNow
                            };
                            await externalDbContext.DeletedOffices.AddAsync(exDeletedOffice);
                            await internalDbContext.DeletedOffices.AddAsync(inDeletedOffice);
                            externalDbContext.Offices.Remove(office);
                        }

                        externalDbContext.Cities.Remove(city);
                    }
                    externalDbContext.Countries.Remove(externalCountry);
                }
            }
            await externalDbContext.SaveChangesAsync();
            await internalDbContext.SaveChangesAsync();
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
                    internalCity = new InCity { Id = externalCity.Id, Name = externalCity.Name, CountryId = externalCity.CountryId };
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
                    var exDeletedCity = new ExDeletedCity
                    {
                        Id = internalCity.Id,
                        Name = internalCity.Name,
                        CountryId = internalCity.CountryId,
                        DeletedAt = DateTime.UtcNow
                    };
                    var inDeletedCity = new InDeletedCity
                    {
                        Id = internalCity.Id,
                        Name = internalCity.Name,
                        CountryId = internalCity.CountryId,
                        DeletedAt = DateTime.UtcNow
                    };
                    await internalDbContext.DeletedCities.AddAsync(inDeletedCity);
                    await externalDbContext.DeletedCities.AddAsync(exDeletedCity);

                    var officesToDelete = await internalDbContext.Offices.Where(o => o.CityId == internalCity.Id).ToListAsync();

                    foreach(var office in officesToDelete)
                    {
                        var inDeleteOffice = new InDeletedOffice
                        {
                            Id = office.Id,
                            Name = office.Name,
                            CityId = office.CityId,
                            DeletedAt = DateTime.UtcNow
                        };

                        var exDeletedOffice = new ExDeletedOffice
                        {
                            Id = office.Id,
                            Name = office.Name,
                            CityId = office.CityId,
                            DeletedAt = DateTime.UtcNow
                        };

                        await externalDbContext.DeletedOffices.AddAsync(exDeletedOffice);
                        await internalDbContext.DeletedOffices.AddAsync(inDeleteOffice);
                        internalDbContext.Offices.Remove(office);
                    }

                    internalDbContext.Cities.Remove(internalCity);
                }
            }
            await internalDbContext.SaveChangesAsync();
            await externalDbContext.SaveChangesAsync();
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
                    internalOffice = new InOffice { Id = externalOffice.Id, Name = externalOffice.Name, CityId = externalOffice.CityId };
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
                    var exDeletedOffice = new ExDeletedOffice
                    {
                        Id = internalOffice.Id,
                        Name = internalOffice.Name,
                        CityId = internalOffice.CityId,
                        DeletedAt = DateTime.UtcNow
                    };
                    var inDeletedOffice = new InDeletedOffice
                    {
                        Id = internalOffice.Id,
                        Name = internalOffice.Name,
                        CityId = internalOffice.CityId,
                        DeletedAt = DateTime.UtcNow
                    };

                    await internalDbContext.DeletedOffices.AddAsync(inDeletedOffice);
                    await externalDbContext.DeletedOffices.AddAsync(exDeletedOffice);

                    internalDbContext.Offices.Remove(internalOffice);
                }
            }
            await internalDbContext.SaveChangesAsync();
            await externalDbContext.SaveChangesAsync();
        }

    }
}
