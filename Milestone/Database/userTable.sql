USE [cst350]
GO

/****** Object: Table [dbo].[users] Script Date: 8/5/2023 3:24:49 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[users] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [FIRSTNAME] NVARCHAR (50) NOT NULL,
    [LASTNAME]  NVARCHAR (50) NOT NULL,
    [SEX]       NVARCHAR (1)  NOT NULL,
    [AGE]       INT           NOT NULL,
    [STATE]     NVARCHAR (50) NOT NULL,
    [EMAIL]     NVARCHAR (50) NOT NULL,
    [USERNAME]  NVARCHAR (50) NOT NULL,
    [PASSWORD]  NVARCHAR (50) NOT NULL
);


