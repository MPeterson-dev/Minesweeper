USE [cst350]
GO

SET IDENTITY_INSERT dbo.savedgames ON
GO

CREATE TABLE [dbo].[savedgames] (
    [USERId]        INT           IDENTITY (1, 1) NOT NULL,
    [GAMEId]     INT			 NOT NULL,
    [LIVESITES]  NVARCHAR (200) NOT NULL,
    [TIME]       NVARCHAR (50)  NOT NULL,
    [DATE]  NVARCHAR (50) NOT NULL,
    [BUTTONSTATES]  NVARCHAR (200) NOT NULL
);
insert into savedgames (USERId, GAMEId, LIVESITES, TIME, DATE, BUTTONSTATES) values (1, 45, '12+23+32+32', '12:35PM', '8/30/5', 'Phasellus in felis.');
insert into savedgames (USERId, GAMEId, LIVESITES, TIME, DATE, BUTTONSTATES) values (1, 45, '12+23+32+32', '12:35PM', '8/30/5', 'Phasellus in felis.');
