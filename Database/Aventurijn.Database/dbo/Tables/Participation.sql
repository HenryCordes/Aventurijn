CREATE TABLE [dbo].[Participation] (
    [ParticipationId]               INT            IDENTITY (1, 1) NOT NULL,
    [ExtraInfo]                     NVARCHAR (MAX) NULL,
    [Participating]                 BIT            NOT NULL,
    [SubjectId]                     BIGINT         NOT NULL,
    [ActivityId]                    BIGINT         NOT NULL,
    [StudentId]                     INT            NOT NULL,
    [ParticipationDateTime]         DATETIME       NOT NULL,
    [ParticipationDateTimeAsString] NVARCHAR (MAX) NULL,
    [Subject_SubjectId]             INT            NULL,
    CONSTRAINT [PK_dbo.Participation] PRIMARY KEY CLUSTERED ([ParticipationId] ASC),
    CONSTRAINT [FK_dbo.Participation_dbo.Activity_ActivityId] FOREIGN KEY ([ActivityId]) REFERENCES [dbo].[Activity] ([ActivityId]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.Participation_dbo.Student_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Student] ([StudentId]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.Participation_dbo.Subject_Subject_SubjectId] FOREIGN KEY ([Subject_SubjectId]) REFERENCES [dbo].[Subject] ([SubjectId])
);

GO
CREATE NONCLUSTERED INDEX [IX_ParticipationDateTime]
    ON [dbo].[Participation]([ParticipationDateTime] ASC);

GO










GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_StudentId]
    ON [dbo].[Participation]([StudentId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ActivityId]
    ON [dbo].[Participation]([ActivityId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Subject_SubjectId]
    ON [dbo].[Participation]([Subject_SubjectId] ASC);

