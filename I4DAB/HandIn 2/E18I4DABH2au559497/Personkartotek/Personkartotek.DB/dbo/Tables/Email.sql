CREATE TABLE [dbo].[Email] (
    [EmailId]      INT            IDENTITY (1, 1) NOT NULL,
    [PersonId]     INT            NOT NULL,
    [EmailAddress] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Email] PRIMARY KEY CLUSTERED ([EmailId] ASC),
    CONSTRAINT [FK_144] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([PersonId])
);


GO
CREATE NONCLUSTERED INDEX [fkIdx_144]
    ON [dbo].[Email]([PersonId] ASC);

