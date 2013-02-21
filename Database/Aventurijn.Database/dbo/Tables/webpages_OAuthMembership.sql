CREATE TABLE [dbo].[webpages_OAuthMembership] (
    [Provider]       NVARCHAR (30)  NOT NULL,
    [ProviderUserId] NVARCHAR (100) NOT NULL,
    [UserId]         INT            NOT NULL,
    CONSTRAINT [PK_dbo.webpages_OAuthMembership] PRIMARY KEY CLUSTERED ([Provider] ASC, [ProviderUserId] ASC),
    CONSTRAINT [FK_dbo.webpages_OAuthMembership_dbo.webpages_Membership_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[webpages_Membership] ([UserId]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[webpages_OAuthMembership]([UserId] ASC);

