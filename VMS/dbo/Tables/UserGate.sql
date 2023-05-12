CREATE TABLE [dbo].[UserGate] (
    [Id]      INT IDENTITY (1, 1) NOT NULL,
    [User_Id] INT NOT NULL,
    [Gate_Id] INT NOT NULL,
    CONSTRAINT [PK_UserGate] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserGate_Gates] FOREIGN KEY ([Gate_Id]) REFERENCES [dbo].[Gates] ([Id]),
    CONSTRAINT [FK_UserGate_User] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[User] ([Id])
);

