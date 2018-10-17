CREATE TABLE [dbo].[Address] (
    [AddressId]   INT            IDENTITY (1, 1) NOT NULL,
    [PersonId]    INT            NULL,
    [ZipId]       INT            NULL,
    [StreetName]  NVARCHAR (MAX) NOT NULL,
    [HouseNumber] NVARCHAR (MAX) NOT NULL,
    [City]        NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Address] PRIMARY KEY NONCLUSTERED ([AddressId] ASC),
    CONSTRAINT [FK_100] FOREIGN KEY ([ZipId]) REFERENCES [dbo].[Zip] ([ZipId]),
    CONSTRAINT [FK_138] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([PersonId])
);


GO
CREATE NONCLUSTERED INDEX [fkIdx_100]
    ON [dbo].[Address]([ZipId] ASC);


GO
CREATE NONCLUSTERED INDEX [fkIdx_138]
    ON [dbo].[Address]([PersonId] ASC);

