using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace quickstartcore.Models
{
    public class Person
    {
        [JsonProperty(PropertyName = "personId", Required = Required.Always)]
        public string PersonId { get; set; }
        [JsonProperty(PropertyName = "personType", Required = Required.AllowNull)]
        public string PersonType { get; set; }
        [JsonProperty(PropertyName = "firstName", Required = Required.Always)]
        public string FirstName { get; set; }
        [JsonProperty(PropertyName = "middleName", Required = Required.AllowNull)]
        public string MiddleName { get; set; }
        [JsonProperty(PropertyName = "lastName", Required = Required.Always)]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "address", Required = Required.AllowNull)]
        public Address Address { get; set; }
        [JsonProperty(PropertyName = "email", Required = Required.AllowNull)]
        public Email Email { get; set; }
    }

    public class Address
    {
        [JsonProperty(PropertyName = "addressId", Required = Required.Always)]
        public string AddressId { get; set; }

        //[JsonProperty(PropertyName = "personId", Required = Required.Always)]
        //public string PersonId { get; set; }
        //[JsonProperty(PropertyName = "zipId", Required = Required.Always)]
        //public string ZipId { get; set; }

        [JsonProperty(PropertyName = "streetName", Required = Required.Always)]
        public string StreetName { get; set; }
        [JsonProperty(PropertyName = "houseNumber", Required = Required.Always)]
        public string HouseNumber { get; set; }

        //[JsonProperty(PropertyName = "person", Required = Required.AllowNull)]
        //public Person Person { get; set; }
        [JsonProperty(PropertyName = "zip", Required = Required.AllowNull)]
        public Zip Zip { get; set; }
    }

    public class Email
    {
        [JsonProperty(PropertyName = "emailId", Required = Required.Always)]
        public string EmailId { get; set; }

        //[JsonProperty(PropertyName = "personId", Required = Required.Always)]
        //public string PersonId { get; set; }

        [JsonProperty(PropertyName = "emailAddress", Required = Required.Always)]
        public string EmailAddress { get; set; }
    }

    public class Zip
    {
        [JsonProperty(PropertyName = "zipId", Required = Required.Always)]
        public string ZipId { get; set; }
        [JsonProperty(PropertyName = "city", Required = Required.Always)]
        public string City { get; set; }
        [JsonProperty(PropertyName = "country", Required = Required.Always)]
        public string Country { get; set; }
        [JsonProperty(PropertyName = "zipCode", Required = Required.Always)]
        public string ZipCode { get; set; }
    }
}
