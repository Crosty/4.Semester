CREATE TABLE [dbo].[ZipList] (
    [ZipId]     INT            NULL,
    [ZipListId] INT            IDENTITY (1, 1) NOT NULL,
    [City]      NVARCHAR (MAX) NOT NULL,
    [Country]   NVARCHAR (MAX) NOT NULL,
    [ZipCode]   NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_ZipList] PRIMARY KEY CLUSTERED ([ZipListId] ASC),
    CONSTRAINT [FK_90] FOREIGN KEY ([ZipId]) REFERENCES [dbo].[Zip] ([ZipId])
);




GO
CREATE NONCLUSTERED INDEX [fkIdx_90]
    ON [dbo].[ZipList]([ZipId] ASC);

