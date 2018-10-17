CREATE TABLE [dbo].[Zip] (
    [ZipId]   INT            IDENTITY (1, 1) NOT NULL,
    [City]    NVARCHAR (MAX) NOT NULL,
    [Country] NVARCHAR (MAX) NOT NULL,
    [ZipCode] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_ZIP] PRIMARY KEY CLUSTERED ([ZipId] ASC)
);

