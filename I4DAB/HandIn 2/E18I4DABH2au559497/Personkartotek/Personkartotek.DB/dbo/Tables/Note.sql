CREATE TABLE [dbo].[Note] (
    [NoteId]      INT            IDENTITY (1, 1) NOT NULL,
    [PersonId]    INT            NOT NULL,
    [Description] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Note] PRIMARY KEY CLUSTERED ([NoteId] ASC),
    CONSTRAINT [FK_148] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([PersonId])
);


GO
CREATE NONCLUSTERED INDEX [fkIdx_148]
    ON [dbo].[Note]([PersonId] ASC);

