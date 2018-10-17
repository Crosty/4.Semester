CREATE TABLE [dbo].[Person] (
    [PersonId]   INT            IDENTITY (1, 1) NOT NULL,
    [PersonType] NVARCHAR (MAX) NOT NULL,
    [FirstName]  NVARCHAR (MAX) NOT NULL,
    [MiddleName] NVARCHAR (MAX) NOT NULL,
    [LastName]   NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED ([PersonId] ASC)
);

