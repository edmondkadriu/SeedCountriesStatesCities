namespace SeedCountries
{
    public interface ICountryService
    {
        List<Country> GetAllCountriesFromJson(string jsonFilePath);
    }
}
