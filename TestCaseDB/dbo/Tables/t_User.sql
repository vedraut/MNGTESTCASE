CREATE TABLE [dbo].[t_User] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [FirstName]     NVARCHAR (50) NOT NULL,
    [LastName]      NVARCHAR (50) NOT NULL,
    [DateOfBirth]   DATETIME      NULL,
    [Gender]        NVARCHAR (10) NULL,
    [Email]         NVARCHAR (50) NULL,
    [ContactNumber] NVARCHAR (20) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

