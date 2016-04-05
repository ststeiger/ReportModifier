
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[_MyList]') AND type in (N'U'))
EXECUTE('
CREATE TABLE dbo._MyList 
( 
	 MyUID uniqueidentifier NOT NULL 
	,ReportPath nvarchar(250) NULL 
	,ReportName nvarchar(250) NULL 
	,Name nvarchar(250) NULL 
	,Type nvarchar(250) NULL 
	,Nullable nvarchar(250) NULL 
	,AllowBlank nvarchar(250) NULL 
	,MultiValue nvarchar(250) NULL 
	,UsedInQuery nvarchar(250) NULL 
	,Prompt nvarchar(250) NULL 
	,DynamicPrompt nvarchar(250) NULL 
	,PromptUser nvarchar(250) NULL 
	,State nvarchar(250) NULL 
) ON [PRIMARY] 
')


INSERT INTO ReportServer.dbo._MyList
(
	 MyUID
	,ReportPath
	,ReportName
	,Name
	,Type
	,Nullable
	,AllowBlank
	,MultiValue
	,UsedInQuery
	,Prompt
	,DynamicPrompt
	,PromptUser
	,State
)
SELECT 
	 NEWID() AS MyUID 
	,tReportServerCatalog.Path AS ReportPath 
	,tReportServerCatalog.Name AS ReportName 
	,tXmlToTable.Paravalue.value('Name[1]', 'nvarchar(250)') AS Name 
	,Paravalue.value('Type[1]', 'nvarchar(250)') AS Type 
	,Paravalue.value('Nullable[1]', 'nvarchar(250)') AS Nullable 
	,Paravalue.value('AllowBlank[1]', 'nvarchar(250)') AS AllowBlank 
	,Paravalue.value('MultiValue[1]', 'nvarchar(250)') AS MultiValue 
	,Paravalue.value('UsedInQuery[1]', 'nvarchar(250)') AS UsedInQuery 
	,Paravalue.value('Prompt[1]', 'nvarchar(250)') AS Prompt 
	,Paravalue.value('DynamicPrompt[1]', 'nvarchar(250)') AS DynamicPrompt 
	,Paravalue.value('PromptUser[1]', 'nvarchar(250)') AS PromptUser 
	,Paravalue.value('State[1]', 'nvarchar(250)') AS State 
FROM 
( 
	SELECT 
		 C.Name 
		,C.Path 
		,CONVERT(XML,C.Parameter) AS ParameterXML 
	FROM ReportServer.dbo.Catalog AS C 
	WHERE C.Content IS NOT NULL 
	AND C.Type = 2 
	--AND C.Name  =  @ReportName 
	--AND Name = 'AL_Anlageblatt' 
) AS tReportServerCatalog 
CROSS APPLY ParameterXML.nodes('//Parameters/Parameter') AS tXmlToTable ( Paravalue ) 
