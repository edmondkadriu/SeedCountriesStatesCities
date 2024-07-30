namespace SeedCountries
{
    public class CountryDto
    {
        public string Name { get; set; }
        public string Iso2 { get; set; }
        public int Id { get; set; }
        public string Nationality { get; set; }
        public string Capital { get; set; }
        public string phone_code { get; set; }
        public string Region { get; set; }
        public string Subregion { get; set; }
        public List<Tajmzone> Timezones {  get; set; }
        public List<StateDto> States {  get; set; }
    }

    public class StateDto
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public List<CityDto> Cities {  get; set; }
    }

    public class Tajmzone
    {
        public string TzName { get; set; }
        public string GmtOffsetName { get; set; }
    }

    public class CityDto
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
    }
}
