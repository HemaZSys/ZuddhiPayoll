CREATE TABLE [dbo].[Emp] (
    [Id]         INT             NOT NULL,
    [Name]       VARCHAR (50)    NULL,
    [Address]    VARCHAR (100)   NULL,
    [Balance]    DECIMAL (18, 2) NULL,
    [Remarks]    NCHAR (500)     NULL,
    [CreateDate] DATETIME        NULL,
    [UpdateDate] DATETIME        NULL,
    [bizId]      VARCHAR (50)    NULL,
    CONSTRAINT [PK_Emp] PRIMARY KEY CLUSTERED ([Id] ASC)
	);