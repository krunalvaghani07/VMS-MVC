CREATE TABLE [dbo].[Role] (
    [Id]        INT            NOT NULL,
    [Role_Name] NVARCHAR (MAX) NOT NULL,
    [isActive]  BIT            NOT NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([Id] ASC)
);

