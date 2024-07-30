using SeedCountries;
using System;

namespace SeedCountries
{
    public class Country
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Nationality { get; set; }
        public string Capital { get; set; }
        public string PhoneCode { get; set; }
        public string Region { get; set; }
        public string Subregion { get; set; }
        public List<Timezone> Timezones { get; set; } = new List<Timezone>();
        public List<State> States { get; set; } = new List<State>();
    }

    public class State
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public Guid CountryId { get; set; }
        public Country Country { get; set; }
        public List<City> Cities { get; set; } = new List<City>();
    }

    public class City
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public Guid StateId { get; set; }
        public State State { get; set; }
    }

    public class Timezone
    {
        public Guid Id { get; set; }
        public string TimezoneName { get; set; }
        public string GmtOffsetName { get; set; }
        public Guid CountryId { get; set; }
        public Country Country { get; set; }
    }
}
