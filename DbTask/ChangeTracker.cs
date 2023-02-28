using DbTask.ExternalDb.DbContexts;
using DbTask.ExternalDb.Models;
using DbTask.InternalDb.DbContexts;
using DbTask.InternalDb.Models;
using Microsoft.EntityFrameworkCore;

namespace DbTask
{
    internal class ChangeTracker
    {
        public void Synch()
        {
            using var externalDb = new ExternalDbContext();
            using var internalDb = new InternalDbContext();


            externalDb.Countries.Add(new ExCountry
            {
                Name ="fsd"
            });
            externalDb.SaveChanges();
            // Synchronize Country table from internal to external
            var internalCountries = internalDb.Countries.ToList();
            var externalCountries = externalDb.Countries.ToList();

            // Track changes in both databases
            var externalChanges = externalDb.ChangeTracker.Entries();
            var internalChanges = internalDb.ChangeTracker.Entries();

            var countryChanges = externalChanges.Where(c => c.Entity.GetType() == typeof(ExCountry)).ToList();

            foreach (var country in countryChanges)
            {
                if(country.State == EntityState.Added)
                {
                    foreach (var exCountry in externalCountries)
                    {
                        var internalCountry = internalCountries.FirstOrDefault(c => c.Id == exCountry.Id);

                        if (internalCountry == null)
                        {
                            internalCountry = new InCountry { Name = exCountry.Name };
                            internalDb.Countries.Add(internalCountry);
                        }
                    }
                }
                else if (country.State == EntityState.Deleted)
                {
                    foreach(var exCountry in externalCountries)
                    {
                        var internalCountry = internalCountries.FirstOrDefault(c => c.Id == exCountry.Id);
                        
                        if(internalCountry == null)
                        {
                            internalDb.Remove(internalCountry);
                        }
                    }
                }
                else if(country.State == EntityState.Modified)
                {
                    foreach (var exCountry in externalCountries)
                    {
                        var internalCountry = internalCountries.FirstOrDefault(c => c.Id == exCountry.Id);

                        if (internalCountry == null)
                        {
                            internalDb.Update(internalCountry);
                        }
                    }
                }
            }

            

            // Synchronize City and Offices tables from external to internal
            var externalCities = externalDb.Cities.ToList();
            var externalOffices = externalDb.Offices.ToList();
            var internalCities = internalDb.Cities.ToList();
            var internalOffices = internalDb.Offices.ToList();

            var cityChanges = externalChanges.Where(c => c.Entity.GetType() == typeof(ExCity)).ToList();

            foreach (var city in cityChanges)
            {
                if (city.State == EntityState.Deleted)
                {
                    foreach (var inCity in internalCountries)
                    {
                        var externalCity = externalCountries.FirstOrDefault(c => c.Id == inCity.Id);

                        if (externalCity == null)
                        {
                            internalDb.Remove(externalCity);
                        }
                    }
                }

                else if (city.State == EntityState.Modified)
                {
                    foreach (var inCity in internalCities)
                    {
                        var externalCity = externalCities.FirstOrDefault(c => c.Id == inCity.Id);
                        if(externalCity == null)
                        {
                            externalDb.Update(externalCity);
                        }
                    }
                }

                else if (city.State == EntityState.Added)
                {

                    foreach (var inCity in internalCities)
                    {
                        var externalCity = externalCities.FirstOrDefault(c => c.Id == inCity.Id);

                        if (externalCity == null)
                        {
                            externalCity = new ExCity { Name = inCity.Name, CountryId = inCity.CountryId };
                            externalDb.Cities.Add(externalCity);
                        }
                    }
                }

            }



            var officeChanges = externalChanges.Where(c => c.Entity.GetType() == typeof(ExOffice)).ToList();


            
            foreach (var office in officeChanges)
            {
                if (office.State == EntityState.Deleted)
                {
                    foreach (var inOffice in internalOffices)
                    {
                        var externalOffice = externalOffices.FirstOrDefault(c => c.Id == inOffice.Id);

                        if (externalOffice == null)
                        {
                            internalDb.Remove(externalOffice);
                        }
                    }
                }

                else if (office.State == EntityState.Modified)
                {
                    foreach (var inOffice in internalCities)
                    {
                        var externalOffice = externalOffices.FirstOrDefault(c => c.Id == inOffice.Id);
                        if(externalOffice == null)
                        {
                            externalDb.Update(externalOffice);
                        }
                    }
                }

                else if (office.State == EntityState.Added)
                {

                    foreach (var inOffice in internalOffices)
                    {
                        var externalOffice = externalOffices.FirstOrDefault(c => c.Id == inOffice.Id);

                        if (externalOffice == null)
                        {
                            externalOffice = new ExOffice { Name = inOffice.Name, CityId = inOffice.CityId };
                            externalDb.Offices.Add(externalOffice);
                        }
                    }
                }

            }

            externalDb.SaveChanges();
            internalDb.SaveChanges();
        }

    }
}

