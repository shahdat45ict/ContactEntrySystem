using ContactEntry.Api.Validator;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactEntry.Api.Model
{
    public class Contact
    {
        public int Id { set; get; }
        [JsonProperty("name")]
        public Name Name { set; get; }
        [JsonProperty("address")]
        public Address Address { set; get; }
        [JsonProperty("phone")]
        public List<Phone> phone { set; get; }
        [JsonProperty("email")]
        public string Email { set; get; }

    }
    public class Name
    {
        [JsonProperty("first")]
        public string First { set; get; }
        [JsonProperty("middle")]
        public string Middle { set; get; }
        [JsonProperty("last")]
        public string Last { set; get; }
    }
    public class Address
    {
        [JsonProperty("street")]
        public string Street { set; get; }
        [JsonProperty("city")]
        public string City { set; get; }
        [JsonProperty("state")]
        public string State { set; get; }
        [JsonProperty("zip")]
        public string Zip { set; get; }
    }
    public class Phone
    {
        [JsonProperty("number")]
        public string Number { set; get; }
        [JsonProperty("type")]
        [StringRange(AllowableValues = new[] { "home", "work", "mobile" }, ErrorMessage = "Phone type must be either 'home', 'work' or 'mobile'.")]
        public string Type { set; get; }
    }

}
