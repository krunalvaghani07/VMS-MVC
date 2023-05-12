CREATE TABLE [dbo].[User] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (500) NOT NULL,
    [USER_NAME] NVARCHAR (100) NOT NULL,
    [PASSWORD]  NVARCHAR (MAX) NOT NULL,
    [isActive]  BIT            NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
);

