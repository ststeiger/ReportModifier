
USE [ReportServer] 

INSERT INTO Users (UserID, Sid, UserType, AuthType, UserName) 
SELECT 
	 NEWID() AS UserID -- uniqueidentifier 
	,NULL AS [Sid] -- varbinary(85) 
	,1 AS UserType -- int 
	,3 AS AuthType -- int 
	,dblist.name  AS UserName -- nvarchar(260) 
FROM 
(
	SELECT '' AS name 
	
	UNION SELECT 'SwissRe' AS name 
	UNION SELECT 'COR_Basic' AS name 
	UNION SELECT 'Aperture_RTRChur' AS name 
	UNION SELECT 'COR_Vertragsverwaltung' AS name 
	UNION SELECT 'COR_DMS' AS name 
	UNION SELECT 'SwissRe_EPI' AS name 
	UNION SELECT 'COR_Basic_Portal' AS name 
	UNION SELECT 'Aperture_RSI' AS name 
	UNION SELECT 'Basicv2' AS name 
	
) AS dblist 

WHERE dblist.name != '' 
AND (0 = (SELECT COUNT(*) FROM Users WHERE Users.UserName = dblist.name COLLATE Latin1_General_CI_AS) ) 
