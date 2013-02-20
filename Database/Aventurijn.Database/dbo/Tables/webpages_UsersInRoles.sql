CREATE TABLE [dbo].[webpages_UsersInRoles] (
    [UserId] INT NOT NULL,
    [RoleId] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [Membership_Roles_Source] FOREIGN KEY ([UserId]) REFERENCES [dbo].[webpages_Membership] ([UserId]) ON DELETE CASCADE,
    CONSTRAINT [Membership_Roles_Target] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[webpages_Roles] ([RoleId]) ON DELETE CASCADE 
);

