
using System;
using System.Collections.Generic;
using System.Text;


namespace ReportModifier
{


    class ReportServerTools
    {


        public static string GetSavePath()
        {
            string strSavePath = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            strSavePath = System.IO.Path.Combine(strSavePath, "AutoModifiedReports");


            if (!System.IO.Directory.Exists(strSavePath))
                System.IO.Directory.CreateDirectory(strSavePath);

            return strSavePath;
        } // End Function GetSavePath


        public static System.Xml.XmlNamespaceManager GetReportNamespaceManager(System.Xml.XmlDocument doc)
        {
            if (doc == null)
                throw new ArgumentNullException("doc");

            System.Xml.XmlNamespaceManager nsmgr = new System.Xml.XmlNamespaceManager(doc.NameTable);

            // <Report xmlns="http://schemas.microsoft.com/sqlserver/reporting/2008/01/reportdefinition" xmlns:rd="http://schemas.microsoft.com/SQLServer/reporting/reportdesigner">

            if (doc.DocumentElement != null)
            {
                // string strNamespace = doc.DocumentElement.NamespaceURI;
                // System.Console.WriteLine(strNamespace);
                // nsmgr.AddNamespace("dft", strNamespace);

                System.Xml.XPath.XPathNavigator xNav = doc.CreateNavigator();
                while (xNav.MoveToFollowing(System.Xml.XPath.XPathNodeType.Element))
                {
                    IDictionary<string, string> localNamespaces = xNav.GetNamespacesInScope(System.Xml.XmlNamespaceScope.Local);

                    foreach (System.Collections.Generic.KeyValuePair<string, string> kvp in localNamespaces)
                    {
                        string prefix = kvp.Key;
                        if (string.IsNullOrEmpty(prefix))
                            prefix = "dft";

                        nsmgr.AddNamespace(prefix, kvp.Value);
                    } // Next kvp

                } // Whend



                //System.Xml.XmlNodeList _xmlNameSpaceList = doc.SelectNodes(@"//namespace::*[not(. = ../../namespace::*)]");

                //foreach (System.Xml.XmlNode currentNamespace in _xmlNameSpaceList)
                //{
                //    if (StringComparer.InvariantCultureIgnoreCase.Equals(currentNamespace.LocalName, "xmlns"))
                //    {
                //        nsmgr.AddNamespace("dft", currentNamespace.Value);
                //    }
                //    else
                //        nsmgr.AddNamespace(currentNamespace.LocalName, currentNamespace.Value);

                //}

                return nsmgr;
            } // End if (doc.DocumentElement != null)

            nsmgr.AddNamespace("dft", "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition");
            // nsmgr.AddNamespace("dft", "http://schemas.microsoft.com/sqlserver/reporting/2008/01/reportdefinition");

            return nsmgr;
        } // End Function GetReportNamespaceManager


        public static bool HasDataSet(string strFilename, string dataSetName)
        {
            System.Xml.XmlDocument doc = XmlTools.File2XmlDocument(strFilename);
            System.Xml.XmlNamespaceManager nsmgr = GetReportNamespaceManager(doc);

            return HasDataSet(doc, dataSetName);
        } // End Function HasDataSet


        public static bool HasDataSet(System.Xml.XmlDocument doc, string dataSetName)
        {
            dataSetName = XmlTools.XmlEscape(dataSetName);

            System.Xml.XmlNamespaceManager nsmgr = GetReportNamespaceManager(doc);
            System.Xml.XmlNode xnProc = doc.SelectSingleNode("/dft:Report/dft:DataSets/dft:DataSet[@Name=\"" + dataSetName + "\"]", nsmgr);

            return xnProc != null;
        } // End Function HasDataSet


        public static string GetDataSetDefinition(System.Xml.XmlDocument doc, string dataSetName)
        {
            dataSetName = XmlTools.XmlEscape(dataSetName);

            if (HasDataSet(doc, dataSetName))
            {
                System.Xml.XmlNamespaceManager nsmgr = GetReportNamespaceManager(doc);
                System.Xml.XmlNode xnSQL = doc.SelectSingleNode("/dft:Report/dft:DataSets/dft:DataSet[@Name=\"" + dataSetName + "\"]/dft:Query/dft:CommandText", nsmgr);
                return xnSQL.InnerText;
            }

            return null;
        } // End Function GetDataSetDefinition


        public static void AddMainParametersNode(string strFilename)
        {
            if (HasParameters(strFilename))
                return;

            System.Xml.XmlNode xnDataSets = GetDataSetsNode(strFilename);
            if (xnDataSets == null)
                return;

            System.Xml.XmlDocument doc = xnDataSets.OwnerDocument;
            System.Xml.XmlDocumentFragment xmlDocFrag = doc.CreateDocumentFragment();
            xmlDocFrag.InnerXml = "<ReportParameters></ReportParameters>";

            xnDataSets.ParentNode.InsertAfter(xmlDocFrag, xnDataSets);
            XmlTools.SaveDocument(doc, strFilename, true);
        }


        public static System.Xml.XmlNode GetDataSetsNode(string strFilename)
        {
            System.Xml.XmlDocument doc = XmlTools.File2XmlDocument(strFilename);
            return GetDataSetsNode(doc);
        }


        public static System.Xml.XmlNode GetDataSetsNode(System.Xml.XmlDocument doc)
        {
            System.Xml.XmlNamespaceManager nsmgr = GetReportNamespaceManager(doc);
            System.Xml.XmlNode xnParam = doc.SelectSingleNode("/dft:Report/dft:DataSets", nsmgr);

            return xnParam;
        } // End Function GetParameter


        public static bool HasParameters(string strFilename)
        {
            System.Xml.XmlDocument doc = XmlTools.File2XmlDocument(strFilename);
            System.Xml.XmlNamespaceManager nsmgr = GetReportNamespaceManager(doc);

            return HasParameters(doc);
        } // End Function HasParameter


        public static bool HasParameters(System.Xml.XmlDocument doc)
        {
            System.Xml.XmlNamespaceManager nsmgr = GetReportNamespaceManager(doc);
            System.Xml.XmlNode xnProc = doc.SelectSingleNode("/dft:Report/dft:ReportParameters", nsmgr);

            return xnProc != null;
        } // End Function HasParameter


        public static bool HasParameter(string strFilename, string strParameterName)
        {
            System.Xml.XmlDocument doc = XmlTools.File2XmlDocument(strFilename);
            System.Xml.XmlNamespaceManager nsmgr = GetReportNamespaceManager(doc);

            return HasParameter(doc, strParameterName);
        } // End Function HasParameter


        public static bool HasParameter(System.Xml.XmlDocument doc, string strParameterName)
        {
            strParameterName = XmlTools.XmlEscape(strParameterName);

            System.Xml.XmlNamespaceManager nsmgr = GetReportNamespaceManager(doc);
            System.Xml.XmlNode xnProc = doc.SelectSingleNode("/dft:Report/dft:ReportParameters/dft:ReportParameter[@Name=\"" + strParameterName + "\"]", nsmgr);

            return xnProc != null;
        } // End Function HasParameter
        
        
        public static System.Xml.XmlNode GetParameter(string fileName, string strParameterName)
        {
            System.Xml.XmlDocument doc = XmlTools.File2XmlDocument(fileName);
            System.Xml.XmlNamespaceManager nsmgr = GetReportNamespaceManager(doc);

            return GetParameter(doc, strParameterName);
        } // End Function GetParameter


        public static System.Xml.XmlNode GetParameter(System.Xml.XmlDocument doc, string strParameterName)
        {
            strParameterName = XmlTools.XmlEscape(strParameterName);

            System.Xml.XmlNamespaceManager nsmgr = GetReportNamespaceManager(doc);
            System.Xml.XmlNode xnParam = doc.SelectSingleNode("/dft:Report/dft:ReportParameters/dft:ReportParameter[@Name=\"" + strParameterName + "\"]", nsmgr);

            return xnParam;
        } // End Function GetParameter


        public static string GetParameterDefaultValue(string fileName, string strParameterName)
        {
            System.Xml.XmlDocument doc = XmlTools.File2XmlDocument(fileName);
            System.Xml.XmlNamespaceManager nsmgr = GetReportNamespaceManager(doc);

            return GetParameterDefaultValue(doc, strParameterName);
        } // End Function GetParameterDefaultValue


        public static string GetParameterDefaultValue(System.Xml.XmlDocument doc, string strParameterName)
        {
            strParameterName = XmlTools.XmlEscape(strParameterName);

            System.Xml.XmlNamespaceManager nsmgr = GetReportNamespaceManager(doc);
            System.Xml.XmlNode xnParam = doc.SelectSingleNode("/dft:Report/dft:ReportParameters/dft:ReportParameter[@Name=\"" + strParameterName + "\"]/dft:DefaultValue/dft:Values/dft:Value", nsmgr);

            if (xnParam != null)
                return xnParam.InnerText;

            return null;
        } // End Function GetParameterDefaultValue
        

        public static System.Xml.XmlNode GetParameterPrompt(System.Xml.XmlDocument doc, string strParameterName)
        {
            strParameterName = XmlTools.XmlEscape(strParameterName);

            System.Xml.XmlNamespaceManager nsmgr = GetReportNamespaceManager(doc);
            // doc.SelectSingleNode("/dft:Report/dft:ReportParameters/dft:ReportParameter[@Name=\"in_standort\"]/dft:Prompt", nsmgr);
            string str = "/dft:Report/dft:ReportParameters/dft:ReportParameter[@Name=\"" + strParameterName + "\"]/dft:Prompt";
            System.Xml.XmlNode xnParam = doc.SelectSingleNode(str, nsmgr);

            return xnParam;
        } // End Function GetParameterPrompt
        

        // ChangeParameterPrompt(strFileName, "in_standortkategorie", "Liegenschaftskategorie / Catégorie d'immeuble / Categoria di patrimonio immobiliare / Estate category");
        // ChangeParameterPrompt(strFileName, "in_standortkategorie", "Standortkategorie / Catégorie site / Categoria sito / Site category");
        // ChangeParameterPrompt(strFileName, "in_standort", "Standort / Site / Sito / Site");
        // ChangeParameterPrompt(strFileName, "in_standort", "Liegenschaft / Immeuble / Patrimonio immobiliare / Estate");
        // ChangeParameterPrompt(strFileName, "in_gebaeude", "Gebäude / Bâtiment / Edificio / Building");
        public static void ChangeParameterPrompt(string strFilename, string strReportParameterName, string strReplacementText)
        {
            System.Xml.XmlDocument doc = XmlTools.File2XmlDocument(strFilename);
            System.Xml.XmlNamespaceManager nsmgr = GetReportNamespaceManager(doc);

            if (!HasParameter(doc, strReportParameterName))
                return;


            System.Xml.XmlNode xnParameterPrompt = GetParameterPrompt(doc, strReportParameterName);
            string strReportName = System.IO.Path.GetFileNameWithoutExtension(strFilename);

            if (xnParameterPrompt != null)
            {
                string strParameterValue = xnParameterPrompt.FirstChild.Value;
                xnParameterPrompt.FirstChild.Value = strReplacementText;
                Logging.LogMessage("Old value in {0}:\t{1}", strReportName, strParameterValue);
            } // End if (xnParameterPrompt != null)
            else
                Logging.LogMessage("{0}\tKein Parameter " + strReportParameterName, strReportName);


            XmlTools.SaveDocument(doc, strFilename);
        } // End Sub ChangeParameterPrompt


        public static void ChangeStichtag(string strFilename)
        {
            System.Xml.XmlDocument doc = XmlTools.File2XmlDocument(strFilename);
            System.Xml.XmlNamespaceManager nsmgr = GetReportNamespaceManager(doc);

            System.Xml.XmlNode xnStichtag = doc.SelectSingleNode("/dft:Report/dft:ReportParameters/dft:ReportParameter[@Name=\"in_stichtag\"]/dft:DefaultValue/dft:Values/dft:Value", nsmgr);
            string strReportName = System.IO.Path.GetFileNameWithoutExtension(strFilename);

            if (!HasParameter(doc, "in_stichtag"))
                return;


            if (xnStichtag != null)
            {
                xnStichtag.FirstChild.Value = "=System.DateTime.Now.ToString(\"dd.MM.yyyy\")";
                string strStichTag = xnStichtag.FirstChild.Value;
                Logging.LogMessage("{0}\t{1}", strReportName, strStichTag);
                // =System.DateTime.Today.ToString("dd.MM.yyyy")
            } // End if (xnStichtag != null)
            else
                Logging.LogMessage("{0}\tKein Parameter Stichtag", strReportName);
            // =System.DateTime.Today.ToString("dd.MM.yyyy")
            // stichtagvalue.FirstChild.Value = "=System.DateTime.Today.ToString(\"dd.MMMM.yyyy\")";

            XmlTools.SaveDocument(doc, strFilename);
        } // End Sub ChangeStichtag


        public static void PrintStichtag(string strFilename)
        {
            System.Xml.XmlDocument doc = XmlTools.File2XmlDocument(strFilename);
            System.Xml.XmlNamespaceManager nsmgr = GetReportNamespaceManager(doc);

            System.Xml.XmlNode xnStichtag = doc.SelectSingleNode("/dft:Report/dft:ReportParameters/dft:ReportParameter[@Name=\"in_stichtag\"]/dft:DefaultValue/dft:Values/dft:Value", nsmgr);

            string strReportName = System.IO.Path.GetFileNameWithoutExtension(strFilename);

            if (xnStichtag != null)
            {
                Logging.LogMessage("{0}\t{1}", strReportName, xnStichtag.FirstChild.Value);
            }
            else
                Logging.LogMessage("{0}\tKein Parameter Stichtag", strReportName);
        } // End Sub PrintStichtag


        public static void PrintProc(string strFilename)
        {
            System.Xml.XmlDocument doc = XmlTools.File2XmlDocument(strFilename);
            System.Xml.XmlNamespaceManager nsmgr = GetReportNamespaceManager(doc);

            System.Xml.XmlNode xnProc = doc.SelectSingleNode("/dft:Report/dft:ReportParameters/dft:ReportParameter[@Name=\"proc\"]/dft:DefaultValue/dft:Values/dft:Value", nsmgr);

            string strReportName = System.IO.Path.GetFileNameWithoutExtension(strFilename);

            if (xnProc != null)
            {
                Logging.LogMessage("{0}\t{1}", strReportName, xnProc.FirstChild.Value);
            }
            else
                Logging.LogMessage("{0}\tKein Parameter proc", strReportName);
        } // End Sub PrintProc


        public static void PrintMandant(string strFilename)
        {
            System.Xml.XmlDocument doc = XmlTools.File2XmlDocument(strFilename);
            System.Xml.XmlNamespaceManager nsmgr = GetReportNamespaceManager(doc);

            System.Xml.XmlNode xnMandant = doc.SelectSingleNode("/dft:Report/dft:ReportParameters/dft:ReportParameter[@Name=\"in_mandant\"]/dft:DefaultValue/dft:Values/dft:Value", nsmgr);

            string strReportName = System.IO.Path.GetFileNameWithoutExtension(strFilename);

            if (xnMandant != null)
            {
                Logging.LogMessage("{0}\t{1}", strReportName, xnMandant.FirstChild.Value);
            }
            else
                Logging.LogMessage("{0}\tKein Parameter in_mandant", strReportName);
        } // End Sub PrintMandant



        public static void AddMandant(string strFileName)
        {
            string strXmlFragment = @"<ReportParameter Name=""in_mandant"">
<DataType>String</DataType>
<DefaultValue>
<Values>
<Value>0</Value>
</Values>
</DefaultValue>
<Prompt>Mandant</Prompt>
<Hidden>true</Hidden>
</ReportParameter>";

            ReportServerTools.AddCustomParameter(strFileName, "first", strXmlFragment);
        } // End Sub AddMandant



        public static void AddProc(string strFileName)
        {
            string strXmlFragment = @"<ReportParameter Name=""proc"">
<DataType>String</DataType>
<DefaultValue>
<Values>
<Value>administrator</Value>
</Values>
</DefaultValue>
<Prompt>Benutzer</Prompt>
<Hidden>true</Hidden>
</ReportParameter>";

            ReportServerTools.AddCustomParameter(strFileName, "in_mandant", strXmlFragment);
        } // End Sub AddProc



        //[System.Obsolete("Warning: XmlFragment überschrieben - Raiffeisenbastel.", true)]
        public static void AddGroups(string strFileName)
        {
            string strXmlFragment = @"<ReportParameter Name=""in_groups"">
<DataType>String</DataType>
<DefaultValue>
<DataSetReference>
<DataSetName>SEL_User</DataSetName>
<ValueField>BG_IDs</ValueField>
</DataSetReference>
</DefaultValue>
<Prompt>Gruppen des Benutzers</Prompt>
<ValidValues>
<DataSetReference>
<DataSetName>SEL_User</DataSetName>
<ValueField>BG_IDs</ValueField>
<LabelField>BG_Namen</LabelField>
</DataSetReference>
</ValidValues>
<Hidden>true</Hidden>
</ReportParameter>";


            strXmlFragment = @"<ReportParameter Name=""in_groups"">
<DataType>String</DataType>
<DefaultValue>
<Values>
<Value>0000</Value>
</Values>
</DefaultValue>
<Prompt>Gruppen des Benutzers</Prompt>
<Hidden>true</Hidden>
</ReportParameter>
";


            ReportServerTools.AddCustomParameter(strFileName, "proc", strXmlFragment);
        } // End Sub AddProc



        //[System.Obsolete("Warning: XmlFragment überschrieben - Raiffeisenbastel.", true)]
        public static void AddSprache(string strFileName)
        {
            string strXmlFragment = @"<ReportParameter Name=""in_sprache"">
<DataType>String</DataType>
<DefaultValue>
<DataSetReference>
<DataSetName>SEL_User</DataSetName>
<ValueField>BE_Language</ValueField>
</DataSetReference>
</DefaultValue>
<Prompt>Sprache</Prompt>
<ValidValues>
<DataSetReference>
<DataSetName>SEL_User</DataSetName>
<ValueField>BE_Language</ValueField>
<LabelField>BE_Language</LabelField>
</DataSetReference>
</ValidValues>
<Hidden>true</Hidden>
</ReportParameter>";


            strXmlFragment = @"<ReportParameter Name=""in_sprache"">
<DataType>String</DataType>
<DefaultValue>
<Values>
<Value>DE</Value>
</Values>
</DefaultValue>
<Prompt>Benutzersprache</Prompt>
<Hidden>true</Hidden>
</ReportParameter>
";

            ReportServerTools.AddCustomParameter(strFileName, "in_groups", strXmlFragment);
        } // End Sub AddSprache



        public static void AddReportName(string strFileName)
        {
            string strXmlFragment = @"<ReportParameter Name=""in_report_name"">
<DataType>String</DataType>
<DefaultValue>
<Values>
<Value>=Globals!ReportName</Value>
</Values>
</DefaultValue>
<Prompt>Report Name</Prompt>
<Hidden>true</Hidden>
</ReportParameter>";

            ReportServerTools.AddCustomParameter(strFileName, "in_groups", strXmlFragment);
        } // End Sub AddProc


        public static void AddCustomParameter(string strFilename, string strAppendAfterParameter, string strXmlFragment)
        {
            string strName = null;

            if (string.IsNullOrEmpty(strXmlFragment))
            {
                throw new ArgumentNullException("strXmlFragment");
            }

            if (string.IsNullOrEmpty(strAppendAfterParameter))
            {
                throw new ArgumentNullException("strAppendAfterParameter");
            }

            strXmlFragment = strXmlFragment.Trim();

            if (!strXmlFragment.StartsWith("<ReportParameter", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("strXmlFragment does not contain a Parameter");

            strName = GetNameFromFragment(strXmlFragment);

            if (!string.IsNullOrEmpty(strName))
            {
                AddCustomParameter(strFilename, strName, strAppendAfterParameter, strXmlFragment);
            } // End if (!string.IsNullOrEmpty(strName))

        } // End Sub AddCustomParameter 

        public static string GetNameFromFragment(string strXmlFragment)
        {
            string strName = null;

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(strXmlFragment);

            if (doc != null)
            {
                System.Xml.XmlNode xn = doc.DocumentElement;

                if (xn != null)
                {
                    System.Xml.XmlAttribute xaName = xn.Attributes["Name"];

                    if (xaName != null)
                    {
                        strName = xaName.Value;
                        xaName = null;
                    } // End if (xaName != null)

                    xn = null;
                } // End if (xn != null)

                doc = null;
            } // End if (doc != null)

            return strName;
        }


        public static void AddCustomParameter(string strFilename, string strParameterName, string strAppendAfterParameter, string strXmlFragment)
        {
            System.Xml.XmlDocument doc = XmlTools.File2XmlDocument(strFilename);
            System.Xml.XmlNamespaceManager nsmgr = GetReportNamespaceManager(doc);
            
            if (HasParameter(doc, strParameterName))
                return;


            System.Xml.XmlNode xnParameters = doc.SelectSingleNode("/dft:Report/dft:ReportParameters", nsmgr);

            if (xnParameters == null)
                return;


            bool bFirst = StringComparer.OrdinalIgnoreCase.Equals(strAppendAfterParameter, "first");
            bool bLast = StringComparer.OrdinalIgnoreCase.Equals(strAppendAfterParameter, "last");

            
            System.Xml.XmlNode xnAppendAfterThis = null;

            if (bFirst || bLast)
                xnAppendAfterThis = xnParameters;
            else
                xnAppendAfterThis = GetParameter(doc, strAppendAfterParameter);


            AddCustomParameter(strFilename, xnAppendAfterThis, bFirst, bLast, strXmlFragment);
        } // End Function AddCustomParameter


        public static void AddOrReplaceMandantParameter(string strFileName)
        {
            string strXmlFragment = @"<ReportParameter Name=""in_mandant"">
    <DataType>String</DataType>
    <DefaultValue>
        <Values>
            <Value>0</Value>
        </Values>
    </DefaultValue>
    <Prompt>in_mandant</Prompt>
    <Hidden>true</Hidden>
</ReportParameter>
";

            AddOrReplaceParameter(strFileName, "in_mandant", null, strXmlFragment);
        } // End Sub AddOrReplaceMandantParameter



        public static void AddOrReplaceProcParameter(string strFileName)
        {
            string strXmlFragment = @"<ReportParameter Name=""proc"">
<DataType>String</DataType>
<DefaultValue>
<Values>
<Value>administrator</Value>
</Values>
</DefaultValue>
<Prompt>Benutzer</Prompt>
<Hidden>true</Hidden>
</ReportParameter>";

            AddOrReplaceParameter(strFileName, "proc", "in_mandant", strXmlFragment);
        } // End Sub AddOrReplaceProcParameter



        public static void AddOrReplaceGroupsParameter(string strFilename)
        {
            AddOrReplaceParameter(strFilename, "in_groups", "in_sprache", @"<ReportParameter Name=""in_groups"">
    <DataType>String</DataType>
    <DefaultValue>
        <DataSetReference>
            <DataSetName>SEL_User</DataSetName>
            <ValueField>BG_IDs</ValueField>
        </DataSetReference>
    </DefaultValue>
    <Prompt>Benutzergruppen</Prompt>
    <Hidden>true</Hidden>
    <ValidValues>
        <DataSetReference>
            <DataSetName>SEL_User</DataSetName>
            <ValueField>BG_IDs</ValueField>
            <LabelField>BG_Namen</LabelField>
        </DataSetReference>
    </ValidValues>
</ReportParameter>
");
        } // End Sub AddOrReplaceGroupsParameter


        public static void AddOrReplaceLanguageParameter(string strFilename)
        {
            AddOrReplaceParameter(strFilename, "in_sprache", "proc",@"<ReportParameter Name=""in_sprache"">
    <DataType>String</DataType>
    <DefaultValue>
        <DataSetReference>
            <DataSetName>SEL_User</DataSetName>
            <ValueField>BE_Language</ValueField>
        </DataSetReference>
    </DefaultValue>
    <Prompt>Sprache</Prompt>
    <Hidden>true</Hidden>
    <ValidValues>
        <DataSetReference>
            <DataSetName>SEL_User</DataSetName>
            <ValueField>BE_Language</ValueField>
            <LabelField>BE_Language</LabelField>
        </DataSetReference>
    </ValidValues>
</ReportParameter>
");

        } // End Sub AddOrReplaceLanguageParameter


        public static void AddReportNameParameter(string strFilename)
        {
            ReportServerTools.AddCustomParameter(strFilename, "in_groups", @"<ReportParameter Name=""in_report_name"">
    <DataType>String</DataType>
    <DefaultValue>
        <Values>
            <Value>=Globals!ReportName</Value>
        </Values>
    </DefaultValue>
    <Prompt>in_report_name</Prompt>
    <Hidden>true</Hidden>
</ReportParameter>");
        }

        
        public static void AddOrReplaceParameter(string strFilename, string parameterName, string strAppendAfterParameter, string fragment)
        {
            string strName = GetNameFromFragment(fragment);
            if (!StringComparer.InvariantCultureIgnoreCase.Equals(strName, parameterName))
                throw new Exception("Parameter name mismatch - \"" + parameterName + "\" != \"" + strName + "\"");

            System.Xml.XmlNode xnPrevious = null;
            System.Xml.XmlNode xn = GetParameter(strFilename, parameterName);
            bool bFirst = false;


            if (xn != null)
            {
                xnPrevious = xn.PreviousSibling;
                xn.ParentNode.RemoveChild(xn);
                XmlTools.SaveDocument(xn.OwnerDocument, strFilename, true);
            } // End if (xn != null)

            if (xnPrevious == null)
            {
                xnPrevious = GetParameter(strFilename, strAppendAfterParameter);
            } // End if (xnPrevious == null)

            if (xnPrevious == null)
            {
                System.Xml.XmlDocument doc = XmlTools.File2XmlDocument(strFilename);
                System.Xml.XmlNamespaceManager nsmgr = GetReportNamespaceManager(doc);
                xnPrevious = doc.SelectSingleNode("/dft:Report/dft:ReportParameters", nsmgr);
                bFirst = true;


                if (xnPrevious == null)
                    return;
            } // End if (xnPrevious == null)

            AddCustomParameter(strFilename, xnPrevious, bFirst, false, fragment);
        } // End Sub AddOrReplaceParameter
        

        public static void AddCustomParameter(string strFilename, System.Xml.XmlNode xnInsertHere, bool bFirst, bool bLast, string strXmlFragment)
        {
            string strReportName = System.IO.Path.GetFileNameWithoutExtension(strFilename);


            if (xnInsertHere != null)
            {
                Logging.LogMessage("{0}\t{1}", strReportName, "hasParameters");

                System.Xml.XmlDocument doc = xnInsertHere.OwnerDocument;
                System.Xml.XmlDocumentFragment xmlDocFrag = doc.CreateDocumentFragment();
                xmlDocFrag.InnerXml = strXmlFragment;


                bool bDoNotFechParameter = bFirst || bLast;

                if (bDoNotFechParameter)
                {
                    if (bFirst)
                        xnInsertHere.PrependChild(xmlDocFrag);
                    else
                        xnInsertHere.AppendChild(xmlDocFrag);
                }
                else
                {
                    if (StringComparer.InvariantCultureIgnoreCase.Equals(xnInsertHere.LocalName, "ReportParameters"))
                        xnInsertHere.AppendChild(xmlDocFrag);
                    else
                        xnInsertHere.ParentNode.InsertAfter(xmlDocFrag, xnInsertHere);
                } // End else of if (bDoNotFechParameter)

                XmlTools.SaveDocument(doc, strFilename, true);
            } // End if (xn
            else
                Logging.LogMessage("{0}\tKeine Parameter in Report.", strReportName);
        } // End Sub AddCustomParameter


        public static void Add_SEL_User_DataSet(string strFileName)
        {
            string strXmlFragment = @"<DataSet Name=""SEL_User"">
      <Query>
        <DataSourceName>COR_Basic</DataSourceName>
        <QueryParameters>
          <QueryParameter Name=""@proc"">
            <Value>=Parameters!proc.Value</Value>
          </QueryParameter>
        </QueryParameters>
        <CommandType>StoredProcedure</CommandType>
        <CommandText>sp_RPT_SEL_GetCurrentUser</CommandText>
        <Timeout>300</Timeout>
      </Query>
      <Fields>
        <Field Name=""BE_ID"">
          <DataField>BE_ID</DataField>
          <rd:TypeName>System.Int32</rd:TypeName>
        </Field>
        <Field Name=""BE_Name"">
          <DataField>BE_Name</DataField>
          <rd:TypeName>System.String</rd:TypeName>
        </Field>
        <Field Name=""BE_Vorname"">
          <DataField>BE_Vorname</DataField>
          <rd:TypeName>System.String</rd:TypeName>
        </Field>
        <Field Name=""BE_User"">
          <DataField>BE_User</DataField>
          <rd:TypeName>System.String</rd:TypeName>
        </Field>
        <Field Name=""BE_Language"">
          <DataField>BE_Language</DataField>
          <rd:TypeName>System.String</rd:TypeName>
        </Field>
        <Field Name=""BE_Level"">
          <DataField>BE_Level</DataField>
          <rd:TypeName>System.Byte</rd:TypeName>
        </Field>
        <Field Name=""BE_isLDAPSync"">
          <DataField>BE_isLDAPSync</DataField>
          <rd:TypeName>System.Boolean</rd:TypeName>
        </Field>
        <Field Name=""BG_IDs"">
          <DataField>BG_IDs</DataField>
          <rd:TypeName>System.String</rd:TypeName>
        </Field>
        <Field Name=""BG_Namen"">
          <DataField>BG_Namen</DataField>
          <rd:TypeName>System.String</rd:TypeName>
        </Field>
      </Fields>
    </DataSet>";

            ReportServerTools.AddCustomDataSet(strFileName, strXmlFragment);
        } // End Sub Add_SEL_User_DataSet


        public static string CreateFragmentString(string strFilename, string strXmlFragment)
        {
            System.Xml.XmlDocument doc = XmlTools.File2XmlDocument(strFilename);
            System.Xml.XmlNamespaceManager nsmgr = GetReportNamespaceManager(doc);
            doc = null;

            string strXmlFragment2 = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Report ";

            IDictionary<string, string> dic = nsmgr.GetNamespacesInScope(System.Xml.XmlNamespaceScope.Local);
            foreach (System.Collections.Generic.KeyValuePair<string,string> kvp in dic)
            {
                if (StringComparer.InvariantCultureIgnoreCase.Equals(kvp.Key, "dft"))
                    strXmlFragment2 += "xmlns=\"" + kvp.Value + "\" ";
                else
                    strXmlFragment2 += "xmlns:" + kvp.Key + "=\"" + kvp.Value + "\" ";
                System.Console.WriteLine(kvp);
            } // Next kvp 
            strXmlFragment2 += ">\n";
            strXmlFragment2 += strXmlFragment;


            strXmlFragment2 += @"
</Report>";

            return strXmlFragment2;
        } // End Function CreateFragmentString


        public static void AddCustomDataSet(string strFilename, string strXmlFragment)
        {
            string strName = null;

            if (string.IsNullOrEmpty(strXmlFragment))
            {
                throw new ArgumentNullException("strXmlFragment");
            }

            strXmlFragment = strXmlFragment.Trim();

            if (!strXmlFragment.StartsWith("<DataSet", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("strXmlFragment does not contain a DataSet");

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(CreateFragmentString(strFilename, strXmlFragment));


            if (doc != null)
            {
                System.Xml.XmlNode xn = doc.DocumentElement;
                xn = xn.FirstChild;

                if (xn != null)
                {
                    System.Xml.XmlAttribute xaName = xn.Attributes["Name"];

                    if (xaName != null)
                    {
                        strName = xaName.Value;
                        xaName = null;
                    } // End if (xaName != null)

                    xn = null;
                } // End if (xn != null)

                doc = null;
            } // End if (doc != null)


            if (!string.IsNullOrEmpty(strName))
            {
                AddCustomDataSet(strFilename, strName, strXmlFragment);
            } // End if (!string.IsNullOrEmpty(strName))

        } // End Sub AddCustomDataSet 


        public static void AddCustomDataSet(string strFilename, string strDataSetName, string strXmlFragment)
        {
            System.Xml.XmlDocument doc = XmlTools.File2XmlDocument(strFilename);
            System.Xml.XmlNamespaceManager nsmgr = GetReportNamespaceManager(doc);

            if (HasDataSet(doc, strDataSetName))
                return;

            string strReportName = System.IO.Path.GetFileNameWithoutExtension(strFilename);
            System.Xml.XmlNode xnDataSets = doc.SelectSingleNode("/dft:Report/dft:DataSets", nsmgr);

            if (xnDataSets != null)
            {
                System.Xml.XmlNode xnAppendAfterThis = null;

                // bool bFirst = StringComparer.OrdinalIgnoreCase.Equals(strAppendAfterParameter, "first");
                // bool bLast = StringComparer.OrdinalIgnoreCase.Equals(strAppendAfterParameter, "last");

                // bool bDoNotFechParameter = bFirst || bLast;

                // if (!bDoNotFechParameter)
                // xnAppendAfterThis = GetParameter(doc, strAppendAfterParameter);
                xnAppendAfterThis = xnDataSets; 

                if (xnAppendAfterThis != null) // || bDoNotFechParameter)
                {
                    Logging.LogMessage("{0}\t{1}", strReportName, "Has DataSets");



                    System.Xml.XmlDocument doc2 = new System.Xml.XmlDocument();
                    doc2.LoadXml(CreateFragmentString(strFilename, strXmlFragment));
                    System.Xml.XmlNode fragmentToInsert = doc2.DocumentElement.FirstChild;
                    doc2 = null;

                    // System.Xml.XmlDocumentFragment xmlDocFrag = doc.CreateDocumentFragment();
                    // xmlDocFrag.InnerXml = strXmlFragment;
                    // // xmlDocFrag.AppendChild(doc.ImportNode(fragmentToInsert, true));


                    // employeeNode.AppendChild(employeeNode.OwnerDocument.ImportNode(otherXmlDocument.DocumentElement, true));
                    // xnAppendAfterThis.ParentNode.InsertAfter(fragmentToInsert, xnAppendAfterThis);
                    // xnAppendAfterThis.ParentNode.InsertAfter(doc.ImportNode(fragmentToInsert, true), xnAppendAfterThis);
                    xnAppendAfterThis.AppendChild(doc.ImportNode(fragmentToInsert, true));

                    // xnAppendAfterThis.ParentNode.InsertAfter(xmlDocFrag, xnAppendAfterThis);

                    //if (bDoNotFechParameter)
                    //{
                    //    if (bFirst)
                    //        xnDataSets.PrependChild(xmlDocFrag);
                    //    else
                    //        xnDataSets.AppendChild(xmlDocFrag);
                    //}
                    //else
                    //{
                    //    xnAppendAfterThis.ParentNode.InsertAfter(xmlDocFrag, xnAppendAfterThis);
                    //} // End else of if (bDoNotFechParameter)
                    
                } // End if (xnAppendAfterThis != null || string.IsNullOrEmpty(strAppendAfterParameter))
                else
                    Logging.LogMessage("{0}\tKeine Parameter in Report.", strReportName);

            } // End if (xnParameters != null)

            XmlTools.SaveDocument(doc, strFilename, true);
            //XmlTools.SaveDocument(doc, strFilename, "<ReportParameter Name=\"proc\" xmlns=\"\">", "<ReportParameter Name=\"proc\">");
        } // End Sub AddCustomDataSet


        public static System.Xml.XmlNode GetImageTag(System.Xml.XmlDocument doc, string strImageName)
        {
            strImageName = XmlTools.XmlEscape(strImageName);

            System.Xml.XmlNamespaceManager nsmgr = GetReportNamespaceManager(doc);
            // doc.SelectSingleNode("/dft:Report/dft:ReportParameters/dft:ReportParameter[@Name=\"in_standort\"]/dft:Prompt", nsmgr);
            //string str = "/dft:Report/dft:Page/dft:PageHeader/dft:ReportItems/dft:Image[@Name=\"" + strImageName + "\"]";
            string str = "//dft:ReportItems/dft:Image[@Name=\"" + strImageName + "\"]";
            System.Xml.XmlNode xnParam = doc.SelectSingleNode(str, nsmgr);

            return xnParam;
        } // End Function GetImageTag


        // AlterImage(strFileName, "in_gebaeude", "Gebäude / Bâtiment / Edificio / Building");
        public static void AlterImage(string strFilename, string strReportParameterName)
        {
            System.Xml.XmlDocument doc = XmlTools.File2XmlDocument(strFilename);
            System.Xml.XmlNamespaceManager nsmgr = GetReportNamespaceManager(doc);


            string strReportName = System.IO.Path.GetFileNameWithoutExtension(strFilename);

            System.Xml.XmlNode xnImageTag = GetImageTag(doc, strReportParameterName);
            // System.Console.WriteLine(xnImageTag);

            if (xnImageTag == null)
            {
                Logging.LogMessage("{0}\tKein Image in " + strReportParameterName, strReportName);
                return;
            } // End if (xnImageTag == null)

            // http://www.w3schools.com/xpath/xpath_syntax.asp
            // http://www.bennadel.com/blog/2142-using-and-expressions-in-xpath-xml-search-directives-in-coldfusion.htm

            System.Xml.XmlNode xnSource = xnImageTag.SelectSingleNode("./dft:Source", nsmgr);
            xnSource.InnerText = "External";

            System.Xml.XmlNode xnValue = xnImageTag.SelectSingleNode("./dft:Value", nsmgr);
            xnValue.InnerText = "logo_immo.png";

            System.Xml.XmlNode xnSizing = xnImageTag.SelectSingleNode("./dft:Sizing", nsmgr);
            xnSizing.InnerText = "FitProportional";


            System.Xml.XmlNode xnLeft = xnImageTag.SelectSingleNode("./dft:Left", nsmgr);
            // System.Xml.XmlNode xnPageWidth = doc.SelectSingleNode("/dft:Report/dft:Page/dft:PageWidth", nsmgr);
            // System.Xml.XmlNode xnPageHeight = doc.SelectSingleNode("/dft:Report/dft:Page/dft:PageHeight", nsmgr);

            if (xnLeft != null)
            {

                double strLeft = ConvertToCm(xnLeft.InnerText);
                // double strPageWidth = ConvertToCm(xnPageWidth.InnerText);
                // double strPageHeight = ConvertToCm(xnPageHeight.InnerText);



                if (strLeft > 10) // Right aligned logo
                {
                    System.Xml.XmlNode xnStyle = xnImageTag.SelectSingleNode("./dft:Style", nsmgr);

                    if (xnStyle != null)
                    {
                        System.Xml.XmlNode xnPadding = xnStyle.SelectSingleNode("./dft:PaddingLeft", nsmgr);

                        if (xnPadding == null)
                        {
                            var ele = doc.CreateElement("PaddingLeft", doc.DocumentElement.NamespaceURI);
                            ele.InnerText = "45pt";
                            System.Xml.XmlNode xnCrap = xnStyle.AppendChild(ele);
                            // xnCrap.Attributes.Remove(xnCrap.Attributes["xmlns"]);
                        } // End if (xnPadding == null)

                    } // End if (xnStyle != null)

                } // End if (strLeft > 10) 

            } // End if (xnLeft != null)
            // else System.Console.WriteLine("Null left " + strFilename);

            ///dft:Report/dft:Page/dft:PageHeader/dft:ReportItems/dft:Image/dft:Style/dft:PaddingLeft

            XmlTools.SaveDocument(doc, strFilename);
        } // End Sub ChangeParameterPrompt


        public static double ConvertToCm(string str)
        {
            double dbl = 0.0;

            if (string.IsNullOrEmpty(str))
                return dbl;

            str = str.Replace("cm", ""); //.Replace("mm", "");
            if(!double.TryParse(str, out dbl))
                throw new InvalidCastException("str is not a double");

            return dbl;
        } // End Function ConvertToCm


        public static void CopyToSaveDirectory(string strFileName)
        {
            string strSavePath = GetSavePath();
            strSavePath = System.IO.Path.Combine(strSavePath, System.IO.Path.GetFileName(strFileName));

            System.IO.File.Copy(strFileName, strSavePath, true);
        } // End Sub CopyToSaveDirectory


        public static bool IsReadOnly(string strFileName)
        {
            System.IO.FileInfo fiThisFile = new System.IO.FileInfo(strFileName);
            if (!fiThisFile.Exists)
                return false;

            //if((fiThisFile.Attributes & System.IO.FileAttributes.ReadOnly) == System.IO.FileAttributes.ReadOnly) 
            if ((fiThisFile.Attributes & System.IO.FileAttributes.ReadOnly) > 0) // The file is read-only (i.e. write-protected)
            {
                return true; 
            }

            return false;
        } // End Function IsReadOnly


        private static void SetReadOnlyAttribute(string strFileName, bool readOnly)
        {
            System.IO.FileInfo fiThisFile = new System.IO.FileInfo(strFileName);
            if (!fiThisFile.Exists)
            {
                fiThisFile = null;
                return;
            } // End if (!fiThisFile.Exists)
            

            System.IO.FileAttributes old_attributes = fiThisFile.Attributes;
            System.IO.FileAttributes new_attributes = old_attributes;

            if (readOnly)
                new_attributes |= System.IO.FileAttributes.ReadOnly; // new_attributes = fiThisFile.Attributes | System.IO.FileAttributes.ReadOnly;
            else
                new_attributes &= ~System.IO.FileAttributes.ReadOnly; //new_attributes = (System.IO.FileAttributes)(fiThisFile.Attributes - System.IO.FileAttributes.ReadOnly);
            //new_attributes ^= System.IO.FileAttributes.ReadOnly; // This toggles value, not unset...

            if (old_attributes != new_attributes) // Don't perform operation if not necessary
                System.IO.File.SetAttributes(fiThisFile.FullName, new_attributes);

            fiThisFile = null;
        } // End Sub SetReadOnlyAttribute


        public static void CopyBack(string strTargetFileName)
        {
            string strSourcePath = GetSavePath();
            strSourcePath = System.IO.Path.Combine(strSourcePath, System.IO.Path.GetFileName(strTargetFileName));

            SetReadOnlyAttribute(strTargetFileName, false);
            System.IO.File.Copy(strSourcePath, strTargetFileName, true);
        } // End Sub CopyBack


    } // End Class ReportServerTools


} // End Namespace ReportModifier
