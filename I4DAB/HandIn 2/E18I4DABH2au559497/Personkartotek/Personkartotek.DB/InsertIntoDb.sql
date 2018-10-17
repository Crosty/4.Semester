SET IDENTITY_INSERT [dbo].[Person] ON
INSERT INTO [dbo].[Person] ([PersonId], [PersonType], [FirstName], [MiddleName], [LastName]) VALUES (3, 'Træt', 'Peter', 'Ulriksen', 'Jensensen')
SET IDENTITY_INSERT [dbo].[Person] OFF

SET IDENTITY_INSERT [dbo].[Address] ON
INSERT INTO [dbo].[Address] ([AddressId], [PersonId], [StreetName], [HouseNumber], [City], [ZipId]) VALUES (3, 3, 'Jernaldervej', '24', 'Aarhus', 3)
SET IDENTITY_INSERT [dbo].[Address] OFF

SET IDENTITY_INSERT [dbo].[Phone] ON
INSERT INTO [dbo].[Phone] ([PhoneId], [PersonId], [PhoneNumber], [PhoneProvider], [PhoneType]) VALUES (3, 3, '44444444', 'Telmore', 'Work')
SET IDENTITY_INSERT [dbo].[Phone] OFF

SET IDENTITY_INSERT [dbo].[Email] ON
INSERT INTO [dbo].[Email] ([EmailId], [PersonId], [EmailAddress]) VALUES (3, 3, 'Peter@hotmail.com')
SET IDENTITY_INSERT [dbo].[Email] OFF

SET IDENTITY_INSERT [dbo].[Note] ON
INSERT INTO [dbo].[Note] ([NoteId], [PersonId], [Description]) VALUES (2, 2, 'Pretty random')
SET IDENTITY_INSERT [dbo].[Note] OFF

SET IDENTITY_INSERT [dbo].[Zip] ON
INSERT INTO [dbo].[Zip] ([ZipId], [City], [Country], [ZipCode]) VALUES (2, 'Aarhus', 'Denmark', '8230')
SET IDENTITY_INSERT [dbo].[Zip] OFF

SET IDENTITY_INSERT [dbo].[ZipList] ON
INSERT INTO [dbo].[ZipList] ([ZipListId], [City], [Country], [ZipCode]) VALUES (2, 'Aarhus', 'Denmark', '8230')
SET IDENTITY_INSERT [dbo].[ZipList] OFF

SET IDENTITY_INSERT [dbo].[AA] ON
INSERT INTO [dbo].[AA] ([AlternativeId], [PersonId], [AddressId], [AddressType], [StreetName], [HouseNumber], [City]) VALUES ('2', '2', '2', 'Private', 'Piskevej', '71', 'Horsens')
SET IDENTITY_INSERT [dbo].[AA] OFF