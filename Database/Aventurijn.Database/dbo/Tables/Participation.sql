CREATE TABLE [dbo].[Participation] (
    [ParticipationId]       INT            IDENTITY (1, 1) NOT NULL,
    [ExtraInfo]             NVARCHAR (MAX) NULL,
    [Participating]         BIT            NOT NULL,
    [ParticipationDateTime] DATETIME       NOT NULL,
    [Activity_ActivityId]   NVARCHAR (128) NULL,
    [Student_StudentId]     INT            NULL,
    PRIMARY KEY CLUSTERED ([ParticipationId] ASC),
    CONSTRAINT [Participation_Activity] FOREIGN KEY ([Activity_ActivityId]) REFERENCES [dbo].[Activity] ([ActivityId]),
    CONSTRAINT [Participation_Student] FOREIGN KEY ([Student_StudentId]) REFERENCES [dbo].[Student] ([StudentId])
);

