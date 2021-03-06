USE [ReportServer]


SELECT 
	 [PolicyID]
	,[PolicyFlag]
FROM [Policies]

SELECT 
	 [ID]
	,[RoleID]
	,[UserID]
	,[PolicyID]
FROM [PolicyUserRole]


SELECT 
	 [RoleID]
	,[RoleName]
	,[Description]
	,[TaskMask]
	,[RoleFlags]
FROM [Roles]

SELECT 
	 [UserID]
	,[Sid]
	,[UserType]
	,[AuthType]
	,[UserName]
FROM [Users]
