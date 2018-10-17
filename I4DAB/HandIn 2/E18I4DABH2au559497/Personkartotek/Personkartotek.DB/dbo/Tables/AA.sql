CREATE TABLE [dbo].[AA] (
    [AddressId]     INT            NOT NULL,
    [AlternativeId] INT            IDENTITY (1, 1) NOT NULL,
    [PersonId]      INT            NOT NULL,
    [StreetName]    NVARCHAR (MAX) NOT NULL,
    [HouseNumber]   NVARCHAR (MAX) NOT NULL,
    [City]          NVARCHAR (MAX) NOT NULL,
    [AddressType]   NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [FK_53] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Address] ([AddressId]),
    CONSTRAINT [FK_56] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([PersonId])
);


GO
CREATE NONCLUSTERED INDEX [fkIdx_53]
    ON [dbo].[AA]([AddressId] ASC);


GO
CREATE NONCLUSTERED INDEX [fkIdx_56]
    ON [dbo].[AA]([PersonId] ASC);

