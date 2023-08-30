USE [cst350]
GO

CREATE TABLE [dbo].[savedgames] (
    [USERId]        INT           IDENTITY (1, 1) NOT NULL,
    [GAMEId]     INT			 NOT NULL,
	[GAMENAME] NVARCHAR (100) NOT NULL,
    [LIVESITES]  NVARCHAR (200) NOT NULL,
    [TIME]       NVARCHAR (50)  NOT NULL,
    [DATE]  NVARCHAR (50) NOT NULL,
    [BUTTONSTATES]  NVARCHAR (200) NOT NULL
);

SET IDENTITY_INSERT dbo.savedgames ON
GO

insert into savedgames (USERId, GAMEId, GAMENAME, LIVESITES, TIME, DATE, BUTTONSTATES) values (1, 45,'TestGame', '12+23+32+32', '12:35PM', '8/30/5', 'Phasellus in felis.');
insert into savedgames (USERId, GAMEId, GAMENAME, LIVESITES, TIME, DATE, BUTTONSTATES) values (1, 45,'TestGame2', '12+23+32+32', '12:35PM', '8/30/5', 'Phasellus in felis.');
