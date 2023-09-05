USE [Kama.Aro.Salary]
GO

/****** Object:  View [dbo].[GetRand]    Script Date: 12/26/2022 11:39:47 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER   VIEW [dbo].[GetRand]
AS
SELECT RAND() AS Rands
GO