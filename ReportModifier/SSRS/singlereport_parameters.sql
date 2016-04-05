
DECLARE @XML AS xml 
SET @XML = 
(
	SELECT TOP 1 
		CONVERT(XML, C.Parameter) AS ParameterXML 
	FROM ReportServer.dbo.Catalog AS C 
	WHERE C.Content IS NOT NULL 
	AND C.Type = 2 
	--AND C.Name = @ReportName 
	AND Name = 'AL_Anlageblatt' 
) 

;WITH CTE AS
(
	SELECT 
		 T.N.value('(Name/text())[1]', 'nvarchar(500)') AS Name 
		--,T.N.value('(Type/text())[1]', 'nvarchar(500)') AS Type 
		--,T.N.value('(Nullable/text())[1]', 'nvarchar(500)') AS Nullable 
		--,T.N.value('(AllowBlank/text())[1]', 'nvarchar(500)') AS AllowBlank 
		--,T.N.value('(MultiValue/text())[1]', 'nvarchar(500)') AS MultiValue 
		--,T.N.value('(UsedInQuery/text())[1]', 'nvarchar(500)') AS UsedInQuery 
		--,T.N.value('(Prompt/text())[1]', 'nvarchar(500)') AS Prompt 
		--,T.N.value('(DynamicPrompt/text())[1]', 'nvarchar(500)') AS DynamicPrompt 
		--,T.N.value('(PromptUser/text())[1]', 'nvarchar(500)') AS PromptUser 
		--,T.N.value('(State/text())[1]', 'nvarchar(500)') AS State 
		
		--@XML.value('(Statement/Id/text())[1]', 'uniqueidentifier') as StatementId, 
		--       T.N.value('(Date/text())[1]', 'smalldatetime') as InvoiceDate, 
		--       T.N.value('(AmountDue/text())[1]', 'decimal') as AmountDue 
	FROM @XML.nodes('//Parameters/Parameter') AS T(N) 
)
SELECT * FROM CTE 
WHERE Name IN ('in_standort', 'in_gebaeude') 
