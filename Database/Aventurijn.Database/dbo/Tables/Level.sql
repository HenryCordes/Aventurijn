CREATE TABLE [dbo].[Level] (
    [LevelId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (100) NULL,
    CONSTRAINT [PK_dbo.Level] PRIMARY KEY CLUSTERED ([LevelId] ASC)
);



