USE [kama.Aro.Salary]
GO
CREATE OR ALTER PROC wag.spFillOrganProgressReportRaw
AS
BEGIN
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

DROP TABLE [Kama.Aro.Salary.Extention].rpt.OrganProgressReport;

;WITH MainSelect AS 
(
	SELECT
		OrganID, 
		Row_Number() Over(Partition By OrganID ORDER BY OrganID) RowNumber,

		CASE WHEN [Month] = 1 THEN AllWageCount ELSE 0 END AllWageCount1,
		CASE WHEN [Month] = 1 THEN HaveHokmWageCount ELSE 0 END HaveHokmWageCount1,
		CASE WHEN [Month] = 1 THEN HaveNHokmWageCount ELSE 0 END HaveNHokmWageCount1,
		CASE WHEN [Month] = 1 THEN HaveHokmAndNHokmWageCount ELSE 0 END  HaveHokmAndNHokmWageCount1,
		CASE WHEN [Month] = 1 THEN NoHokmInPaknaCount ELSE 0 END NoHokmInPaknaCount1,
		CASE WHEN [Month] = 1 THEN HokmInPaknaNotSalaryCount ELSE 0 END HokmInPaknaNotSalaryCount1,

		CASE WHEN [Month] = 2 THEN AllWageCount ELSE 0 END AllWageCount2,
		CASE WHEN [Month] = 2 THEN HaveHokmWageCount ELSE 0 END HaveHokmWageCount2,
		CASE WHEN [Month] = 2 THEN HaveNHokmWageCount ELSE 0 END HaveNHokmWageCount2,
		CASE WHEN [Month] = 2 THEN HaveHokmAndNHokmWageCount ELSE 0 END  HaveHokmAndNHokmWageCount2,
		CASE WHEN [Month] = 2 THEN NoHokmInPaknaCount ELSE 0 END NoHokmInPaknaCount2,
		CASE WHEN [Month] = 2 THEN HokmInPaknaNotSalaryCount ELSE 0 END HokmInPaknaNotSalaryCount2,

		CASE WHEN [Month] = 3 THEN AllWageCount ELSE 0 END AllWageCount3,
		CASE WHEN [Month] = 3 THEN HaveHokmWageCount ELSE 0 END HaveHokmWageCount3,
		CASE WHEN [Month] = 3 THEN HaveNHokmWageCount ELSE 0 END HaveNHokmWageCount3,
		CASE WHEN [Month] = 3 THEN HaveHokmAndNHokmWageCount ELSE 0 END  HaveHokmAndNHokmWageCount3,
		CASE WHEN [Month] = 3 THEN NoHokmInPaknaCount ELSE 0 END NoHokmInPaknaCount3,
		CASE WHEN [Month] = 3 THEN HokmInPaknaNotSalaryCount ELSE 0 END HokmInPaknaNotSalaryCount3,

		CASE WHEN [Month] = 4 THEN AllWageCount ELSE 0 END AllWageCount4,
		CASE WHEN [Month] = 4 THEN HaveHokmWageCount ELSE 0 END HaveHokmWageCount4,
		CASE WHEN [Month] = 4 THEN HaveNHokmWageCount ELSE 0 END HaveNHokmWageCount4,
		CASE WHEN [Month] = 4 THEN HaveHokmAndNHokmWageCount ELSE 0 END  HaveHokmAndNHokmWageCount4,
		CASE WHEN [Month] = 4 THEN NoHokmInPaknaCount ELSE 0 END NoHokmInPaknaCount4,
		CASE WHEN [Month] = 4 THEN HokmInPaknaNotSalaryCount ELSE 0 END HokmInPaknaNotSalaryCount4,

		CASE WHEN [Month] = 5 THEN AllWageCount ELSE 0 END AllWageCount5,
		CASE WHEN [Month] = 5 THEN HaveHokmWageCount ELSE 0 END HaveHokmWageCount5,
		CASE WHEN [Month] = 5 THEN HaveNHokmWageCount ELSE 0 END HaveNHokmWageCount5,
		CASE WHEN [Month] = 5 THEN HaveHokmAndNHokmWageCount ELSE 0 END  HaveHokmAndNHokmWageCount5,
		CASE WHEN [Month] = 5 THEN NoHokmInPaknaCount ELSE 0 END NoHokmInPaknaCount5,
		CASE WHEN [Month] = 5 THEN HokmInPaknaNotSalaryCount ELSE 0 END HokmInPaknaNotSalaryCount5,

		CASE WHEN [Month] = 6 THEN AllWageCount ELSE 0 END AllWageCount6,
		CASE WHEN [Month] = 6 THEN HaveHokmWageCount ELSE 0 END HaveHokmWageCount6,
		CASE WHEN [Month] = 6 THEN HaveNHokmWageCount ELSE 0 END HaveNHokmWageCount6,
		CASE WHEN [Month] = 6 THEN HaveHokmAndNHokmWageCount ELSE 0 END  HaveHokmAndNHokmWageCount6,
		CASE WHEN [Month] = 6 THEN NoHokmInPaknaCount ELSE 0 END NoHokmInPaknaCount6,
		CASE WHEN [Month] = 6 THEN HokmInPaknaNotSalaryCount ELSE 0 END HokmInPaknaNotSalaryCount6,

		CASE WHEN [Month] = 7 THEN AllWageCount ELSE 0 END AllWageCount7,
		CASE WHEN [Month] = 7 THEN HaveHokmWageCount ELSE 0 END HaveHokmWageCount7,
		CASE WHEN [Month] = 7 THEN HaveNHokmWageCount ELSE 0 END HaveNHokmWageCount7,
		CASE WHEN [Month] = 7 THEN HaveHokmAndNHokmWageCount ELSE 0 END  HaveHokmAndNHokmWageCount7,
		CASE WHEN [Month] = 7 THEN NoHokmInPaknaCount ELSE 0 END NoHokmInPaknaCount7,
		CASE WHEN [Month] = 7 THEN HokmInPaknaNotSalaryCount ELSE 0 END HokmInPaknaNotSalaryCount7,

		CASE WHEN [Month] = 8 THEN AllWageCount ELSE 0 END AllWageCount8,
		CASE WHEN [Month] = 8 THEN HaveHokmWageCount ELSE 0 END HaveHokmWageCount8,
		CASE WHEN [Month] = 8 THEN HaveNHokmWageCount ELSE 0 END HaveNHokmWageCount8,
		CASE WHEN [Month] = 8 THEN HaveHokmAndNHokmWageCount ELSE 0 END  HaveHokmAndNHokmWageCount8,
		CASE WHEN [Month] = 8 THEN NoHokmInPaknaCount ELSE 0 END NoHokmInPaknaCount8,
		CASE WHEN [Month] = 8 THEN HokmInPaknaNotSalaryCount ELSE 0 END HokmInPaknaNotSalaryCount8,

		CASE WHEN [Month] = 9 THEN AllWageCount ELSE 0 END AllWageCount9,
		CASE WHEN [Month] = 9 THEN HaveHokmWageCount ELSE 0 END HaveHokmWageCount9,
		CASE WHEN [Month] = 9 THEN HaveNHokmWageCount ELSE 0 END HaveNHokmWageCount9,
		CASE WHEN [Month] = 9 THEN HaveHokmAndNHokmWageCount ELSE 0 END  HaveHokmAndNHokmWageCount9,
		CASE WHEN [Month] = 9 THEN NoHokmInPaknaCount ELSE 0 END NoHokmInPaknaCount9,
		CASE WHEN [Month] = 9 THEN HokmInPaknaNotSalaryCount ELSE 0 END HokmInPaknaNotSalaryCount9,

		CASE WHEN [Month] = 10 THEN AllWageCount ELSE 0 END AllWageCount10,
		CASE WHEN [Month] = 10 THEN HaveHokmWageCount ELSE 0 END HaveHokmWageCount10,
		CASE WHEN [Month] = 10 THEN HaveNHokmWageCount ELSE 0 END HaveNHokmWageCount10,
		CASE WHEN [Month] = 10 THEN HaveHokmAndNHokmWageCount ELSE 0 END  HaveHokmAndNHokmWageCount10,
		CASE WHEN [Month] = 10 THEN NoHokmInPaknaCount ELSE 0 END NoHokmInPaknaCount10,
		CASE WHEN [Month] = 10 THEN HokmInPaknaNotSalaryCount ELSE 0 END HokmInPaknaNotSalaryCount10,

		CASE WHEN [Month] = 11 THEN AllWageCount ELSE 0 END AllWageCount11,
		CASE WHEN [Month] = 11 THEN HaveHokmWageCount ELSE 0 END HaveHokmWageCount11,
		CASE WHEN [Month] = 11 THEN HaveNHokmWageCount ELSE 0 END HaveNHokmWageCount11,
		CASE WHEN [Month] = 11 THEN HaveHokmAndNHokmWageCount ELSE 0 END  HaveHokmAndNHokmWageCount11,
		CASE WHEN [Month] = 11 THEN NoHokmInPaknaCount ELSE 0 END NoHokmInPaknaCount11,
		CASE WHEN [Month] = 11 THEN HokmInPaknaNotSalaryCount ELSE 0 END HokmInPaknaNotSalaryCount11,

		CASE WHEN [Month] = 12 THEN AllWageCount ELSE 0 END AllWageCount12,
		CASE WHEN [Month] = 12 THEN HaveHokmWageCount ELSE 0 END HaveHokmWageCount12,
		CASE WHEN [Month] = 12 THEN HaveNHokmWageCount ELSE 0 END HaveNHokmWageCount12,
		CASE WHEN [Month] = 12 THEN HaveHokmAndNHokmWageCount ELSE 0 END  HaveHokmAndNHokmWageCount12,
		CASE WHEN [Month] = 12 THEN NoHokmInPaknaCount ELSE 0 END NoHokmInPaknaCount12,
		CASE WHEN [Month] = 12 THEN HokmInPaknaNotSalaryCount ELSE 0 END HokmInPaknaNotSalaryCount12

	FROM [Kama.Aro.Salary.Extention].rpt.OrganProgressReportRaw
)
Select 
	OrganID,

	SUM(AllWageCount1) AllWageCount1,
	SUM(HaveHokmWageCount1) HaveHokmWageCount1,
	SUM(HaveNHokmWageCount1) HaveNHokmWageCount1,
	SUM(HaveHokmAndNHokmWageCount1) HaveHokmAndNHokmWageCount1,
	SUM(NoHokmInPaknaCount1) NoHokmInPaknaCount1,

	SUM(AllWageCount2) AllWageCount2,
	SUM(HaveHokmWageCount2) HaveHokmWageCount2,
	SUM(HaveNHokmWageCount2) HaveNHokmWageCount2,
	SUM(HaveHokmAndNHokmWageCount2) HaveHokmAndNHokmWageCount2,
	SUM(NoHokmInPaknaCount2) NoHokmInPaknaCount2,

	SUM(AllWageCount3) AllWageCount3,
	SUM(HaveHokmWageCount3) HaveHokmWageCount3,
	SUM(HaveNHokmWageCount3) HaveNHokmWageCount3,
	SUM(HaveHokmAndNHokmWageCount3) HaveHokmAndNHokmWageCount3,
	SUM(NoHokmInPaknaCount3) NoHokmInPaknaCount3,

	SUM(AllWageCount4) AllWageCount4,
	SUM(HaveHokmWageCount4) HaveHokmWageCount4,
	SUM(HaveNHokmWageCount4) HaveNHokmWageCount4,
	SUM(HaveHokmAndNHokmWageCount4) HaveHokmAndNHokmWageCount4,
	SUM(NoHokmInPaknaCount4) NoHokmInPaknaCount4,

	SUM(AllWageCount5) AllWageCount5,
	SUM(HaveHokmWageCount5) HaveHokmWageCount5,
	SUM(HaveNHokmWageCount5) HaveNHokmWageCount5,
	SUM(HaveHokmAndNHokmWageCount5) HaveHokmAndNHokmWageCount5,
	SUM(NoHokmInPaknaCount5) NoHokmInPaknaCount5,

	SUM(AllWageCount6) AllWageCount6,
	SUM(HaveHokmWageCount6) HaveHokmWageCount6,
	SUM(HaveNHokmWageCount6) HaveNHokmWageCount6,
	SUM(HaveHokmAndNHokmWageCount6) HaveHokmAndNHokmWageCount6,
	SUM(NoHokmInPaknaCount6) NoHokmInPaknaCount6,

	SUM(AllWageCount7) AllWageCount7,
	SUM(HaveHokmWageCount7) HaveHokmWageCount7,
	SUM(HaveNHokmWageCount7) HaveNHokmWageCount7,
	SUM(HaveHokmAndNHokmWageCount7) HaveHokmAndNHokmWageCount7,
	SUM(NoHokmInPaknaCount7) NoHokmInPaknaCount7,

	SUM(AllWageCount8) AllWageCount8,
	SUM(HaveHokmWageCount8) HaveHokmWageCount8,
	SUM(HaveNHokmWageCount8) HaveNHokmWageCount8,
	SUM(HaveHokmAndNHokmWageCount8) HaveHokmAndNHokmWageCount8,
	SUM(NoHokmInPaknaCount8) NoHokmInPaknaCount8,

	SUM(AllWageCount9) AllWageCount9,
	SUM(HaveHokmWageCount9) HaveHokmWageCount9,
	SUM(HaveNHokmWageCount9) HaveNHokmWageCount9,
	SUM(HaveHokmAndNHokmWageCount9) HaveHokmAndNHokmWageCount9,
	SUM(NoHokmInPaknaCount9) NoHokmInPaknaCount9,
	SUM(HokmInPaknaNotSalaryCount9) HokmInPaknaNotSalaryCount9,

	SUM(AllWageCount10) AllWageCount10,
	SUM(HaveHokmWageCount10) HaveHokmWageCount10,
	SUM(HaveNHokmWageCount10) HaveNHokmWageCount10,
	SUM(HaveHokmAndNHokmWageCount10) HaveHokmAndNHokmWageCount10,
	SUM(NoHokmInPaknaCount10) NoHokmInPaknaCount10,
	SUM(HokmInPaknaNotSalaryCount10) HokmInPaknaNotSalaryCount10,

	SUM(AllWageCount11) AllWageCount11,
	SUM(HaveHokmWageCount11) HaveHokmWageCount11,
	SUM(HaveNHokmWageCount11) HaveNHokmWageCount11,
	SUM(HaveHokmAndNHokmWageCount11) HaveHokmAndNHokmWageCount11,
	SUM(NoHokmInPaknaCount11) NoHokmInPaknaCount11,
	SUM(HokmInPaknaNotSalaryCount11) HokmInPaknaNotSalaryCount11,

	SUM(AllWageCount12) AllWageCount12,
	SUM(HaveHokmWageCount12) HaveHokmWageCount12,
	SUM(HaveNHokmWageCount12) HaveNHokmWageCount12,
	SUM(HaveHokmAndNHokmWageCount12) HaveHokmAndNHokmWageCount12,
	SUM(NoHokmInPaknaCount12) NoHokmInPaknaCount12,
	SUM(HokmInPaknaNotSalaryCount12) HokmInPaknaNotSalaryCount12
	
INTO [Kama.Aro.Salary.Extention].rpt.OrganProgressReport
FROM MainSelect
GROUP BY OrganID
END