using DbTask;
using DbTask.InternalDb.Models;

internal class Program
{
    /// <summary>
    /// This project do not do any changes to build and will be deleted!
    /// </summary>
    private static void Main(string[] args)
    {
        InCountry country = new InCountry()
        {
            Name = "Test"
        };
        DbTask.InternalDb.Models.InCity city = new DbTask.InternalDb.Models.InCity()
        {
            Name = "City1",
            CountryId = 1
        };
        InOffice office = new InOffice()
        {
            Name = "office1",
            CityId = 1
        };

        DbServices externalDbService = new DbServices();
        ChangeTracker changeTracker = new ChangeTracker();


        changeTracker.Synch();

        externalDbService.AddCountry(country);

        changeTracker.Synch();

        //externalDbService.AddCity(city);
        //externalDbService.AddOffice(office);
        Console.WriteLine("Test");
    }
}