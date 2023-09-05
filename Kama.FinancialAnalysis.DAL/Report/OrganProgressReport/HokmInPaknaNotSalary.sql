CREATE OR ALTER PROC wag.spFillHokmInPaknaNotSalary
AS
    BEGIN
        --*********************************************************************************************************1
        WITH PayrollEmployee AS 
	       (
	             SELECT p.Month, 
                        p.OrganID, 
                        pe.EmployeeInfoID, 
                        p.ID
                 FROM wag._Payroll p
                      INNER JOIN wag.PayrollEmployee pe ON p.ID = pe.PayrollID
                 WHERE p.LastState = 100
                       AND p.Year = 1400
                       AND pe.EmployeeInfoID IS NOT NULL
                       AND p.Month = 1
		   )
           INSERT INTO [Kama.Aro.Salary.Extention].rpt.HokmInPaknaNotSalary
                    SELECT info.[OrganID], 
                           CAST(1 AS TINYINT) Month, 
                           COUNT(*) [EmplyeeCount]
                    FROM [Kama.Aro.Pakna].[emp].[EmployeeInfoByRowNumber2] info
                         INNER JOIN [Kama.Aro.Pakna].[pbl].[EmploymentStatusItemType] employmentStatusItemType ON employmentStatusItemType.[Code] = info.EmploymentStatus
                         LEFT JOIN PayrollEmployee ON PayrollEmployee.EmployeeInfoID = info.ID
                    WHERE info.RowNumber = 1
                          AND PayrollEmployee.EmployeeInfoID IS NULL
                          AND employmentStatusItemType.[Type] IN(1, 2) -- افزایش/بلااثر
                          AND info.[EmploymentType] IN(1, 2, 3, 6, 10, 11, 12, 13, 14, 15, 18, 22, 23)
                          AND info.IssuanceDate >= N'2021-03-21 00:00:00'
                          AND info.IssuanceDate <= N'2022-03-21 00:00:00' -- سال 1400
                    GROUP BY info.[OrganID];
        --*********************************************************************************************************2
        WITH PayrollEmployee AS 
		 (
		         SELECT p.Month, 
                        p.OrganID, 
                        pe.EmployeeInfoID, 
                        p.ID
                 FROM wag._Payroll p
                      INNER JOIN wag.PayrollEmployee pe ON p.ID = pe.PayrollID
                 WHERE p.LastState = 100
                       AND p.Year = 1400
                       AND pe.EmployeeInfoID IS NOT NULL
                       AND p.Month = 2
		  )
             INSERT INTO [Kama.Aro.Salary.Extention].rpt.HokmInPaknaNotSalary
                    SELECT info.[OrganID], 
                           CAST(2 AS TINYINT) Month, 
                           COUNT(*) [EmplyeeCount]
                    FROM [Kama.Aro.Pakna].[emp].[EmployeeInfoByRowNumber2] info
                         INNER JOIN [Kama.Aro.Pakna].[pbl].[EmploymentStatusItemType] employmentStatusItemType ON employmentStatusItemType.[Code] = info.EmploymentStatus
                         LEFT JOIN PayrollEmployee ON PayrollEmployee.EmployeeInfoID = info.ID
                    WHERE info.RowNumber = 1
                          AND PayrollEmployee.EmployeeInfoID IS NULL
                          AND employmentStatusItemType.[Type] IN(1, 2) -- افزایش/بلااثر
                          AND info.[EmploymentType] IN(1, 2, 3, 6, 10, 11, 12, 13, 14, 15, 18, 22, 23)
                          AND info.IssuanceDate >= N'2021-03-21 00:00:00'
                          AND info.IssuanceDate <= N'2022-03-21 00:00:00' -- سال 1400
                    GROUP BY info.[OrganID];
        --*********************************************************************************************************3
        WITH PayrollEmployee  AS 
		   (
		         SELECT p.Month, 
                        p.OrganID, 
                        pe.EmployeeInfoID, 
                        p.ID
                 FROM wag._Payroll p
                      INNER JOIN wag.PayrollEmployee pe ON p.ID = pe.PayrollID
                 WHERE p.LastState = 100
                       AND p.Year = 1400
                       AND pe.EmployeeInfoID IS NOT NULL
                       AND p.Month = 3
		    )
             INSERT INTO [Kama.Aro.Salary.Extention].rpt.HokmInPaknaNotSalary
                     SELECT info.[OrganID], 
                           CAST(3 AS TINYINT) Month, 
                           COUNT(*) [EmplyeeCount]
                    FROM [Kama.Aro.Pakna].[emp].[EmployeeInfoByRowNumber2] info
                         INNER JOIN [Kama.Aro.Pakna].[pbl].[EmploymentStatusItemType] employmentStatusItemType ON employmentStatusItemType.[Code] = info.EmploymentStatus
                         LEFT JOIN PayrollEmployee ON PayrollEmployee.EmployeeInfoID = info.ID
                    WHERE info.RowNumber = 1
                          AND PayrollEmployee.EmployeeInfoID IS NULL
                          AND employmentStatusItemType.[Type] IN(1, 2) -- افزایش/بلااثر
                          AND info.[EmploymentType] IN(1, 2, 3, 6, 10, 11, 12, 13, 14, 15, 18, 22, 23)
                          AND info.IssuanceDate >= N'2021-03-21 00:00:00'
                          AND info.IssuanceDate <= N'2022-03-21 00:00:00' -- سال 1400
                    GROUP BY info.[OrganID];

        --*********************************************************************************************************4
        WITH PayrollEmployee AS 
		    (
		         SELECT p.Month, 
                        p.OrganID, 
                        pe.EmployeeInfoID, 
                        p.ID
                 FROM wag._Payroll p
                      INNER JOIN wag.PayrollEmployee pe ON p.ID = pe.PayrollID
                 WHERE p.LastState = 100
                       AND p.Year = 1400
                       AND pe.EmployeeInfoID IS NOT NULL
                       AND p.Month = 4
			)
             INSERT INTO [Kama.Aro.Salary.Extention].rpt.HokmInPaknaNotSalary
                      SELECT info.[OrganID], 
                           CAST(4 AS TINYINT) Month, 
                           COUNT(*) [EmplyeeCount]
                    FROM [Kama.Aro.Pakna].[emp].[EmployeeInfoByRowNumber2] info
                         INNER JOIN [Kama.Aro.Pakna].[pbl].[EmploymentStatusItemType] employmentStatusItemType ON employmentStatusItemType.[Code] = info.EmploymentStatus
                         LEFT JOIN PayrollEmployee ON PayrollEmployee.EmployeeInfoID = info.ID
                    WHERE info.RowNumber = 1
                          AND PayrollEmployee.EmployeeInfoID IS NULL
                          AND employmentStatusItemType.[Type] IN(1, 2) -- افزایش/بلااثر
                          AND info.[EmploymentType] IN(1, 2, 3, 6, 10, 11, 12, 13, 14, 15, 18, 22, 23)
                          AND info.IssuanceDate >= N'2021-03-21 00:00:00'
                          AND info.IssuanceDate <= N'2022-03-21 00:00:00' -- سال 1400
                    GROUP BY info.[OrganID];
        --*********************************************************************************************************5
        WITH PayrollEmployee AS
           (
			     SELECT p.Month, 
                        p.OrganID, 
                        pe.EmployeeInfoID, 
                        p.ID
                 FROM wag._Payroll p
                      INNER JOIN wag.PayrollEmployee pe ON p.ID = pe.PayrollID
                 WHERE p.LastState = 100
                       AND p.Year = 1400
                       AND pe.EmployeeInfoID IS NOT NULL
                       AND p.Month = 5
			)
             INSERT INTO [Kama.Aro.Salary.Extention].rpt.HokmInPaknaNotSalary
                    SELECT info.[OrganID], 
                           CAST(5 AS TINYINT) Month, 
                           COUNT(*) [EmplyeeCount]
                    FROM [Kama.Aro.Pakna].[emp].[EmployeeInfoByRowNumber2] info
                         INNER JOIN [Kama.Aro.Pakna].[pbl].[EmploymentStatusItemType] employmentStatusItemType ON employmentStatusItemType.[Code] = info.EmploymentStatus
                         LEFT JOIN PayrollEmployee ON PayrollEmployee.EmployeeInfoID = info.ID
                    WHERE info.RowNumber = 1
                          AND PayrollEmployee.EmployeeInfoID IS NULL
                          AND employmentStatusItemType.[Type] IN(1, 2) -- افزایش/بلااثر
                          AND info.[EmploymentType] IN(1, 2, 3, 6, 10, 11, 12, 13, 14, 15, 18, 22, 23)
                          AND info.IssuanceDate >= N'2021-03-21 00:00:00'
                          AND info.IssuanceDate <= N'2022-03-21 00:00:00' -- سال 1400
                    GROUP BY info.[OrganID];
        --*********************************************************************************************************6
        WITH PayrollEmployee AS
             (
			     SELECT p.Month, 
                        p.OrganID, 
                        pe.EmployeeInfoID, 
                        p.ID
                 FROM wag._Payroll p
                      INNER JOIN wag.PayrollEmployee pe ON p.ID = pe.PayrollID
                 WHERE p.LastState = 100
                       AND p.Year = 1400
                       AND pe.EmployeeInfoID IS NOT NULL
                       AND p.Month = 6
			 )
             INSERT INTO [Kama.Aro.Salary.Extention].rpt.HokmInPaknaNotSalary
                    SELECT info.[OrganID], 
                           CAST(6 AS TINYINT) Month, 
                           COUNT(*) [EmplyeeCount]
                    FROM [Kama.Aro.Pakna].[emp].[EmployeeInfoByRowNumber2] info
                         INNER JOIN [Kama.Aro.Pakna].[pbl].[EmploymentStatusItemType] employmentStatusItemType ON employmentStatusItemType.[Code] = info.EmploymentStatus
                         LEFT JOIN PayrollEmployee ON PayrollEmployee.EmployeeInfoID = info.ID
                    WHERE info.RowNumber = 1
                          AND PayrollEmployee.EmployeeInfoID IS NULL
                          AND employmentStatusItemType.[Type] IN(1, 2) -- افزایش/بلااثر
                          AND info.[EmploymentType] IN(1, 2, 3, 6, 10, 11, 12, 13, 14, 15, 18, 22, 23)
                          AND info.IssuanceDate >= N'2021-03-21 00:00:00'
                          AND info.IssuanceDate <= N'2022-03-21 00:00:00' -- سال 1400
                    GROUP BY info.[OrganID];
        --*********************************************************************************************************7
         WITH PayrollEmployee AS
             (
			     SELECT p.Month, 
                        p.OrganID, 
                        pe.EmployeeInfoID, 
                        p.ID
                 FROM wag._Payroll p
                      INNER JOIN wag.PayrollEmployee pe ON p.ID = pe.PayrollID
                 WHERE p.LastState = 100
                       AND p.Year = 1400
                       AND pe.EmployeeInfoID IS NOT NULL
                       AND p.Month = 7
			 )
             INSERT INTO [Kama.Aro.Salary.Extention].rpt.HokmInPaknaNotSalary
                    SELECT info.[OrganID], 
                           CAST(7 AS TINYINT) Month, 
                           COUNT(*) [EmplyeeCount]
                    FROM [Kama.Aro.Pakna].[emp].[EmployeeInfoByRowNumber2] info
                         INNER JOIN [Kama.Aro.Pakna].[pbl].[EmploymentStatusItemType] employmentStatusItemType ON employmentStatusItemType.[Code] = info.EmploymentStatus
                         LEFT JOIN PayrollEmployee ON PayrollEmployee.EmployeeInfoID = info.ID
                    WHERE info.RowNumber = 1
                          AND PayrollEmployee.EmployeeInfoID IS NULL
                          --AND employmentStatusItemType.[CountType] = 2
                          AND employmentStatusItemType.[Type] IN(1, 2) -- افزایش/بلااثر
                         AND info.[EmploymentType] IN(1, 2, 3, 6, 10, 11, 12, 13, 14, 15, 18, 22, 23)
                    AND info.IssuanceDate >= N'2021-03-21 00:00:00'
                    AND info.IssuanceDate <= N'2022-03-21 00:00:00' -- سال 1400
                    GROUP BY info.[OrganID];
        --*****************************************************************************************************************************8
         WITH PayrollEmployee AS
             (
			     SELECT p.Month, 
                        p.OrganID, 
                        pe.EmployeeInfoID, 
                        p.ID
                 FROM wag._Payroll p
                      INNER JOIN wag.PayrollEmployee pe ON p.ID = pe.PayrollID
                 WHERE p.LastState = 100
                       AND p.Year = 1400
                       AND pe.EmployeeInfoID IS NOT NULL
                       AND p.Month = 8
			 )
             INSERT INTO [Kama.Aro.Salary.Extention].rpt.HokmInPaknaNotSalary
                    SELECT info.[OrganID], 
                           CAST(8 AS TINYINT) Month, 
                           COUNT(*) [EmplyeeCount]
                    FROM [Kama.Aro.Pakna].[emp].[EmployeeInfoByRowNumber2] info
                         INNER JOIN [Kama.Aro.Pakna].[pbl].[EmploymentStatusItemType] employmentStatusItemType ON employmentStatusItemType.[Code] = info.EmploymentStatus
                         LEFT JOIN PayrollEmployee ON PayrollEmployee.EmployeeInfoID = info.ID
                    WHERE info.RowNumber = 1
                          AND PayrollEmployee.EmployeeInfoID IS NULL
                          AND employmentStatusItemType.[Type] IN(1, 2) -- افزایش/بلااثر
                          AND info.[EmploymentType] IN(1, 2, 3, 6, 10, 11, 12, 13, 14, 15, 18, 22, 23)
                          AND info.IssuanceDate >= N'2021-03-21 00:00:00'
                          AND info.IssuanceDate <= N'2022-03-21 00:00:00' -- سال 1400
                    GROUP BY info.[OrganID];
        --**************************************************************************************************************************9
         WITH PayrollEmployee AS
             (
			     SELECT p.Month, 
                        p.OrganID, 
                        pe.EmployeeInfoID, 
                        p.ID
                 FROM wag._Payroll p
                      INNER JOIN wag.PayrollEmployee pe ON p.ID = pe.PayrollID
                 WHERE p.LastState = 100
                       AND p.Year = 1400
                       AND pe.EmployeeInfoID IS NOT NULL
                       AND p.Month = 9
			 )
             INSERT INTO [Kama.Aro.Salary.Extention].rpt.HokmInPaknaNotSalary
                    SELECT info.[OrganID], 
                           CAST(9 AS TINYINT) Month, 
                           COUNT(*) [EmplyeeCount]
                    FROM [Kama.Aro.Pakna].[emp].[EmployeeInfoByRowNumber2] info
                         INNER JOIN [Kama.Aro.Pakna].[pbl].[EmploymentStatusItemType] employmentStatusItemType ON employmentStatusItemType.[Code] = info.EmploymentStatus
                         LEFT JOIN PayrollEmployee ON PayrollEmployee.EmployeeInfoID = info.ID
                    WHERE info.RowNumber = 1
                          AND PayrollEmployee.EmployeeInfoID IS NULL
                          AND employmentStatusItemType.[Type] IN(1, 2) -- افزایش/بلااثر
                          AND info.[EmploymentType] IN(1, 2, 3, 6, 10, 11, 12, 13, 14, 15, 18, 22, 23)
                          AND info.IssuanceDate >= N'2021-03-21 00:00:00'
                          AND info.IssuanceDate <= N'2022-03-21 00:00:00' -- سال 1400
                    GROUP BY info.[OrganID];

        --**********************************************************************************************************************10
        WITH PayrollEmployee AS
             (
			     SELECT p.Month, 
                        p.OrganID, 
                        pe.EmployeeInfoID, 
                        p.ID
                 FROM wag._Payroll p
                      INNER JOIN wag.PayrollEmployee pe ON p.ID = pe.PayrollID
                 WHERE p.LastState = 100
                       AND p.Year = 1400
                       AND pe.EmployeeInfoID IS NOT NULL
                       AND p.Month = 10
			 )
             INSERT INTO [Kama.Aro.Salary.Extention].rpt.HokmInPaknaNotSalary
                    SELECT info.[OrganID], 
                           CAST(10 AS TINYINT) Month, 
                           COUNT(*) [EmplyeeCount]
                    FROM [Kama.Aro.Pakna].[emp].[EmployeeInfoByRowNumber2] info
                         INNER JOIN [Kama.Aro.Pakna].[pbl].[EmploymentStatusItemType] employmentStatusItemType ON employmentStatusItemType.[Code] = info.EmploymentStatus
                         LEFT JOIN PayrollEmployee ON PayrollEmployee.EmployeeInfoID = info.ID
                    WHERE info.RowNumber = 1
                          AND PayrollEmployee.EmployeeInfoID IS NULL
                          AND employmentStatusItemType.[Type] IN(1, 2) -- افزایش/بلااثر
                          AND info.[EmploymentType] IN(1, 2, 3, 6, 10, 11, 12, 13, 14, 15, 18, 22, 23)
                          AND info.IssuanceDate >= N'2021-03-21 00:00:00'
                          AND info.IssuanceDate <= N'2022-03-21 00:00:00' -- سال 1400
                    GROUP BY info.[OrganID];
        --***************************************************************************************************************************11
         WITH PayrollEmployee AS
             (
			     SELECT p.Month, 
                        p.OrganID, 
                        pe.EmployeeInfoID, 
                        p.ID
                 FROM wag._Payroll p
                      INNER JOIN wag.PayrollEmployee pe ON p.ID = pe.PayrollID
                 WHERE p.LastState = 100
                       AND p.Year = 1400
                       AND pe.EmployeeInfoID IS NOT NULL
                       AND p.Month = 11
			 )
             INSERT INTO [Kama.Aro.Salary.Extention].rpt.HokmInPaknaNotSalary
                    SELECT info.[OrganID], 
                           CAST(11 AS TINYINT) Month, 
                           COUNT(*) [EmplyeeCount]
                    FROM [Kama.Aro.Pakna].[emp].[EmployeeInfoByRowNumber2] info
                         INNER JOIN [Kama.Aro.Pakna].[pbl].[EmploymentStatusItemType] employmentStatusItemType ON employmentStatusItemType.[Code] = info.EmploymentStatus
                         LEFT JOIN PayrollEmployee ON PayrollEmployee.EmployeeInfoID = info.ID
                    WHERE info.RowNumber = 1
                          AND PayrollEmployee.EmployeeInfoID IS NULL
                          AND employmentStatusItemType.[Type] IN(1, 2) -- افزایش/بلااثر
                          AND info.[EmploymentType] IN(1, 2, 3, 6, 10, 11, 12, 13, 14, 15, 18, 22, 23)
                          AND info.IssuanceDate >= N'2021-03-21 00:00:00'
                          AND info.IssuanceDate <= N'2022-03-21 00:00:00' -- سال 1400
                    GROUP BY info.[OrganID];
        --************************************************************************************************************************************12
        WITH PayrollEmployee AS
             (
			     SELECT p.Month, 
                        p.OrganID, 
                        pe.EmployeeInfoID, 
                        p.ID
                 FROM wag._Payroll p
                      INNER JOIN wag.PayrollEmployee pe ON p.ID = pe.PayrollID
                 WHERE p.LastState = 100
                       AND p.Year = 1400
                       AND pe.EmployeeInfoID IS NOT NULL
                       AND p.Month = 12
			 )
             INSERT INTO [Kama.Aro.Salary.Extention].rpt.HokmInPaknaNotSalary
                    SELECT info.[OrganID], 
                           CAST(12 AS TINYINT) Month, 
                           COUNT(*) [EmplyeeCount]
                    FROM [Kama.Aro.Pakna].[emp].[EmployeeInfoByRowNumber2] info
                         INNER JOIN [Kama.Aro.Pakna].[pbl].[EmploymentStatusItemType] employmentStatusItemType ON employmentStatusItemType.[Code] = info.EmploymentStatus
                         LEFT JOIN PayrollEmployee ON PayrollEmployee.EmployeeInfoID = info.ID
                    WHERE info.RowNumber = 1
                          AND PayrollEmployee.EmployeeInfoID IS NULL
                          AND employmentStatusItemType.[Type] IN(1, 2) -- افزایش/بلااثر
                          AND info.[EmploymentType] IN(1, 2, 3, 6, 10, 11, 12, 13, 14, 15, 18, 22, 23)
                          AND info.IssuanceDate >= N'2021-03-21 00:00:00'
                          AND info.IssuanceDate <= N'2022-03-21 00:00:00' -- سال 1400
                    GROUP BY info.[OrganID];
    END;