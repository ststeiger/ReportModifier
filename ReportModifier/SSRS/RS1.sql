/****** Skript für SelectTopNRows-Befehl aus SSMS  ******/
SELECT TOP 1000 [SecDataID]
      ,[PolicyID]
      ,[AuthType]
      ,[XmlDescription]
      ,[NtSecDescPrimary]
      ,[NtSecDescSecondary]
  FROM [ReportServer].[dbo].[SecData]
  
  <Policies>
  <Policy>
	<GroupUserName>VORDEFINIERT\Administratoren</GroupUserName>
	<GroupUserId>AQIAAAAAAAUgAAAAIAIAAA==</GroupUserId>
		<Roles>
			<Role><Name>Inhalts-Manager</Name></Role>
		</Roles>
  </Policy>
  <Policy>
	<GroupUserName>COR\COR_DB
  </GroupUserName>
  
  <GroupUserId>AQUAAAAAAAUVAAAAFVAYYzi5/VhBuNx5mQQAAA==</GroupUserId>
	  <Roles>
		  <Role><Name>Berichts-Generator</Name></Role>
		  <Role><Name>Browser</Name></Role>
		  <Role><Name>Inhalts-Manager</Name></Role>
		  <Role><Name>Meine Berichte</Name></Role>
		  <Role><Name>Verleger</Name></Role>
	  </Roles>
  </Policy>
  </Policies>
  