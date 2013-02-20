CREATE TABLE [dbo].[Subject] (
    [SubjectId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (100) NULL,
    [Code]      NVARCHAR (10)  NULL,
    PRIMARY KEY CLUSTERED ([SubjectId] ASC)
);

