USE [kama.Aro.Salary]
GO
CREATE OR ALTER PROC wag.spFillOrganProgressReportRaw
AS
    BEGIN
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DROP TABLE [Kama.Aro.Salary.Extention].rpt.OrganProgressReportRaw

--تعداد کدهای ملی که اقلام حکم برایشان ثبت شده
;WITH AllWage AS 
(
	SELECT 
		Month,
		p.OrganID,
		COUNT(DISTINCT ed.NationalCode) [Count]
	FROM wag._Payroll p
		INNER JOIN wag.PayrollEmployee pe on pe.PayrollID = p.ID
		INNER JOIN pbl.EmployeeDetail ed on ed.ID = pe.EmployeeID
	where p.LastState = 100 
		AND p.Year =1400  
	GROUP BY 
		Month,
		p.OrganID
)
, HaveHokmWage AS 
(
	SELECT 
		Month,
		p.OrganID,
		COUNT(DISTINCT ed.NationalCode) [Count]
	FROM wag._Payroll p 
		INNER JOIN wag.PayrollEmployee pe on pe.PayrollID = p.ID
		INNER JOIN pbl.EmployeeDetail ed on ed.ID = pe.EmployeeID
	where p.LastState = 100 
		AND p.Year =1400  
		AND pe.SumHokm <> 0
		AND pe.SumNHokm =0
	GROUP BY 
		Month,
		p.OrganID
)
, HaveNHokmWage AS 
(
	SELECT 
		Month,
		p.OrganID,
		COUNT(DISTINCT ed.NationalCode) [Count]
	FROM wag._Payroll p
		INNER JOIN wag.PayrollEmployee pe on pe.PayrollID = p.ID
		INNER JOIN pbl.EmployeeDetail ed on ed.ID = pe.EmployeeID
	where p.LastState = 100 
		AND p.Year =1400  
		AND pe.SumHokm =0
		AND pe.SumNHokm <> 0
	GROUP BY 
		Month,
		p.OrganID
)
, HaveHokmAndNHokmWage AS 
(
	SELECT 
		Month,
		p.OrganID,
		COUNT(DISTINCT ed.NationalCode) [Count]
	FROM wag._Payroll p
		INNER JOIN wag.PayrollEmployee pe on pe.PayrollID = p.ID
		INNER JOIN pbl.EmployeeDetail ed on ed.ID = pe.EmployeeID
	where p.LastState = 100 
		AND p.Year =1400  
		AND pe.SumHokm <> 0
		AND pe.SumNHokm <> 0
	GROUP BY 
		Month,
		p.OrganID
)
, NoHokmInPakna AS 
(
	SELECT 
		Month,
		p.OrganID,
		COUNT(DISTINCT ed.NationalCode) [Count]
	FROM wag._Payroll p
		INNER JOIN wag.PayrollEmployee pe on pe.PayrollID = p.ID
		INNER JOIN pbl.EmployeeDetail ed on ed.ID = pe.EmployeeID
	where p.LastState = 100 
		AND p.Year =1400  
		AND pe.EmployeeInfoID IS NULL
	GROUP BY 
		Month,
		p.OrganID
)
,PayrollEmployee AS
(
SELECT p.Month,p.OrganID,pe.EmployeeInfoID FROM wag._Payroll p
INNER JOIN wag.PayrollEmployee pe ON p.ID=pe.PayrollID
where p.LastState=100 and p.Year=1400 
)
SELECT
	AllWage.Month,
	AllWage.OrganID,
	ISNULL(AllWage.[Count], 0) AllWageCount,
	ISNULL(HaveHokmWage.[Count], 0) HaveHokmWageCount,
	ISNULL(HaveNHokmWage.[Count], 0) HaveNHokmWageCount,
	ISNULL(HaveHokmAndNHokmWage.[Count], 0) HaveHokmAndNHokmWageCount,
	ISNULL(NoHokmInPakna.[Count], 0) NoHokmInPaknaCount,
	ISNULL(HokmInPaknaNotSalary.EmplyeeCount, 0) HokmInPaknaNotSalaryCount
INTO [Kama.Aro.Salary.Extention].rpt.OrganProgressReportRaw
FROM AllWage 
	LEFT JOIN HaveHokmWage ON HaveHokmWage.OrganID = AllWage.OrganID AND HaveHokmWage.Month =AllWage.Month
	LEFT JOIN HaveNHokmWage ON HaveNHokmWage.OrganID = AllWage.OrganID AND HaveNHokmWage.Month =AllWage.Month
	LEFT JOIN HaveHokmAndNHokmWage ON HaveHokmAndNHokmWage.OrganID = AllWage.OrganID AND HaveHokmAndNHokmWage.Month =AllWage.Month
	LEFT JOIN NoHokmInPakna ON NoHokmInPakna.OrganID = AllWage.OrganID AND NoHokmInPakna.Month =AllWage.Month
	LEFT JOIN [Kama.Aro.Salary.Extention].rpt.HokmInPaknaNotSalary ON HokmInPaknaNotSalary.OrganID = AllWage.OrganID AND HokmInPaknaNotSalary.Month =AllWage.Month
END
