
USE ReportServer


SELECT 
	 PolicyUserRole.ID 
	--,PolicyUserRole.RoleID 
	--,PolicyUserRole.UserID 
	--,PolicyUserRole.PolicyID 
	
	,Users.UserID 
	--,Users.Sid 
	,Users.UserType 
	--,Users.AuthType 
	,Users.UserName 
	 
	,Roles.RoleID 
	,Roles.RoleName 
	,Roles.Description 
	,Roles.TaskMask 
	,Roles.RoleFlags 
	 
	,Policies.PolicyID 
	,Policies.PolicyFlag 
FROM PolicyUserRole 

LEFT JOIN Users 
	ON Users.UserID = PolicyUserRole.UserID 
	
LEFT JOIN Roles 
	ON Roles.RoleID = PolicyUserRole.RoleID 
	
LEFT JOIN Policies 
	ON Policies.PolicyID = PolicyUserRole.PolicyID
	



SELECT 
	 MachineName 
	,InstallationID 
	,InstanceName 
	,Client 
	,PublicKey 
	,SymmetricKey 
FROM Keys 




USE [UserAccounts]


SELECT 
	 [UserName] 
	,[PasswordHash] 
	,[salt] 
FROM [Users] 


SELECT 
	 [LOG_UID] 
	,[LOG_URL] 
	,[LOG_Text] 
	,[LOG_DateTime] 
FROM [T_Log] 
