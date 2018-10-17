using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using Personkartotek.Infrastructure.PersonkartotekDBADONET;
using Personkartotek.System.Models;

namespace Personkartotek.App
{
    public class App
    {
        public void TheApp()
        {
            var createDbUtil = new PersonkartotekDbUtil();

            //Add a full Person
            //var p1 = new Person()
            //{
            //    PersonId = 1,
            //    PersonType = "Technician",
            //    FirstName = "Søren",
            //    MiddleName = "Trin",
            //    LastName = "Øresø"
            //};

            //createDbUtil.AddPersonDb(ref p1);
            //createDbUtil.GetPersonByName(ref p1);

            //var zl = new ZipList()
            //{
            //    ZipListId = p1.PersonId,
            //    City = "Odense",
            //    Country = "Denmark",
            //    ZipCode = "8420"
            //};

            //createDbUtil.AddZipListDb(ref zl);

            //var zp = new Zip()
            //{
            //    ZipId = zl.ZipListId,
            //    City = zl.City,
            //    Country = zl.Country,
            //    ZipCode = zl.ZipCode,
            //    ZipLists = new List<ZipList>()
            //};

            //zp.ZipLists = new List<ZipList>();
            //createDbUtil.AddZipDb(ref zp);


            //var ads = new Address()
            //{
            //    AddressId = p1.PersonId,
            //    Person = p1.PersonId,
            //    Zip = zp.ZipId,
            //    StreetName = "Haourevej",
            //    HouseNumber = "39",
            //    City = zp.City,
            //    Zips = new List<Zip>()
            //};

            //p1.Addresses = new List<Address>();
            //ads.Zips = new List<Zip>();
            //createDbUtil.AddAddressDb(ref ads);

            //var aa = new AA()
            //{
            //    AlternativeId = p1.PersonId,
            //    Address = ads.AddressId,
            //    Person = p1.PersonId,
            //    StreetName = "Jensvej",
            //    HouseNumber = "9",
            //    City = "Odense",
            //    AddressType = "Sommerhus"
            //};

            //p1.ACollection = new List<AA>();
            //createDbUtil.AddAaDb(ref aa);

            //var ph = new Phone()
            //{
            //    PhoneId = p1.PersonId,
            //    Person = p1.PersonId,
            //    PhoneNumber = "41823749",
            //    PhoneProvider = "TDC",
            //    PhoneType = "Private"
            //};

            //p1.Phones = new List<Phone>();
            //createDbUtil.AddPhoneDb(ref ph);

            //var em = new Email()
            //{
            //    EmailId = p1.PersonId,
            //    Person = p1.PersonId,
            //    EmailAddress = "SørenSø@msn.com"
            //};

            //p1.Emails = new List<Email>();
            //createDbUtil.AddEmailDb(ref em);

            //var n = new Note()
            //{
            //    NoteId = p1.PersonId,
            //    Person = p1.PersonId,
            //    Description = "Ny til gruppen"
            //};

            //p1.Notes = new List<Note>();
            //createDbUtil.AddNoteDb(ref n);

            // ****Test til video****

            //Add a new person
            var p2 = new Person()
            {
                PersonId = 3,
                PersonType = "Leader",
                FirstName = "Peter",
                MiddleName = "Funny",
                LastName = "Pan"
            };

            //createDbUtil.AddPersonDb(ref p2);
            //createDbUtil.DeletePersonDb(ref p2);
            createDbUtil.UpdatePersonDb(ref p2);

            //Console.WriteLine("***Created Person***");
            //Console.WriteLine("***Deleted Person***");
            Console.WriteLine("***Updated Person***");

            //Add a new Zip
            var z2 = new Zip()
            {
                ZipId = 2,
                City = "London",
                Country = "England",
                ZipCode = "idk"
            };

            //createDbUtil.AddZipDb(ref z2);
            //createDbUtil.DeleteZipDb(ref z2);
            //createDbUtil.UpdateZipDb(ref z2);

            //Console.WriteLine("***Created Zip***");
            //Console.WriteLine("***Deleted Zip***");
            //Console.WriteLine("***Updated Zip***");

            //Add an Address to the new person
            //var ads2 = new Address()
            //{
            //    AddressId = 2,
            //    Person = p2.PersonId,
            //    Zip = z2.ZipId,
            //    City = z2.City,
            //    StreetName = "Oregonvej",
            //    HouseNumber = "89"
            //};

            //createDbUtil.AddAddressDb(ref ads2);
            //createDbUtil.DeleteAddressDb(ref ads2);
            //createDbUtil.UpdateAddressDb(ref ads2);

            //Console.WriteLine("***Created Address***");
            //Console.WriteLine("***Deleted Address***");
            //Console.WriteLine("***Updated Address***");
        }
    }
}
