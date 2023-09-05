USE [Kama.Aro.Pakna]

;WITH TotalPakna AS
(
	select
		rowInfo.OrganID,
		COUNT(*) [Count]
	from [emp].[EmployeeInfoByRowNumber] rowInfo
		INNER JOIN [pbl].[EmploymentStatusItemType] es ON es.Code = rowInfo.EmploymentStatus
	WHERE rowInfo.RowNumber = 1
		--AND es.CountType = 2
		AND es.[Type] IN (1, 2) -- افزایش/بلااثر
		AND rowInfo.IssuanceDate >= N'2021-03-21 00:00:00' AND rowInfo.IssuanceDate <= N'2022-04-20 00:00:00' -- سال 1400
	GROUP BY rowInfo.OrganID
)
, Pakna AS
(
	SELECT
		rowInfo.OrganID,
		rowInfo.EmploymentType,
		COUNT(*) [Count]
	from [emp].[EmployeeInfoByRowNumber] rowInfo
		INNER JOIN [pbl].[EmploymentStatusItemType] es ON es.Code = rowInfo.EmploymentStatus
	WHERE rowInfo.RowNumber = 1
		--AND es.CountType = 2
		AND es.[Type] IN (1, 2) -- افزایش/بلااثر
		AND rowInfo.IssuanceDate >= N'2021-03-21 00:00:00' AND rowInfo.IssuanceDate <= N'2022-04-20 00:00:00' -- سال 1400
	GROUP BY rowInfo.OrganID, rowInfo.EmploymentType
)
, Salary AS
(
	SELECT * FROM [Kama.Aro.Salary].[pbl].[reportDivan]
)
,  mSelect AS
(
	SELECT
		dep.ID,
		dep.[BudgetCode],
		dep.[Name],
		COALESCE(TotalPakna.[Count], 0) [Count],
		COALESCE(Salary.TotalCount, 8) TotalCount,
		COALESCE(Salary.InsertCount, 0) InsertCount,
		COALESCE(Salary.EmployeeCount, 0) EmployeeCount

	FROM org.Department dep
		LEFT JOIN TotalPakna ON TotalPakna.OrganID = dep.ID
		LEFT JOIN Salary ON Salary.OrganID = dep.ID
)

SELECT
	mSelect.[BudgetCode] N'کد بودجه',
	mSelect.[Name] N'دستگاه اجرایی',

	COALESCE((SELECT SUM([Count]) FROM Pakna WHERE OrganID = mSelect.ID AND Pakna.EmploymentType = 0), 0) N'نامشخص',
	COALESCE((SELECT SUM([Count]) FROM Pakna WHERE OrganID = mSelect.ID AND Pakna.EmploymentType = 1), 0) N'رسمی قطعی',
	COALESCE((SELECT SUM([Count]) FROM Pakna WHERE OrganID = mSelect.ID AND Pakna.EmploymentType = 2), 0) N'رسمی آزمایشی',
	COALESCE((SELECT SUM([Count]) FROM Pakna WHERE OrganID = mSelect.ID AND Pakna.EmploymentType = 3), 0) N'پیمانی',
	COALESCE((SELECT SUM([Count]) FROM Pakna WHERE OrganID = mSelect.ID AND Pakna.EmploymentType = 6), 0) N'کارگری دائم',
	COALESCE((SELECT SUM([Count]) FROM Pakna WHERE OrganID = mSelect.ID AND Pakna.EmploymentType = 10), 0) N'قراردادی کار معین مشخص',
	COALESCE((SELECT SUM([Count]) FROM Pakna WHERE OrganID = mSelect.ID AND Pakna.EmploymentType = 11), 0) N'قراردادی موقت کارگری',
	COALESCE((SELECT SUM([Count]) FROM Pakna WHERE OrganID = mSelect.ID AND Pakna.EmploymentType = 12), 0) N'مشمول طرح پزشکان و پیراپزشکان',
	COALESCE((SELECT SUM([Count]) FROM Pakna WHERE OrganID = mSelect.ID AND Pakna.EmploymentType = 13), 0) N'بازنشسته مشمول تبصره یک قانون ممنوعیت',
	COALESCE((SELECT SUM([Count]) FROM Pakna WHERE OrganID = mSelect.ID AND Pakna.EmploymentType = 14), 0) N'سرباز امریه',
	COALESCE((SELECT SUM([Count]) FROM Pakna WHERE OrganID = mSelect.ID AND Pakna.EmploymentType = 15), 0) N'بازنشسته مشمول تبصره دو قانون ممنوعیت',
	COALESCE((SELECT SUM([Count]) FROM Pakna WHERE OrganID = mSelect.ID AND Pakna.EmploymentType = 16), 0) N'مشمول طرح عمرانی  شاغل در فعالیت های مربوط به طرح',
	COALESCE((SELECT SUM([Count]) FROM Pakna WHERE OrganID = mSelect.ID AND Pakna.EmploymentType = 18), 0) N'مشمول ماده 3 قانون بازرسی',
	COALESCE((SELECT SUM([Count]) FROM Pakna WHERE OrganID = mSelect.ID AND Pakna.EmploymentType = 19), 0) N'شرکتی پیمانکاری  بکارگیری شده در قالب شرکت تامین نیروی انسانی',
	COALESCE((SELECT SUM([Count]) FROM Pakna WHERE OrganID = mSelect.ID AND Pakna.EmploymentType = 21), 0) N'مشمول طرح عمرانی  شاغل در فعالیت های جاری دستگاه',
	COALESCE((SELECT SUM([Count]) FROM Pakna WHERE OrganID = mSelect.ID AND Pakna.EmploymentType = 22), 0) N'قرارداد حق التدریس',
	COALESCE((SELECT SUM([Count]) FROM Pakna WHERE OrganID = mSelect.ID AND Pakna.EmploymentType = 23), 0) N'قرارداد ساعتی    پاره وقت',
	COALESCE((SELECT SUM([Count]) FROM Pakna WHERE OrganID = mSelect.ID AND Pakna.EmploymentType = 100), 0) N'اشکال دیگر بکارگیری بجز موارد فوق الذکر',

	mSelect.[Count] N'تعداد پاکنا',
	mSelect.TotalCount N'تعداد لیست هایی که باید ثبت می شد',
	mSelect.InsertCount N'تعداد لیست های ثبت شده',
	mSelect.TotalCount - mSelect.InsertCount N'تعداد لیست های ثبت نشده',
	mSelect.EmployeeCount N'تعداد کل کارکنان'
FROM mSelect
WHERE mSelect.TotalCount - mSelect.InsertCount >= 0