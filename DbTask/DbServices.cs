using DbTask.ExternalDb.DbContexts;
using DbTask.ExternalDb.Models;
using DbTask.InternalDb.DbContexts;
using DbTask.InternalDb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DbTask
{
    public class DbServices
    {
        InternalDbContext internalDb = new InternalDbContext();
        ExternalDbContext externalDb = new ExternalDbContext();

        public void AddCountry(InCountry country)
        {
            internalDb.Countries.Add(country);
            externalDb.SaveChanges();
            //externalDb.SaveChanges();
            //SyncEntities(
            //    externalDb.Countries,
            //    internalDb.Countries,
            //    externalCountry => new InCountry
            //    {
            //        Name = externalCountry.Name
            //    },
            //    internalDb);
        }

        public void AddCity(InternalDb.Models.InCity city)
        {
            internalDb.Cities.Add(city);
            internalDb.SaveChanges();

            SyncEntities(
                externalDb.Cities,
                internalDb.Cities,
                externalCountry => new ExternalDb.Models.ExCity
                {
                    Name = externalCountry.Name,
                    CountryId = externalCountry.CountryId
                },
                externalDb);
        }

        public void AddOffice(InOffice office)
        {
            internalDb.Offices.Add(office);
            internalDb.SaveChanges();

            SyncEntities(
                externalDb.Offices,
                internalDb.Offices,
                internalOffice => new ExOffice
                {
                    Name = internalOffice.Name,
                    CityId = internalOffice.CityId
                },
                externalDb);
        }


        /// <summary>
        /// Synchronize actions and add to database.
        /// </summary>
        public void SyncEntities<TExternal, TInternal>(
            DbSet<TExternal> externalEntities,
            DbSet<TInternal> internalEntities,
            Func<TExternal, TInternal> mapFunction,
            DbContext context)
            where TExternal : class
            where TInternal : class
        {
            var externalEntitiesList = externalEntities.ToList();

            var entityType = internalDb.Model.FindEntityType(typeof(TInternal));

            foreach (var externalEntity in externalEntitiesList)
            {
                var primaryKey = GetPrimaryKeyValue(entityType, externalEntity);
                var internalEntity = internalEntities.Find(primaryKey);
                var entity = mapFunction(externalEntity);

                if (internalEntity == null)
                {
                    internalEntities.Add(entity);
                }
                else
                {
                    // Update existing entity with the values from the external entity
                    internalEntities.Entry(internalEntity).CurrentValues.SetValues(entity);
                }
            }
            context.SaveChanges();
        }


        /// <summary>
        /// Synchronize actions and add to database.
        /// </summary>
        public void SyncEntities<TExternal, TInternal>(
          DbSet<TExternal> externalEntities,
          DbSet<TInternal> internalEntities,
          Func<TInternal, TExternal> mapFunction,
          DbContext context)
          where TExternal : class
          where TInternal : class
        {
            var internalEntitiesList = internalEntities.ToList();

            var entityType = internalDb.Model.FindEntityType(typeof(TInternal));

            foreach (var internalEntityValue in internalEntitiesList)
            {
                var primaryKey = GetPrimaryKeyValue(entityType, internalEntityValue);
                var internalEntity = internalEntities.Find(primaryKey);
                var entity = mapFunction(internalEntity);

                externalEntities.Add(entity);

            }
            context.SaveChanges();
        }

        /// <summary>
        /// Get primary key of value
        /// </summary>
        private object GetPrimaryKeyValue<T>(IEntityType entityType, T entity)
        {
            var keyName = entityType.FindPrimaryKey().Properties
                .Select(x => x.Name).Single();

            var keyProperty = entity.GetType().GetProperty(keyName);
            var keyValue = keyProperty.GetValue(entity);

            return keyValue;
        }
    }
}
