CREATE TABLE [dbo].[Visitor] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (MAX) NOT NULL,
    [Address]        NVARCHAR (MAX) NULL,
    [Phone]          NVARCHAR (10)  NULL,
    [Purpose]        NVARCHAR (MAX) NULL,
    [Entry_Time]     DATETIME       NOT NULL,
    [Exit_Time]      DATETIME       NULL,
    [Photo]          NVARCHAR (MAX) NULL,
    [Person_to_Meet] NVARCHAR (50)  NULL,
    [Department]     NVARCHAR (100) NULL,
    [Carried_Assets] NVARCHAR (MAX) NULL,
    [CreatedOn]      DATETIME       NULL,
    [CreatedBy]      INT            NULL,
    [ModifiedOn]     DATETIME       NULL,
    [ModifiedBy]     INT            NULL,
    CONSTRAINT [PK_Visitor] PRIMARY KEY CLUSTERED ([Id] ASC)
);





