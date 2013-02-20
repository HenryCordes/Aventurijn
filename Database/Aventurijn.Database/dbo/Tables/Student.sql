CREATE TABLE [dbo].[Student] (
    [StudentId]     INT            IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (100) NULL,
    [LastName]      NVARCHAR (100) NULL,
    [BirthDate]     DATETIME       NOT NULL,
    [Level_LevelId] INT            NULL,
    PRIMARY KEY CLUSTERED ([StudentId] ASC),
    CONSTRAINT [Student_Level] FOREIGN KEY ([Level_LevelId]) REFERENCES [dbo].[Level] ([LevelId]) 
);

