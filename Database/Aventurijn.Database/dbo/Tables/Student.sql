CREATE TABLE [dbo].[Student] (
    [StudentId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (100) NULL,
    [LastName]  NVARCHAR (100) NULL,
    [Insertion] NVARCHAR (10)  NULL,
    [BirthDate] DATETIME       NOT NULL,
    [LevelId]   INT            NOT NULL,
    CONSTRAINT [PK_dbo.Student] PRIMARY KEY CLUSTERED ([StudentId] ASC),
    CONSTRAINT [FK_dbo.Student_dbo.Level_LevelId] FOREIGN KEY ([LevelId]) REFERENCES [dbo].[Level] ([LevelId]) ON DELETE CASCADE
);






GO
CREATE NONCLUSTERED INDEX [IX_LevelId]
    ON [dbo].[Student]([LevelId] ASC);

