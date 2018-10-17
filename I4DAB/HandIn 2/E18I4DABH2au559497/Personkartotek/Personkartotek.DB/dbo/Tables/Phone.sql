CREATE TABLE [dbo].[Phone] (
    [PhoneId]       INT            IDENTITY (1, 1) NOT NULL,
    [PhoneNumber]   NVARCHAR (MAX) NOT NULL,
    [PhoneProvider] NVARCHAR (MAX) NOT NULL,
    [PhoneType]     NVARCHAR (MAX) NOT NULL,
    [PersonId]      INT            NOT NULL,
    CONSTRAINT [PK_Phone] PRIMARY KEY CLUSTERED ([PhoneId] ASC),
    CONSTRAINT [FK_152] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([PersonId])
);


GO
CREATE NONCLUSTERED INDEX [fkIdx_152]
    ON [dbo].[Phone]([PersonId] ASC);

