
USE DefaultDb
USE [DefaultDb]
GO



CREATE TABLE [dbo].[T_in_mandant_Parameter](
	[Report] [nvarchar](300) NULL,
	[ParaValue] [nvarchar](300) NULL
) ON [PRIMARY]

GO



SELECT 
	 Report
	,ParaValue
FROM T_StichtagParameter
WHERE ParaValue = '=Now'





USE DefaultDb


SELECT 
	 ParaValue 
	,COUNT(*) AS cnt 
FROM T_StichtagParameter 
GROUP BY ParaValue 

ORDER BY cnt DESC 



SELECT DISTINCT ParaValue, COUNT(*) AS cnt FROM T_StichtagParameter GROUP BY ParaValue ORDER BY cnt DESC 
SELECT DISTINCT ParaValue, COUNT(*) AS cnt FROM T_ProcParameter GROUP BY ParaValue ORDER BY cnt DESC 
SELECT DISTINCT ParaValue, COUNT(*) AS cnt FROM T_in_mandant_Parameter GROUP BY ParaValue ORDER BY cnt DESC 

-- =Day(Today) & "." & Month(Today) & "." & Year(Today)
-- =FormatDateTime(DateSerial(Year(Today),Month(Today),Day(Today)),2)
-- =Now
-- =System.DateTime.Now.ToString("dd.MM.yyyy")
-- =System.DateTime.Today.ToString("dd.MM.yyyy")
-- Kein Parameter Stichtag



USE [DefaultDb]


SELECT 
	 [Report]
	,[ParaValue]
FROM [T_ProcParameter]
WHERE ParaValue = 'Kein Parameter proc'
AND Report NOT LIKE 'xx%'
AND Report NOT LIKE 'zz%'
AND Report NOT LIKE '%kopie%'
AND Report NOT LIKE '%Helsana%'
