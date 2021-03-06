
SELECT 
	 RE_UID
	,RE_Link
	,REPLACE(SUBSTRING(RE_Link, 1, -1 + CHARINDEX('&', RE_Link)), '{@report}', '') AS RDL 
	,FT_De
	,FT_Status
FROM T_FMS_Reports

LEFT JOIN T_FMS_Translation
	ON FT_UID = RE_FT_UID 
	
WHERE RE_Status = 1 
AND RE_Link IS NOT NULL 

ORDER BY RDL  
