
USE [ReportServer]
GO



--------exec FindObjectsNonRecursive @Path=N'',@AuthType=1
--------exec ObjectExists @Path=N'',@AuthType=1
--------exec GetAllProperties @Path=N'',@AuthType=1

--exec GetPolicy @ItemName=N'',@AuthType=1

DECLARE @AuthType int
DECLARE @ItemName as nvarchar(425)

SET @AuthType = 1 
SET @ItemName=N'' 




SELECT 
	  Catalog.ItemID 
	 ,Catalog.Path 
	 ,Catalog.Name
	,SecData.AuthType
	 
	,SecData.XmlDescription
	,Catalog.PolicyRoot 
	,SecData.NtSecDescPrimary
	,Catalog.Type
FROM Catalog 

INNER JOIN Policies 
	ON Catalog.PolicyID = Policies.PolicyID 
	
LEFT JOIN SecData 
	ON Policies.PolicyID = SecData.PolicyID 
	AND AuthType = @AuthType

WHERE (1=1) 
AND Catalog.Path = @ItemName 
AND PolicyFlag = 0 


GO

<Policies><Policy><GroupUserName>VORDEFINIERT\Administratoren</GroupUserName>
<GroupUserId>AQIAAAAAAAUgAAAAIAIAAA==</GroupUserId><Roles><Role><Name>Inhalts-Manager</Name></Role>
</Roles></Policy>

<Policy><GroupUserName>COR\COR_DB</GroupUserName>
<GroupUserId>AQUAAAAAAAUVAAAAFVAYYzi5/VhBuNx5mQQAAA==</GroupUserId>
<Roles>
<Role><Name>Berichts-Generator</Name></Role>
<Role><Name>Browser</Name></Role><Role><Name>Inhalts-Manager</Name></Role>
<Role>
<Name>Meine Berichte</Name></Role><Role><Name>Verleger</Name></Role></Roles>
</Policy>
</Policies>
