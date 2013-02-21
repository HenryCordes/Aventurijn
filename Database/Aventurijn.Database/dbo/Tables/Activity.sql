CREATE TABLE [dbo].[Activity] (
    [ActivityId]        NVARCHAR (128) NOT NULL,
    [Name]              NVARCHAR (100) NULL,
    [CreationDate]      DATETIME       NOT NULL,
    [Subject_SubjectId] INT            NULL,
    CONSTRAINT [PK_dbo.Activity] PRIMARY KEY CLUSTERED ([ActivityId] ASC),
    CONSTRAINT [FK_dbo.Activity_dbo.Subject_Subject_SubjectId] FOREIGN KEY ([Subject_SubjectId]) REFERENCES [dbo].[Subject] ([SubjectId])
);




GO
CREATE NONCLUSTERED INDEX [IX_Subject_SubjectId]
    ON [dbo].[Activity]([Subject_SubjectId] ASC);

