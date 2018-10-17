SELECT        Email.EmailId, Email.EmailAddress, Note.NoteId, Note.Description, Person.PersonId, Person.PersonType, Person.FirstName, Person.MiddleName, Person.LastName, Address.AddressId, Address.StreetName, 
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