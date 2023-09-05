DECLARE @LawID UNIQUEIDENTIFIER= '206E8E81-6B61-4DC1-B810-729D8FFBA524';
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
--تعداد انتخاب شده
SELECT lwt.WageTitleID, 
       COUNT(DISTINCT OrganID) DepartmentCount
INTO   [Kama.Aro.Salary.Extention].wag.WageTitleReport
FROM wag.LawWageTitle lwt
WHERE LawID = @LawID
GROUP BY lwt.WageTitleID
ORDER BY lwt.WageTitleID DESC;
--تعداد استفاده شده
SELECT lwt.WageTitleID, 
       COUNT(DISTINCT lwt.OrganID) UseDepartmentCount
INTO [Kama.Aro.Salary.Extention].wag.WageTitleReport1
FROM wag._LawWageTitle lwt
     INNER JOIN wag.PayrollDetail pd ON lwt.WageTitleID = pd.WageTitleID
     INNER JOIN wag._Payroll p ON p.ID = pd.PayrollID
WHERE p.LastState = 100
      AND p.Year = 1400
      AND p.LawID = @LawID
GROUP BY lwt.WageTitleID;
--بقیه ستون ها
SELECT wt.Name WageTitleName, 
       wt.IncomeType, 
       wt.ID WageTitleID, 
       MIN(pd.Amount) MinAmount, 
       MAX(pd.Amount) MaxAmount, 
       AVG(pd.Amount) AvgAmount, 
       COUNT(pd.EmployeeID) EmployeeCount
INTO [Kama.Aro.Salary.Extention].wag.WageTitleReport2
FROM wag._Payroll p
     INNER JOIN wag.PayrollDetail pd ON p.ID = pd.PayrollID
     INNER JOIN wag.WageTitle wt ON wt.ID = pd.WageTitleID
WHERE p.LastState = 100
      AND p.Year = 1400
      AND p.LawID = @LawID
GROUP BY wt.Name, 
         wt.IncomeType, 
         wt.ID;