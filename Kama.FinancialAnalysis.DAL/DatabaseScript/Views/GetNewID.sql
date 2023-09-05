USE [Kama.Aro.Salary]
GO

/****** Object:  View [dbo].[GetNewID]    Script Date: 12/26/2022 11:38:53 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER  VIEW [dbo].[GetNewID]
AS
SELECT NewId() AS [NewIDs]
GO


