CREATE TABLE [dbo].[Gates] (
    [Id]        INT            NOT NULL,
    [Name]      NVARCHAR (MAX) NOT NULL,
    [isDeleted] BIT            NULL,
    CONSTRAINT [PK_Gates] PRIMARY KEY CLUSTERED ([Id] ASC)
);

