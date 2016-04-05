
SELECT 
	 C.path 
	,C.Name 
	,U.UserName 
	,R.RoleName 
	,R.Description 
	,U.AuthType 
FROM Reportserver.dbo.Users AS U 

INNER JOIN Reportserver.dbo.PolicyUserRole AS PUR 
	ON U.UserID = PUR.UserID 
	
INNER JOIN Reportserver.dbo.Policies AS P 
	ON P.PolicyID = PUR.PolicyID 
	
INNER JOIN Reportserver.dbo.Roles AS R 
	ON R.RoleID = PUR.RoleID 
	
INNER JOIN Reportserver.dbo.Catalog AS c 
	ON C.PolicyID = P.PolicyID 
	
--WHERE c.Name = @ReportName 
ORDER BY U.UserName 
