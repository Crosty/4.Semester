using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace quickstartcore.Models
{
    public class Person
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "personType")]
        public string PersonType { get; set; }
        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }
        [JsonProperty(PropertyName = "middleName")]
        public string MiddleName { get; set; }
        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }
        [JsonProperty("email")]
        public Email Email { get; set; }
    }

    public class Address
    {
        [JsonProperty(PropertyName = "addressId")]
        public string AddressId { get; set; }
        [JsonProperty(PropertyName = "streetName")]
        public string StreetName { get; set; }
        [JsonProperty(PropertyName = "houseNumber")]
        public string HouseNumber { get; set; }

        [JsonProperty("zip")]
        public Zip Zip { get; set; }
    }

    public class Email
    {
        [JsonProperty(PropertyName = "emailId")]
        public string EmailId { get; set; }
        [JsonProperty(PropertyName = "emailAddress")]
        public string EmailAddress { get; set; }
    }

    public class Zip
    {
        [JsonProperty(PropertyName = "zipId")]
        public string ZipId { get; set; }
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }
        [JsonProperty(PropertyName = "zipCode")]
        public string ZipCode { get; set; }
    }
}
