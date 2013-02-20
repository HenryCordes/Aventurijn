CREATE TABLE [dbo].[webpages_OAuthMembership] (
    [Provider]       NVARCHAR (30)  NOT NULL,
    [ProviderUserId] NVARCHAR (100) NOT NULL,
    [UserId]         INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Provider] ASC, [ProviderUserId] ASC),
    CONSTRAINT [OAuthMembership_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[webpages_Membership] ([UserId]) ON DELETE CASCADE
);

