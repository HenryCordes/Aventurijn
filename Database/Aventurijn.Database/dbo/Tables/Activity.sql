CREATE TABLE [dbo].[Activity] (
    [ActivityId]        NVARCHAR (128) NOT NULL,
    [Name]              NVARCHAR (100) NULL,
    [CreationDate]      DATETIME       NOT NULL,
    [Subject_SubjectId] INT            NULL,
    PRIMARY KEY CLUSTERED ([ActivityId] ASC),
    CONSTRAINT [Activity_Subject] FOREIGN KEY ([Subject_SubjectId]) REFERENCES [dbo].[Subject] ([SubjectId])
);

