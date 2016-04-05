--exec GetSystemPolicy @AuthType=1

DECLARE @AuthType int
SET @AuthType = 1 

SELECT 
	 SecData.NtSecDescPrimary 
	,SecData.XmlDescription 
FROM Policies 
LEFT OUTER JOIN SecData 
	ON Policies.PolicyID = SecData.PolicyID 
	
AND AuthType = @AuthType 

WHERE PolicyFlag = 1


GO




<Policies>
<Policy>
<GroupUserName>VORDEFINIERT\Administratoren</GroupUserName>
<GroupUserId>AQIAAAAAAAUgAAAAIAIAAA==</GroupUserId><Roles><Role>
<Name>Systemadministrator</Name></Role>
<Role><Name>Systembenutzer</Name></Role>
</Roles>
</Policy>


<Policy><GroupUserName>COR\COR_DB</GroupUserName>
<GroupUserId>AQUAAAAAAAUVAAAAFVAYYzi5/VhBuNx5mQQAAA==</GroupUserId>
<Roles><Role><Name>Systembenutzer</Name></Role></Roles>
</Policy>
</Policies>