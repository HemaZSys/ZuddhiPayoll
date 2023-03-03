CREATE TABLE [dbo].[Emp] (
    [Id]          INT           NOT NULL,
    [Name]        VARCHAR (50)  NULL,
    [Designation] VARCHAR (50)  NULL,
    [DateofJoin]  DATETIME      NULL,
    [Gender]      NCHAR (50)    NULL,
    [Education]   VARCHAR (50)  NULL,
    [Address]     VARCHAR (500) NULL,
    [Contact]     VARCHAR (50)  NULL,
    [PAN]         VARCHAR (50)  NULL,
    [Aadhar]      VARCHAR (50)  NULL,
    [Passport]    VARCHAR (50)  NULL,
    CONSTRAINT [PK_Emp] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);


