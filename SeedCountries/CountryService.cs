using Newtonsoft.Json;

namespace SeedCountries
{
    public class CountryService
    {   
        public List<Country> GetAllCountriesFromJson(IFormFile file)
        {
            try
            {
                using (var streamReader = new StreamReader(file.OpenReadStream()))
                {
                    var json = streamReader.ReadToEnd();
                    var countriesDto = JsonConvert.DeserializeObject<List<CountryDto>>(json);

                    var countries = countriesDto?.Select(x =>
                    {
                        var countryId = Guid.NewGuid();
                        var stateId = Guid.NewGuid();
                        return new Country()
                        {
                            Id = countryId,
                            Name = x.Name ?? "null",
                            Capital = x.Capital ?? "null",
                            Code = x.Iso2 ?? "null",
                            Nationality = x.Nationality ?? "null",
                            PhoneCode = x.phone_code ?? "null",
                            Region = x.Region ?? "null",
                            Subregion = x.Subregion ?? "null",
                            Timezones = x.Timezones?.Select(z => new Timezone()
                            {
                                Id = Guid.NewGuid(),
                                TimezoneName = z.TzName ?? "null",
                                GmtOffsetName = z.GmtOffsetName ?? "null",
                                CountryId = countryId,
                            }).ToList(),
                            States = x.States?.Select(s => new State()
                            {
                                Id = stateId,
                                CountryId = countryId,
                                Name = s.Name,
                                Latitude = s.latitude,
                                Longitude = s.longitude,
                                Cities = s.Cities?.Select(y => new City()
                                {
                                    Id = Guid.NewGuid(),
                                    Name = y.Name ?? "null",
                                    Latitude = y.latitude ?? "null",
                                    Longitude = y.longitude ?? "null",
                                    StateId = stateId
                                }).ToList() ?? new List<City>()
                            }).ToList()
                        };
                    }).ToList();
                    return countries;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while deserializing JSON data from file: {ex.Message}");
                return null;
            }
        }
    }
}
