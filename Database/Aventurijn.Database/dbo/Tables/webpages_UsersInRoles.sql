CREATE TABLE [dbo].[webpages_UsersInRoles] (
    [UserId] INT NOT NULL,
    [RoleId] INT NOT NULL,
    CONSTRAINT [PK_dbo.webpages_UsersInRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_dbo.webpages_UsersInRoles_dbo.webpages_Membership_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[webpages_Membership] ([UserId]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.webpages_UsersInRoles_dbo.webpages_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[webpages_Roles] ([RoleId]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[webpages_UsersInRoles]([UserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_RoleId]
    ON [dbo].[webpages_UsersInRoles]([RoleId] ASC);

