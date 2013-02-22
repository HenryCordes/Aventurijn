CREATE TABLE [dbo].[Participation] (
    [ParticipationId]       INT            IDENTITY (1, 1) NOT NULL,
    [ExtraInfo]             NVARCHAR (MAX) NULL,
    [Participating]         BIT            NOT NULL,
    [ActivityId]            BIGINT         NOT NULL,
    [StudentId]             INT            NOT NULL,
    [ParticipationDateTime] DATETIME       NOT NULL,
    [Activity_ActivityId]   NVARCHAR (128) NULL,
    CONSTRAINT [PK_dbo.Participation] PRIMARY KEY CLUSTERED ([ParticipationId] ASC),
    CONSTRAINT [FK_dbo.Participation_dbo.Activity_Activity_ActivityId] FOREIGN KEY ([Activity_ActivityId]) REFERENCES [dbo].[Activity] ([ActivityId]),
    CONSTRAINT [FK_dbo.Participation_dbo.Student_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Student] ([StudentId]) ON DELETE CASCADE
);






GO



GO
CREATE NONCLUSTERED INDEX [IX_Activity_ActivityId]
    ON [dbo].[Participation]([Activity_ActivityId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StudentId]
    ON [dbo].[Participation]([StudentId] ASC);

