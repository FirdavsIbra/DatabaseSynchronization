using DbTask;
using DbTask.ExternalDb.Models;
using DbTask.InternalDb.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        ExCountry country = new ExCountry()
        {
            Name = "Test"
        };
        InCity city = new InCity()
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
        //externalDbService.AddCountry(country);
        externalDbService.AddCity(city);
        externalDbService.AddOffice(office);
        Console.WriteLine("Test");
    }
}