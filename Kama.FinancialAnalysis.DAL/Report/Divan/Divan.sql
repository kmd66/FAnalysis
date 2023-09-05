DELETE [pbl].[reportDivan]
;WITH LawCount AS 
(  -- قانون
	SELECT 
		OrganID ID, 
		COUNT(*) Cnt
	FROM law.organlaw 
	WHERE [Enabled] = 1
	GROUP BY OrganID
)
, PayrollCount AS     -- لیست حقوق
(
	SELECT 
		OrganID ID, 
		Count(*) Cnt
	FROM wag.Payroll 
		INNER JOIN pbl.BaseDocument doc ON Payroll.ID = doc.ID
		INNER JOIN pbl.DocumentFlow ConfirmFlow ON doc.ID = ConfirmFlow.DocumentID AND ConfirmFlow.ToDocState = 100 AND ConfirmFlow.ActionDate IS NULL
	WHERE RemoverID IS NULL and Year in(1400,1401) AND LTRIM(Rtrim( Cast(Year as varchar)+Cast(Month as varchar))) in('14001','14002','14003','14004','14005','14006','14007','14008','14009','140010','140011','140012','14011')
	GROUP BY OrganID
)
, EmployeeCount AS
(      -- لیست کارکنان
	SELECT 
		Payroll.OrganID ID,
		COUNT(DISTINCT w.EmployeeID) [Count]
	FROM wag.PayrollEmployee w
		INNER JOIN wag._Payroll Payroll ON payroll.ID = w.PayrollID
	where  Payroll.Year = 1400  AND Cast(Year as varchar)+Cast(Month as varchar) in('14001','14002','14003','14004','14005','14006','14007','14008','14009','140010','140011','140012','14011')
	GROUP BY Payroll.OrganID
)
, MainSelect AS
(
	SELECT
		Department.ID DepartmentID,
		Department.[Name] DepartmentName, --N'عنوان دستگاه اجرایی',
		LawCount.cnt * 13 TotalCount,-- N'تعدادی که باید ثبت می شد',
		PayrollCount.Cnt InsertCount,-- N'تعدادی که  ثبت شده',
		EmployeeCount.Count EmployeeCount --N'نعداد کل کارمندان'
	FROM PayrollCount
		INNER JOIN EmployeeCount on EmployeeCount.ID=PayrollCount.ID
		INNER JOIN org._Department Department on Department.ID=PayrollCount.ID
		INNER JOIN LawCount on LawCount.ID = PayrollCount.ID
	--ORDER BY [Name]
)

insert into [pbl].[reportDivan]
([ID], [OrganID], [TotalCount], [InsertCount], [EmployeeCount])
SELECT
 NEWID() [ID],
 DepartmentID [OrganID],
 [TotalCount],
 [InsertCount],
 [EmployeeCount]
FROM MainSelect

--select * from [pbl].[reportDivan]



