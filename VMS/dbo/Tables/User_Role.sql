CREATE TABLE [dbo].[User_Role] (
    [Id]      INT IDENTITY (1, 1) NOT NULL,
    [User_Id] INT NOT NULL,
    [Role_Id] INT NOT NULL,
    CONSTRAINT [PK_User_Role] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_User_Role_Role] FOREIGN KEY ([Role_Id]) REFERENCES [dbo].[Role] ([Id]),
    CONSTRAINT [FK_User_Role_User] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[User] ([Id])
);

