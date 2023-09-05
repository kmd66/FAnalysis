--set TRANSACTION ISOLATION LEVEL  READ UNCOMMITTED;
--with pay as 
--(

if OBJECT_ID('tempdb..#tempPay','U') is not null
DROP TABLE #tempPay


SELECT 
	Payroll.ID,
	Payroll.OrganID
	--	Law.Name LawName
into #tempPay
FROM wag.Payroll
	INNER JOIN pbl.BaseDocument doc ON doc.ID = Payroll.ID
	INNER JOIN pbl.DocumentFlow LastFlow ON LastFlow.DocumentID = Payroll.ID AND LastFlow.ActionDate IS NULL
	--INNER JOIN law.Law ON law.ID = payroll.LawID
WHERE doc.RemoverID IS NULL AND LastFlow.ToDocState = 100
	--and pay.OrganName like N'%استانداری%'
	and year = 1400
--)


;with emp as 
(
	select emp.Sum,emp.Deductions,emp.PayrollID, pay.OrganID from
	[wag].[PayrollEmployee] emp WITH(NOLOCK)
	inner join #tempPay pay WITH(NOLOCK) on emp.PayrollID = pay.id
	--where sum >500000000
)

SELECT 
	org.MainOrgan1Name,
	org.ParentName,
	org.Name,org.BudgetCode,
	-- org.MainOrgan2Name
	CMN.DepartmentSubType.DstTitle,
	SUM(ISNULL(Sum, 0)) Sums,
	SUM(ISNULL(Deductions,0)) Deductions,
	avg( CAST(ISNULL(Sum,0) as DECIMAL)) AVGs
FROM org._department org 
	LEFT JOIN emp WITH(NOLOCK) on org.id = emp.OrganID
	LEFT JOIN CMN.DepartmentSubType on CMN.DepartmentSubType.DstID=org.SubType
WHERE org.Type <> 10
GROUP BY org.MainOrgan1Name,org.ParentName,org.Name,org.BudgetCode, --, org.MainOrgan2Name
	CMN.DepartmentSubType.DstTitle
ORDER BY org.MainOrgan1Name,org.ParentName,org.Name 

