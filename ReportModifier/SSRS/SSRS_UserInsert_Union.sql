
SELECT 'UNION SELECT ''SwissRe'' AS name ' AS cmd 

UNION ALL 

SELECT 'UNION SELECT ''' + REPLACE(name, '''', '''''') + ''' AS name ' AS cmd 
FROM sys.databases 
WHERE (1=1) 
AND owner_sid != 0x01 
AND name NOT LIKE 'ReportServer%' 
AND name NOT LIKE 'xxx[_]%' 
