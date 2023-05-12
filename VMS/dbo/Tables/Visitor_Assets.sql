CREATE TABLE [dbo].[Visitor_Assets] (
    [Id]           INT            NOT NULL,
    [Visitor_Id]   INT            NOT NULL,
    [CarriedAsset] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Visitor_Assets] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Visitor_Assets_Visitor] FOREIGN KEY ([Visitor_Id]) REFERENCES [dbo].[Visitor] ([Id])
);

