CREATE TABLE [dbo].[Activity] (
    [ActivityId]   BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (100) NULL,
    [CreationDate] DATETIME       NOT NULL,
    [SubjectId]    INT            NOT NULL,
    CONSTRAINT [PK_dbo.Activity] PRIMARY KEY CLUSTERED ([ActivityId] ASC),
    CONSTRAINT [FK_dbo.Activity_dbo.Subject_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [dbo].[Subject] ([SubjectId]) ON DELETE CASCADE
);








GO
CREATE NONCLUSTERED INDEX [IX_SubjectId]
    ON [dbo].[Activity]([SubjectId] ASC);

