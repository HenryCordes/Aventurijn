CREATE TABLE [dbo].[Participation] (
    [ParticipationId]       INT            IDENTITY (1, 1) NOT NULL,
    [ExtraInfo]             NVARCHAR (MAX) NULL,
    [Participating]         BIT            NOT NULL,
    [ParticipationDateTime] DATETIME       NOT NULL,
    [Activity_ActivityId]   NVARCHAR (128) NULL,
    [Student_StudentId]     INT            NULL,
    CONSTRAINT [PK_dbo.Participation] PRIMARY KEY CLUSTERED ([ParticipationId] ASC),
    CONSTRAINT [FK_dbo.Participation_dbo.Activity_Activity_ActivityId] FOREIGN KEY ([Activity_ActivityId]) REFERENCES [dbo].[Activity] ([ActivityId]),
    CONSTRAINT [FK_dbo.Participation_dbo.Student_Student_StudentId] FOREIGN KEY ([Student_StudentId]) REFERENCES [dbo].[Student] ([StudentId])
);




GO
CREATE NONCLUSTERED INDEX [IX_Student_StudentId]
    ON [dbo].[Participation]([Student_StudentId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Activity_ActivityId]
    ON [dbo].[Participation]([Activity_ActivityId] ASC);

