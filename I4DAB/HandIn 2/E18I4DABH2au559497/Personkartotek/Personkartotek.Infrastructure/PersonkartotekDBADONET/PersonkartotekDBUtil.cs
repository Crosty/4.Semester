using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personkartotek.System.Models;

namespace Personkartotek.Infrastructure.PersonkartotekDBADONET
{
    public class PersonkartotekDbUtil
    {
        private readonly Person _currentPerson;

        public PersonkartotekDbUtil()
        {
            _currentPerson = new Person()
            {
                PersonId = 0,
                PersonType = "",
                FirstName = "",
                MiddleName = "",
                LastName = "",
                Addresses = null,
                ACollection = null,
                Emails = null,
                Phones = null,
                Notes = null
            };
        }

        private SqlConnection OpenConnection
        {
            get
            {
                //Local
                //var con = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=Personkartotek.DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

                //Server
                var con = new SqlConnection("Data Source=st-i4dab.uni.au.dk;Initial Catalog=E18I4DABau559497;User ID=E18I4DABau559497;Password=E18I4DABau559497;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                con.Open();
                return con;
            }
        }

        #region Person
        public void AddPersonDb(ref Person p)
        {
            string insertStringParam =
                @"INSERT INTO [Person] (PersonType, FirstName, MiddleName, LastName) 
                OUTPUT INSERTED.PersonId
                VALUES(@PersonType, @Firstname, @MiddleName, @LastName)";

            using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                //Get your parameters ready
                cmd.Parameters.AddWithValue("@PersonType", p.PersonType);
                cmd.Parameters.AddWithValue("@FirstName", p.FirstName);
                cmd.Parameters.AddWithValue("@MiddleName", p.MiddleName);
                cmd.Parameters.AddWithValue("@LastName", p.LastName);
                p.PersonId = (int)cmd.ExecuteScalar();
            }
        }

        public void UpdatePersonDb(ref Person p)
        {
            string updateString = @"UPDATE Person
                                            SET PersonType = @PersonType,
                                                FirstName = @FirstName,
                                                MiddleName = @MiddleName,
                                                LastName = @LastName
                                            WHERE PersonId = @PersonId";
            using (SqlCommand cmd = new SqlCommand(updateString, OpenConnection))
            {
                //Get your parameters ready
                cmd.Parameters.AddWithValue("@PersonType", p.PersonType);
                cmd.Parameters.AddWithValue("@FirstName", p.FirstName);
                cmd.Parameters.AddWithValue("@MiddleName", p.MiddleName);
                cmd.Parameters.AddWithValue("@LastName", p.LastName);
                cmd.Parameters.AddWithValue("@PersonId", p.PersonId);

                var id = (int)cmd.ExecuteNonQuery();
            }
        }

        public void DeletePersonDb(ref Person p)
        {
            string deleteString = @"DELETE FROM Person WHERE (PersonId = @PersonId)";

            using (SqlCommand cmd = new SqlCommand(deleteString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@PersonId", p.PersonId);

                var id = (int)cmd.ExecuteNonQuery();
                p = null;
            }
        }

        public void GetPersonByName(ref Person p)
        {
            string sqlcmd = @"SELECT TOP 1 PersonId, PersonType, FirstName, MiddleName, LastName FROM Person WHERE (FirstName = @fName) AND (MiddleName = @mName) AND (LastName = @lName)";

            using (var cmd = new SqlCommand(sqlcmd, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@fName", p.FirstName);
                cmd.Parameters.AddWithValue("@mName", p.MiddleName);
                cmd.Parameters.AddWithValue("@lName", p.LastName);
                SqlDataReader reader = null;
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    _currentPerson.PersonId = (int)reader["PersonId"];
                    _currentPerson.PersonType = (string)reader["PersonType"];
                    _currentPerson.FirstName = (string)reader["FirstName"];
                    _currentPerson.MiddleName = (string)reader["MiddleName"];
                    _currentPerson.LastName = (string)reader["LastName"];

                    p = _currentPerson;
                }
            }
        }

        public void GetCurrentPersonById(ref Person p)
        {
            string sqlcmd =
                @"SELECT [PersonType],[FirstName],[MiddleName],[LastName] FROM Person WHERE ([PersonId] = @PersonId)";

            using (var cmd = new SqlCommand(sqlcmd, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@PersonId", p.PersonId);
                SqlDataReader reader = null;
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    _currentPerson.PersonId = p.PersonId;
                    _currentPerson.PersonType = (string)reader["PersonType"];
                    _currentPerson.FirstName = (string)reader["FirstName"];
                    _currentPerson.MiddleName = (string)reader["MiddleName"];
                    _currentPerson.LastName = (string)reader["LastName"];

                    p = _currentPerson;
                }
            }
        }

        public List<Person> GetPeople()
        {
            string sqlcmd = @"SELECT PersonId, PersonType, FirstName, MiddleName, LastName FROM Person";
            using (var cmd = new SqlCommand(sqlcmd, OpenConnection))
            {
                SqlDataReader reader = null;
                reader = cmd.ExecuteReader();
                List<Person> eachPerson = new List<Person>();

                while (reader.Read())
                {
                    var p = new Person
                    {
                        PersonId = (int)reader["PersonId"],
                        PersonType = (string)reader["PersonType"],
                        FirstName = (string)reader["FirstName"],
                        MiddleName = (string)reader["MiddleName"],
                        LastName = (string)reader["LastName"]
                    };

                    eachPerson.Add(p);
                }

                return eachPerson;
            }
        }

        public List<Address> GetPersonsAddress(ref Person p)
        {
            string selectAddressToolString = @"SELECT AddressId, Person, Zip, StreetName, HouseNumber, City FROM [Address] WHERE ([Person] = @PId)";

            using (var cmd = new SqlCommand(selectAddressToolString, OpenConnection))
            {
                SqlDataReader reader = null;
                cmd.Parameters.AddWithValue("@PId", p.PersonId);
                reader = cmd.ExecuteReader();
                List<Address> eachAddress = new List<Address>();

                while (reader.Read())
                {
                    var address = new Address
                    {
                        AddressId = (int)reader["AddressId"],
                        Person = (int) reader["Person"],
                        Zip = (int) reader["Zip"],
                        StreetName = (string)reader["StreetName"],
                        HouseNumber = (string)reader["HouseNumber"],
                        City = (string)reader["City"]
                    };


                    eachAddress.Add(address);
                }

                return eachAddress;
            }
        }

        public List<AA> GetPersonsAlternativeAddress(ref Person p)
        {
            string selectAAToolString = @"SELECT * FROM [AA] WHERE ([Person] = @PId)";

            using (var cmd = new SqlCommand(selectAAToolString, OpenConnection))
            {
                SqlDataReader reader = null;
                cmd.Parameters.AddWithValue("@PId", p.PersonId);
                reader = cmd.ExecuteReader();
                List<AA> eachAList = new List<AA>();

                while (reader.Read())
                {
                    var aaList = new AA
                    {
                        StreetName = (string)reader["StreetName"],
                        HouseNumber = (string)reader["HouseNumber"],
                        City = (string)reader["City"],
                        AddressType = (string)reader["AddressType"]
                    };

                    eachAList.Add(aaList);
                }

                return eachAList;
            }
        }

        public List<Phone> GetPersonsPhone(ref Person p)
        {
            string selectPhoneToolString = @"SELECT * FROM [Phone] WHERE ([Person] = @PId)";

            using (var cmd = new SqlCommand(selectPhoneToolString, OpenConnection))
            {
                SqlDataReader reader = null;
                cmd.Parameters.AddWithValue("@PId", p.PersonId);
                reader = cmd.ExecuteReader();
                List<Phone> eachPhone = new List<Phone>();

                while (reader.Read())
                {
                    var phone = new Phone
                    {
                        PhoneId = (int)reader["PhoneId"],
                        PhoneNumber = (string)reader["PhoneNumber"],
                        PhoneProvider = (string)reader["PhoneProvider"],
                        PhoneType = (string)reader["PhoneType"]
                    };


                    eachPhone.Add(phone);
                }

                return eachPhone;
            }
        }

        public List<Email> GetPersonsEmail(ref Person p)
        {
            string selectEmailToolString = @"SELECT * FROM [Email] WHERE ([Person] = @PId)";

            using (var cmd = new SqlCommand(selectEmailToolString, OpenConnection))
            {
                SqlDataReader reader = null;
                cmd.Parameters.AddWithValue("@PId", p.PersonId);
                reader = cmd.ExecuteReader();
                List<Email> eachEmail = new List<Email>();

                while (reader.Read())
                {
                    var email = new Email
                    {
                        EmailId = (int)reader["EmailId"],
                        EmailAddress = (string)reader["EmailAddress"]
                    };

                    eachEmail.Add(email);
                }

                return eachEmail;
            }
        }

        public List<Note> GetPersonsNote(ref Person p)
        {
            string selectNoteToolString = @"SELECT * FROM Note WHERE ([Person] = @PId)";

            using (var cmd = new SqlCommand(selectNoteToolString, OpenConnection))
            {
                SqlDataReader reader = null;
                cmd.Parameters.AddWithValue("@PId", p.PersonId);
                reader = cmd.ExecuteReader();
                List<Note> eachNote = new List<Note>();

                while (reader.Read())
                {
                    var note = new Note
                    {
                        NoteId = (int) reader["NoteId"],
                        Description = (string)reader["Description"]
                    };

                    eachNote.Add(note);
                }

                return eachNote;
            }
        }

        #endregion

        #region Address

        public void AddAddressDb(ref Address a)
        {
            string insertStringParam = @"INSERT INTO [Address] (PersonId, ZipId, StreetName, HouseNumber, City) 
                                        OUTPUT INSERTED.AddressId
                                        VALUES (@Person, @Zip, @StreetName, @HouseNumber, @City)";

            using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                //Get your parameters ready
                cmd.Parameters.AddWithValue("@Person", a.Person);
                cmd.Parameters.AddWithValue("@Zip", a.Zip);
                cmd.Parameters.AddWithValue("@StreetName", a.StreetName);
                cmd.Parameters.AddWithValue("@HouseNumber", a.HouseNumber);
                cmd.Parameters.AddWithValue("@City", a.City);

                a.AddressId = (int)cmd.ExecuteScalar(); //Returns the identity to the new record
            }
        }

        public void UpdateAddressDb(ref Address a)
        {
            string updateString = @"UPDATE Address
                                    SET PersonId = @Person, ZipId = @Zip, StreetName = @StreetName, HouseNumber = @HouseNumber, City = @City
                                    WHERE AddressId = @AddressId";

            using (SqlCommand cmd = new SqlCommand(updateString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@Person", a.Person);
                cmd.Parameters.AddWithValue("@Zip", a.Zip);
                cmd.Parameters.AddWithValue("@StreetName", a.StreetName);
                cmd.Parameters.AddWithValue("@HouseNumber", a.HouseNumber);
                cmd.Parameters.AddWithValue("@City", a.City);

                var id = (int)cmd.ExecuteNonQuery();
            }
        }

        public void DeleteAddressDb(ref Address a)
        {
            string deleteString = @"DELETE FROM Address WHERE (AddressId = @AddressId)";

            using (SqlCommand cmd = new SqlCommand(deleteString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@AddressId", a.AddressId);

                var id = (int)cmd.ExecuteNonQuery();
                a = null;
            }
        }

        public void GetAddressById(ref Address a)
        {
            string sqlcmd = @"SELECT PersonId, ZipId, StreetName, HouseNumber, City FROM Address WHERE (AddressId = @AId)";

            using (var cmd = new SqlCommand(sqlcmd, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@AId", a.AddressId);
                SqlDataReader reader = null;
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    a.AddressId = (int) reader["AddressId"];
                    a.Person = (int) reader["Person"];
                    a.Zip = (int) reader["Zip"];
                    a.StreetName = (string) reader["StreetName"];
                    a.HouseNumber = (string) reader["HouseNumber"];
                    a.City = (string) reader["City"];
                }
            }
        }

        public List<Zip> GetZipsInAddress(ref Address a)
        {
            string SelectZipInAddressToolString = @"SELECT City, Country, ZipCode FROM ZIP WHERE (Address = @AId)";

            using (var cmd = new SqlCommand(SelectZipInAddressToolString, OpenConnection))
            {
                SqlDataReader reader = null;
                cmd.Parameters.AddWithValue("@AId", a.AddressId);
                reader = cmd.ExecuteReader();
                List<Zip> eachZip = new List<Zip>();

                while (reader.Read())
                {
                    var zip = new Zip
                    {
                        ZipId = (int)reader["ZipId"],
                        City = (string) reader["City"],
                        Country = (string) reader["Country"],
                        ZipCode = (string)reader["ZipCode"]
                    };

                    eachZip.Add(zip);
                }

                return eachZip;
            }
        }

        #endregion

        #region Phone

        public void AddPhoneDb(ref Phone p)
        {
            string InsertStringParam = @"INSERT INTO [Phone] (PersonId, PhoneNumber, PhoneProvider, PhoneType)
                                        OUTPUT INSERTED.PhoneId
                                        VALUES (@Person, @PhoneNumber, @PhoneProvider, @PhoneType)";

            using (SqlCommand cmd = new SqlCommand(InsertStringParam, OpenConnection))
            {
                //Get your parameters ready
                cmd.Parameters.AddWithValue("@Person", p.Person);
                cmd.Parameters.AddWithValue("@PhoneNumber", p.PhoneNumber);
                cmd.Parameters.AddWithValue("@PhoneProvider", p.PhoneProvider);
                cmd.Parameters.AddWithValue("@PhoneType", p.PhoneType);

                p.PhoneId = (int)cmd.ExecuteScalar(); //Returns the identity to the new record
            }
        }

        public void UpdatePhoneDb(ref Phone p)
        {
            string updateString = @"UPDATE Phone
                                    SET Person = @Person, PhoneNumber = @PhoneNumber, PhoneProvider = @PhoneProvider, PhoneType = @PhoneType
                                    WHERE PhoneId = @PhoneId";

            using (SqlCommand cmd = new SqlCommand(updateString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@Person", p.Person);
                cmd.Parameters.AddWithValue("@PhoneNumber", p.PhoneNumber);
                cmd.Parameters.AddWithValue("@PhoneProvider", p.PhoneProvider);
                cmd.Parameters.AddWithValue("@PhoneType", p.PhoneType);

                var id = (int)cmd.ExecuteNonQuery();
            }
        }

        public void DeletePhoneDb(ref Phone p)
        {
            string deleteString = @"DELETE FROM Phone WHERE (PhoneId = @PhoneId)";

            using (SqlCommand cmd = new SqlCommand(deleteString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@PhoneId", p.PhoneId);

                var id = (int) cmd.ExecuteNonQuery();
                p = null;
            }
        }

        public void GetPhoneById(ref Phone p)
        {
            string sqlcmd = @"SELECT Person, PhoneNumber, PhoneProvider, PhoneType
                              FROM Phone WHERE (PhoneId = @PId)";

            using (var cmd = new SqlCommand(sqlcmd, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@PId", p.PhoneId);
                SqlDataReader reader = null;
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {              
                    p.PhoneId = (int)reader["PhoneId"];
                    p.Person = (int) reader["Person"];
                    p.PhoneNumber = (string)reader["PhoneNumber"];
                    p.PhoneProvider = (string)reader["PhoneProvider"];
                    p.PhoneType = (string)reader["PhoneType"];
                }
            }
        }

        #endregion

        #region Email

        public void AddEmailDb(ref Email e)
        {
            string InsertStringParam = @"INSERT INTO [Email] (PersonId, EmailAddress)
                                        OUTPUT INSERTED.EmailId
                                        VALUES (@Person, @EmailAddress)";

            using (SqlCommand cmd = new SqlCommand(InsertStringParam, OpenConnection))
            {
                //Get your parameters ready
                cmd.Parameters.AddWithValue("@Person", e.Person);
                cmd.Parameters.AddWithValue("@EmailAddress", e.EmailAddress);

                e.EmailId = (int)cmd.ExecuteScalar(); //Returns the identity to the new record
            }
        }

        public void UpdateEmailDb(ref Email e)
        {
            string updateString = @"UPDATE Email
                                    SET Person = @Person, EmailAddress = @EmailAddress
                                    WHERE EmailId = @EmailId";

            using (SqlCommand cmd = new SqlCommand(updateString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@Person", e.Person);
                cmd.Parameters.AddWithValue("@EmailAddress", e.EmailAddress);

                var id = (int)cmd.ExecuteNonQuery();
            }
        }

        public void DeleteEmailDb(ref Email e)
        {
            string deleteString = @"DELETE FROM Email WHERE (EmailId = @EmailId)";

            using (SqlCommand cmd = new SqlCommand(deleteString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@EmailId", e.EmailId);

                var id = (int) cmd.ExecuteNonQuery();
                e = null;
            }
        }

        public void GetEmailById(ref Email e)
        {
            string sqlcmd = @"SELECT Person, EmailAddress FROM Email WHERE (EmailId = @EId)";

            using (var cmd = new SqlCommand(sqlcmd, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@EId", e.EmailId);
                SqlDataReader reader = null;
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    e.EmailId = (int) reader["EmailId"];
                    e.Person = (int) reader["Person"];
                    e.EmailAddress = (string) reader["EmailAddress"];
                }
            }
        }

        #endregion

        #region Zip

        public void AddZipDb(ref Zip z)
        {
            string insertStringParam = @"INSERT INTO [Zip] (City, Country, ZipCode)
                                        OUTPUT INSERTED.ZipId
                                        VALUES (@City, @Country, @ZipCode)";

            using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                //Get your parameters ready
                cmd.Parameters.AddWithValue("@City", z.City);
                cmd.Parameters.AddWithValue("@Country", z.Country);
                cmd.Parameters.AddWithValue("@ZipCode", z.ZipCode);

                z.ZipId = (int) cmd.ExecuteScalar(); // Returns the identity of the new record
            }
        }

        public void UpdateZipDb(ref Zip z)
        {
            string updateString = @"UPDATE Zip
                                    SET City = @City, Country = @Country, ZipCode = @ZipCode
                                    WHERE ZipId = @ZipId";

            using (SqlCommand cmd = new SqlCommand(updateString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@City", z.City);
                cmd.Parameters.AddWithValue("@Country", z.Country);
                cmd.Parameters.AddWithValue("@ZipCode", z.ZipCode);
                cmd.Parameters.AddWithValue("@ZipId", z.ZipId);

                var id = (int)cmd.ExecuteNonQuery();
            }
        }

        public void DeleteZipDb(ref Zip z)
        {
            string deleteString = @"DELETE FROM Zip WHERE (ZipId = @ZipId)";

            using (SqlCommand cmd = new SqlCommand(deleteString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@ZipId", z.ZipId);

                var id = (int) cmd.ExecuteNonQuery();
                z = null;
            }
        }

        public void GetZipById(ref Zip z)
        {
            string sqlcmd = @"SELECT * FROM Zip WHERE (ZipId = @ZipId)";

            using (var cmd = new SqlCommand(sqlcmd, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@ZipId", z.ZipId);
                SqlDataReader reader = null;
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {

                    z.ZipId = (int) reader["ZipId"];
                    z.City = (string) reader["City"];
                    z.Country = (string) reader["Country"];
                    z.ZipCode = (string) reader["ZipCode"];
                }
            }
        }

        public List<ZipList> GetZipListInZip(ref Zip z)
        {
            string SelectZipListInZipToolString = @"SELECT * FROM ZipList WHERE ([ZIP] = @ZId)";

            using (var cmd = new SqlCommand(SelectZipListInZipToolString, OpenConnection))
            {
                SqlDataReader reader = null;
                cmd.Parameters.AddWithValue("@ZId", z.ZipId);
                reader = cmd.ExecuteReader();
                List<ZipList> eachZipList = new List<ZipList>();

                while (reader.Read())
                {
                    var zipList = new ZipList
                    {
                        City = (string)reader["City"],
                        Country = (string)reader["Country"],
                        ZipCode = (string)reader["ZipCode"]
                    };

                    eachZipList.Add(zipList);
                }

                return eachZipList;
            }
        }

        #endregion

        #region Note

        public void AddNoteDb(ref Note n)
        {
            string insertStringParam = @"INSERT INTO [Note] (PersonId, Description)
                                        OUTPUT INSERTED.NoteId
                                        VALUES (@Person, @Description)";

            using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@Person", n.Person);
                cmd.Parameters.AddWithValue("@Description", n.Description);

                n.NoteId = (int) cmd.ExecuteScalar(); // Returns the identity of the new record
            }
        }

        public void UpdateNoteDb(ref Note n)
        {
            string updateString = @"UPDATE Note 
                                    SET Person = @Person, Description = @Description
                                    WHERE NoteId = @NoteId";

            using (SqlCommand cmd = new SqlCommand(updateString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@Person", n.Person);
                cmd.Parameters.AddWithValue("@Description", n.Description);

                var id = (int) cmd.ExecuteNonQuery();
            }
        }

        public void DeleteNoteDb(ref Note n)
        {
            string deleteString = @"DELETE FROM Note WHERE (NoteId = @NoteId)";

            using (SqlCommand cmd = new SqlCommand(deleteString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@NoteId", n.NoteId);
                var id = (int) cmd.ExecuteNonQuery();
                n = null;
            }
        }

        public void GetNoteById(ref Note n)
        {
            string sqlcmd = @"SELECT Person, Description FROM Note WHERE (NoteId = @NoteId)";

            using (var cmd = new SqlCommand(sqlcmd, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@NoteId", n.NoteId);
                SqlDataReader reader = null;
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    n.NoteId = (int) reader["NoteId"];
                    n.Person = (int) reader["Person"];
                    n.Description = (string) reader["Description"];
                }
            }
        }

        #endregion

        #region AA

        public void AddAaDb(ref AA aa)
        {
            string insertStringParam = @"INSERT INTO [AA] (PersonId, AddressId, StreetName, HouseNumber, City, AddressType)
                                        OUTPUT INSERTED.AlternativeId
                                        VALUES (@Person, @Address, @StreetName, @HouseNumber, @City, @AddressType)";

            using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@Person", aa.Person);
                cmd.Parameters.AddWithValue("@Address", aa.Address);
                cmd.Parameters.AddWithValue("@StreetName", aa.StreetName);
                cmd.Parameters.AddWithValue("@HouseNumber", aa.HouseNumber);
                cmd.Parameters.AddWithValue("@City", aa.City);
                cmd.Parameters.AddWithValue("@AddressType", aa.AddressType);

                aa.AlternativeId = (int) cmd.ExecuteScalar(); // Returns the identity of the new record
            }
        }

        public void UpdateAaDb(ref AA aa)
        {
            string updateString = @"UPDATE AA
                                    SET Person = @Person, Address = @Address, StreetName = @StreetName, HouseNumber = @HouseNumber, City = @City, AddressType = @AddressType
                                    WHERE AlternativeId = @AlternativeId";

            using (SqlCommand cmd = new SqlCommand(updateString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@Person", aa.Person);
                cmd.Parameters.AddWithValue("@Address", aa.Address);
                cmd.Parameters.AddWithValue("@StreetName", aa.StreetName);
                cmd.Parameters.AddWithValue("@HouseNumber", aa.HouseNumber);
                cmd.Parameters.AddWithValue("@City", aa.City);
                cmd.Parameters.AddWithValue("@AddressType", aa.AddressType);

                var id = (int) cmd.ExecuteNonQuery();
            }
        }

        public void DeleteAaDb(ref AA aa)
        {
            string deleteString = @"DELETE FROM AA WHERE (AlternativeId = @AlternativeId)";

            using (SqlCommand cmd = new SqlCommand(deleteString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@AlternativeId", aa.AlternativeId);

                var id = (int) cmd.ExecuteNonQuery();
                aa = null;
            }
        }

        public void GetAaById(ref AA aa)
        {
            string sqlcmd = @"SELECT Person, Address, StreetName, HouseNumber, City, AddressType
                              FROM AA WHERE (AlternativeId = @AlternativeId)";

            using (var cmd = new SqlCommand(sqlcmd, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@AlternativeId", aa.AlternativeId);
                SqlDataReader reader = null;
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    aa.AlternativeId = (int) reader["AlternativeId"];
                    aa.Person = (int) reader["Person"];
                    aa.Address = (int) reader["Address"];
                    aa.StreetName = (string) reader["StreetName"];
                    aa.HouseNumber = (string) reader["HouseNumber"];
                    aa.City = (string) reader["City"];
                    aa.AddressType = (string) reader["AddressType"];
                }
            }
        }

        #endregion

        #region ZipList

        public void AddZipListDb(ref ZipList zl)
        {
            string insertStringParam = @"INSERT INTO [ZipList] (City, Country, ZipCode)
                                        OUTPUT INSERTED.ZipListId
                                        VALUES (@City, @Country, @ZipCode)";

            using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@City", zl.City);
                cmd.Parameters.AddWithValue("@Country", zl.Country);
                cmd.Parameters.AddWithValue("@ZipCode", zl.ZipCode);
                
                //zl.Zip = (int) cmd.ExecuteScalar();
                zl.ZipListId = (int) cmd.ExecuteScalar(); // Returns the identity of the new record
            }
        }

        public void UpdateZipListDb(ref ZipList zl)
        {
            string updateString = @"UPDATE ZipList
                                    SET City = @City, Country = @Country, ZipCode = @ZipCode
                                    WHERE ZipListId = @ZipListId";

            using (SqlCommand cmd = new SqlCommand(updateString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@City", zl.City);
                cmd.Parameters.AddWithValue("@Country", zl.Country);
                cmd.Parameters.AddWithValue("@ZipCode", zl.ZipCode);

                var id = (int) cmd.ExecuteNonQuery();
            }
        }

        public void DeleteZipListDb(ref ZipList zl)
        {
            string deleteString = @"DELETE FROM ZipList WHERE (ZipListId = @ZipListId)";

            using (SqlCommand cmd = new SqlCommand(deleteString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@ZipListId", zl.ZipListId);

                var id = (int) cmd.ExecuteNonQuery();
                zl = null;
            }
        }

        public void GetZipListById(ref ZipList zl)
        {
            string sqlcmd = @"SELECT City, Country, ZipCode
                            FROM ZipList WHERE (ZipListId = @ZipListId)";

            using (var cmd = new SqlCommand(sqlcmd, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@ZipListId", zl.ZipListId);
                SqlDataReader reader = null;
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    zl.ZipListId = (int) reader["ZipListId"];
                    zl.City = (string) reader["City"];
                    zl.Country = (string) reader["Country"];
                    zl.ZipCode = (string) reader["ZipCode"];
                }
            }
        }



        #endregion

        /*
        public void GetFullTreePersonDb(ref Person fp)
        {
            string getFullTreePersonkartotek =
                @"SELECT        Email.EmailId, Email.EmailAddress, Note.NoteId, Note.Description, Person.PersonId, Person.PersonType, Person.FirstName, Person.MiddleName, Person.LastName, Address.AddressId, Address.StreetName, 
                         Address.HouseNumber, Address.City, Zip.ZipId, Zip.Country, Zip.City AS ZipCity, Zip.ZipCode, Phone.PhoneId, Phone.PhoneNumber, Phone.PhoneType, Phone.PhoneProvider, ZipList.ZipListId, ZipList.City AS ZipListCity, 
                         ZipList.Country AS ZipListCountry, ZipList.ZipCode AS ZipListCode, AA.AlternativeId, AA.StreetName AS AAStreetName, AA.HouseNumber AS AAHouseNumber, AA.City AS AACity, AA.AddressType
                         FROM            Address INNER JOIN
                         Person ON Address.PersonId = Person.PersonId INNER JOIN
                         Phone ON Person.PersonId = Phone.PersonId INNER JOIN
                         Zip ON Address.ZipId = Zip.ZipId INNER JOIN
                         AA ON Address.AddressId = AA.AddressId CROSS JOIN
                         Email CROSS JOIN
                         ZipList CROSS JOIN
                         Note
                         WHERE (Person.PersonId = @PersonId)";

            using (var cmd = new SqlCommand(getFullTreePersonkartotek, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@PersonId", fp.PersonId);
                SqlDataReader reader = null;
                reader = cmd.ExecuteReader();
                var pCount = 0;     //Person
                var aCount = 0;     //Address
                var eCount = 0;     //Email
                var phCount = 0;    //Phone
                var nCount = 0;     //Note
                var aaCount = 0;    //AlternativeAddress - AA
                var zCount = 0;     //Zip
                var zlCount = 0;    //ZipList

                var pId = 0;        //PersonId
                var aId = 0;        //AddressId
                var eId = 0;        //EmailId
                var phId = 0;       //PhoneId
                var nId = 0;        //NoteId
                var aaId = 0;       //AlternativeId
                var zId = 0;        //ZipId
                var zlId = 0;       //ZipListId

                Person p = new Person();
                Address a = null;
                Phone ph = null;
                Zip zp = null;
                Email em = null;
                ZipList zl = null;
                AA aa = null;
                Note n = null;

                p.Addresses = new List<Address>();

                while (reader.Read())
                {
                    var pid = (int) reader["PersonId"];

                    if (pId != pid) //Hent root person
                    {
                        pCount++;
                        p.PersonId = pid;
                        pId = p.PersonId;
                        p.PersonType = (string) reader["PersonType"];
                        p.FirstName = (string) reader["FirstName"];
                        p.MiddleName = (string) reader["MiddleName"];
                        p.LastName = (string) reader["LastName"];
                    }

                    if (!reader.IsDBNull(5))
                    {
                        aCount++;
                        var aid = (int) reader["AddressId"];

                        if (zId != aid)
                        {
                            a = new Address
                            {
                                Zips = new List<Zip>() { },
                                AddressId = aid
                            };
                            p.Addresses.Add(a);
                        }

                        if (a != null)
                        {
                            zId = a.AddressId;
                            a.StreetName = (string) reader["StreetName"];
                            a.HouseNumber = (string) reader["HouseNumber"];
                            a.City = (string) reader["City"];
                        }
                    }
                }
            }
        }
        */
    }
}

