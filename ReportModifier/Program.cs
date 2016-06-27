
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace ReportModifier
{

    
    //=FormatDateTime(DateSerial(Year(Today),Month(Today),Day(Today)),2)


    //=DateSerial(Year(Today)-5,Month(Today),Day(Today))
    //=DateSerial(Year(Today)+5,Month(Today),Day(Today))
    static class Program
    {


        public static List<string> GetAllReports(string strPath)
        {
            List<string> ls = new List<string>();
            ls.AddRange(System.IO.Directory.GetFiles(strPath, "*.rdl"));

            return ls;
        } // End Function GetAllReports


        public static void AlterReports(string strPath)
        {
            List<string> lsReports = GetAllReports(strPath);


            List<string> lsExclude = new List<string>();

            lsExclude.Add("AL_Anlageblatt");
            lsExclude.Add("AL_Anlagenkosten");
            lsExclude.Add("AL_Kostenuebersicht");
            lsExclude.Add("AP_Arbeitsplatzuebersicht_ML");
            lsExclude.Add("BL_Belegung_ML");
            lsExclude.Add("BO_History");
            lsExclude.Add("FM_BelegungNachMieter_ML");
            lsExclude.Add("FM_FlaecheNachBodenbelag_ML");
            lsExclude.Add("FM_FlaecheNachEnergiebezug");
            lsExclude.Add("FM_FlaecheNachMietertrag");
            lsExclude.Add("FM_FlaecheNachNutzungsart");
            lsExclude.Add("FM_FlaecheNachSIA");
            lsExclude.Add("FM_MietertragNachMieter_ML");
            lsExclude.Add("FM_NutzungsartenDIN_277_SNB_ML");
            lsExclude.Add("FM_NutzungsartenDIN_277_Wincasa_ML");
            lsExclude.Add("FM_NutzungsartenSIA_ML");
            lsExclude.Add("GM_Gebaeudebasisdaten_ML");
            lsExclude.Add("GM_Gebaeudestammdaten_ML");
            lsExclude.Add("KU_Kunstinventar_ML");
            lsExclude.Add("REM_Raumliste_Reinigung_ML");
            lsExclude.Add("RM_Arbeitsplatzbelegung");
            lsExclude.Add("TM_Aufgabenplanungsuebersicht_ML");
            lsExclude.Add("TM_Aufgabenuebersicht_ML");
            lsExclude.Add("TM_Aufgabenuebersicht_SNB_ML");
            lsExclude.Add("TM_Auftrag_ML");
            lsExclude.Add("TM_Reinigungsuebersicht_ML");
            lsExclude.Add("UPS_Budgetuebersicht_ML");


            for (int i = 0; i < lsExclude.Count; ++i)
            {
                lsExclude[i] = lsExclude[i].ToLower();
            } // Next i
            

            foreach (string strFileName in lsReports)
            {
                //if (!lsExclude.Contains(System.IO.Path.GetFileNameWithoutExtension(strFileName).ToLower())) continue;

                //ReportServerTools.CopyToSaveDirectory(strFileName);


                // ReportServerTools.PrintStichtag(strFileName);
                ReportServerTools.ChangeStichtag(strFileName);
                ReportServerTools.CopyBack(strFileName);


                ReportServerTools.ChangeParameterPrompt(strFileName, "in_standortkategorie", "Standortkategorie / Catégorie site / Categoria sito / Site category");
                ReportServerTools.CopyBack(strFileName);
                
                ReportServerTools.ChangeParameterPrompt(strFileName, "in_standort", "Liegenschaft / Immeuble / Patrimonio immobiliare / Estate");
                //ReportServerTools.ChangeParameterPrompt(strFileName, "in_standort", "Standort / Site / Sito / Site");
                ReportServerTools.CopyBack(strFileName);

                ReportServerTools.ChangeParameterPrompt(strFileName, "in_gebaeude", "Gebäude / Bâtiment / Edificio / Building");
                ReportServerTools.CopyBack(strFileName);

                ReportServerTools.ChangeParameterPrompt(strFileName, "in_geschoss", "Geschoss / Étage / Piano / Floor");
                ReportServerTools.CopyBack(strFileName);

                ReportServerTools.ChangeParameterPrompt(strFileName, "in_trakt", "Trakt / Aile / Ala / Wing");
                ReportServerTools.CopyBack(strFileName);

                ReportServerTools.ChangeParameterPrompt(strFileName, "in_haus", "Haus / Maison / Casa / House");
                ReportServerTools.CopyBack(strFileName);

                ReportServerTools.ChangeParameterPrompt(strFileName, "in_raum", "Raum / Pièce / Stanza / Room");
                ReportServerTools.CopyBack(strFileName);

                ReportServerTools.ChangeParameterPrompt(strFileName, "in_stichtag", "Stichtag / Jour de référence / Giorno di riferimento / Reporting date"); 
                ReportServerTools.CopyBack(strFileName);

                
              

                // Here
                ReportServerTools.ChangeParameterPrompt(strFileName, "in_mietertrag", "Mindestertrag Mindestertrag / Rendement minimum / Rendimento minimo / Minimum yield"); ReportServerTools.CopyBack(strFileName);
                ReportServerTools.ChangeParameterPrompt(strFileName, "in_jahr", "Jahr / Année / Anno / Year"); ReportServerTools.CopyBack(strFileName);
                ReportServerTools.ChangeParameterPrompt(strFileName, "in_stichjahr", "Jahr / Année / Anno / Year"); ReportServerTools.CopyBack(strFileName);
                ReportServerTools.ChangeParameterPrompt(strFileName, "in_budgetjahr", "Budgetjahr / Année Budgétaire / Esercizio Finanziario / Budget Year"); ReportServerTools.CopyBack(strFileName);
                ReportServerTools.ChangeParameterPrompt(strFileName, "in_jahr_von", "Jahr von / Année de / Anno di / Year from"); ReportServerTools.CopyBack(strFileName);
                ReportServerTools.ChangeParameterPrompt(strFileName, "in_jahr_bis", "Jahr bis / Année à / Anno a / Year to"); ReportServerTools.CopyBack(strFileName);
                ReportServerTools.ChangeParameterPrompt(strFileName, "in_objekt", "Objekt / Objet / Oggetto / Object"); ReportServerTools.CopyBack(strFileName);
                ReportServerTools.ChangeParameterPrompt(strFileName, "in_status", "Status / Statut / Stato / Status"); ReportServerTools.CopyBack(strFileName);
                ReportServerTools.ChangeParameterPrompt(strFileName, "in_prioritaet", "Priorität / Priorité / Priorità / Priority"); ReportServerTools.CopyBack(strFileName);
                ReportServerTools.ChangeParameterPrompt(strFileName, "in_termin_von", "Termin von / A partir de / A partire dalla / Date from"); ReportServerTools.CopyBack(strFileName);
                ReportServerTools.ChangeParameterPrompt(strFileName, "in_termin_bis", "Termin bis / Jusqu'au / Fino al / Date to"); ReportServerTools.CopyBack(strFileName);
                ReportServerTools.ChangeParameterPrompt(strFileName, "in_verantwortlich", "Verantwortlich / Responsable / Responsabile / Responsible"); ReportServerTools.CopyBack(strFileName);
                ReportServerTools.ChangeParameterPrompt(strFileName, "in_nutzungsart", "Nutzungsart / Type d'utilisation / Tipo di utilizzo / Usage type"); ReportServerTools.CopyBack(strFileName);
       

                // ReportServerTools.PrintProc(strFileName);
                // ReportServerTools.PrintMandant(strFileName);
            } // Next strFileName

        } // End Sub InvestigateStichtag



        public static void AlterRaiffeisenReports(string strPath)
        {
            List<string> lsReports = GetAllReports(strPath);

            List<string> lsExclude = new List<string>();

            for (int i = 0; i < lsExclude.Count; ++i)
            {
                lsExclude[i] = lsExclude[i].ToLower();
            } // Next i


            foreach (string strFileName in lsReports)
            {
                //if (!lsExclude.Contains(System.IO.Path.GetFileNameWithoutExtension(strFileName).ToLower())) continue;

                //ReportServerTools.PrintStichtag(strFileName);
                ReportServerTools.ChangeParameterPrompt(strFileName, "in_standort", "Standort / Site / Sito / Site");
                //ReportServerTools.ChangeParameterPrompt(strFileName, "Standort", "Standort / Site / Sito / Site"); // Raiffeisen
                

                //ReportServerTools.ChangeStichtag(strFileName);
                //ReportServerTools.CopyBack(strFileName);

                //ReportServerTools.AddMandant(strFileName);
                //ReportServerTools.CopyBack(strFileName);

                //ReportServerTools.AddProc(strFileName);
                //ReportServerTools.CopyBack(strFileName);


                //ReportServerTools.AddReportName(strFileName);
                //ReportServerTools.CopyBack(strFileName);
                //ReportServerTools.AddCustomParameter(strFileName,"first","");

                //ReportServerTools.AddGroups(strFileName);
                //ReportServerTools.CopyBack(strFileName);

                //ReportServerTools.AddSprache(strFileName);
                //ReportServerTools.CopyBack(strFileName);
            } // Next strFileName

        } // End Sub AlterRaiffeisenReports





        public static void AlterStzhReports(string strPath)
        {
            List<string> lsReports = GetAllReports(strPath);


            foreach (string strFileName in lsReports)
            {
                //if (!lsExclude.Contains(System.IO.Path.GetFileNameWithoutExtension(strFileName).ToLower())) continue;

                //ReportServerTools.PrintStichtag(strFileName);
                // ReportServerTools.ChangeParameterPrompt(strFileName, "in_standort", "Standort / Site / Sito / Site");

                ReportServerTools.AlterImage(strFileName, "image1");

                //ReportServerTools.ChangeParameterPrompt(strFileName, "Standort", "Standort / Site / Sito / Site"); // Raiffeisen


                //ReportServerTools.ChangeStichtag(strFileName);
                //ReportServerTools.CopyBack(strFileName);

                //ReportServerTools.AddMandant(strFileName);
                //ReportServerTools.CopyBack(strFileName);

                //ReportServerTools.AddProc(strFileName);
                //ReportServerTools.CopyBack(strFileName);


                //ReportServerTools.AddReportName(strFileName);
                //ReportServerTools.CopyBack(strFileName);
                //ReportServerTools.AddCustomParameter(strFileName,"first","");

                //ReportServerTools.AddGroups(strFileName);
                //ReportServerTools.CopyBack(strFileName);

                //ReportServerTools.AddSprache(strFileName);
                //ReportServerTools.CopyBack(strFileName);
            } // Next strFileName


        }



        public static void CheckProcParameter(string strPath)
        {
            List<string> lsReports = GetAllReports(strPath);

            List<string> lsExclude = new List<string>();
            List<string> lsExcludeStartsWith = new List<string>();

            lsExcludeStartsWith.Add("zzz");
            lsExcludeStartsWith.Add("xxx");

            string strFileName = null;
            foreach (string internalstrFileName in lsReports)
            {
                strFileName = internalstrFileName;

                string Report = System.IO.Path.GetFileNameWithoutExtension(strFileName);
                

                bool doContinue = false;

                foreach(var strExcludeString in lsExcludeStartsWith)
                {
                    if (Report.StartsWith(strExcludeString, StringComparison.InvariantCultureIgnoreCase))
                    {
                        doContinue = true;
                        break;
                    }
                        
                }
                
                if(doContinue)
                    continue;


                string paraName = "proc";
                string dataSetName = "SEL_User";
                string strStichtag = ReportServerTools.GetParameterDefaultValue(strFileName, "in_stichtag");
                bool bHasProcParameter = ReportServerTools.HasParameter(strFileName, paraName);
                bool bHasSEL_UserDataSet = ReportServerTools.HasParameter(strFileName, dataSetName);


                bHasProcParameter = ReportServerTools.HasParameter(strFileName, "in_mandant");
                bHasProcParameter = ReportServerTools.HasParameter(strFileName, "in_report_name");
                // bHasStichtagParameter = ReportServerTools.HasParameter(strFileName, "in_stichtag");
                // if (string.IsNullOrEmpty(strStichtag)) bHasStichtagParameter = false;

                // if(bHasStichtagParameter)
                // if (!bHasProcParameter)
                if (true)
                {
                    System.Console.WriteLine("Report {0}", Report);
                    
                    //ReportServerTools.AddMainParametersNode(strFileName);


                    //ReportServerTools.AddOrReplaceMandantParameter(strFileName);
                    //ReportServerTools.AddOrReplaceProcParameter(strFileName);
                    //ReportServerTools.Add_SEL_User_DataSet(strFileName);
                    //ReportServerTools.AddOrReplaceLanguageParameter(strFileName);
                    //ReportServerTools.AddOrReplaceGroupsParameter(strFileName);
                    //ReportServerTools.AddReportNameParameter(strFileName);

                    // =FormatDateTime(DateSerial(Year(Today),Month(Today),Day(Today)),2)


                    // =FormatDateTime(DateSerial(Year(Today),Month(Today),Day(Today)),2)
                    // =System.DateTime.Now.ToString("dd.MM.yyyy")
                    if(!StringComparer.InvariantCultureIgnoreCase.Equals(strStichtag,"=System.DateTime.Now.ToString(\"dd.MM.yyyy\")"))
                        System.Console.WriteLine("    - Stichtag: {0}", strStichtag);
                    //System.Console.WriteLine("    - has {0}: {1}", paraName, bHasProcParameter);
                    //System.Console.WriteLine("    - has {0}: {1}", dataSetName, bHasSEL_UserDataSet);
                    //System.Console.WriteLine(Environment.NewLine);
                } // End if (bHasParameter)

                
                

            } // Next strFileName

        } // End Sub CheckProcParameter 


        public static string GetAssemblyDirectory(System.Reflection.Assembly ass)
        {
            string codeBase = ass.CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            //return System.IO.Path.GetDirectoryName(path);
            return path;
        }


        public static void GetDependencies(System.Reflection.Assembly ass, List<System.Reflection.Assembly>ls, List<string>paths, string name)
        {
            name += "/" + ass.GetName().Name;
            paths.Add(name);

            if (StringComparer.OrdinalIgnoreCase.Equals(ass.GetName().Name, "Microsoft.ReportViewer.ProcessingObjectModel"))
            {
                string assemblyPath = GetAssemblyDirectory(ass);
                System.Console.WriteLine(assemblyPath);
                string assemblyFileName = System.IO.Path.GetFileName(assemblyPath);

                // System.IO.File.Copy(assemblyPath, @"d:\" + assemblyFileName);
            }
            


            System.Reflection.AssemblyName[] asmNames = ass.GetReferencedAssemblies();

            foreach (System.Reflection.AssemblyName asmn in asmNames)
            {
                System.Reflection.Assembly ass2 = System.Reflection.Assembly.Load(asmn);

                if (!ls.Contains(ass2))
                {
                    ls.Add(ass2);
                    GetDependencies(ass2, ls, paths, name);
                    //ls.AddRange(GetDependencies(ass2));
                }
                    
            }

        }


        public static void ViewDependencies()
        {
            System.Reflection.Assembly ass = typeof(Microsoft.Reporting.WebForms.ReportViewer).Assembly;
            string name = "";
            // ass = System.Reflection.Assembly.GetExecutingAssembly();

            System.Collections.Generic.List<System.Reflection.Assembly> ls = new List<System.Reflection.Assembly>();
            System.Collections.Generic.List<string> paths = new List<string>();
            GetDependencies(ass, ls, paths, name);
            System.Console.WriteLine(paths);
            System.Console.WriteLine(ls);

        }




        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        /// 
        [STAThread]
        static void Main()
        {
            // ReportServerTools.GetDataSets(@"D:\Stefan.Steiger\Documents\Visual Studio 2013\TFS\COR-FM-Suite\Portal_2014\Portal_Reports\LeaseContractForm.rdl");
            // ReportServerTools.GetParameters(@"D:\Stefan.Steiger\Documents\Visual Studio 2013\TFS\COR-FM-Suite\Portal_2014\Portal_Reports\LeaseContractForm.rdl");


            ViewDependencies();
            return;

            string strPath = @"S:\StefanSteiger\COR_Basic\cor_basic";
            strPath = @"S:\StefanSteiger\Raiffeisen_Reps";
            strPath = @"D:\Stefan.Steiger\Desktop\Upload";
            strPath = @"D:\stefan.steiger\Downloads\Repis";
            strPath = @"D:\Stefan.Steiger\Documents\Visual Studio 2008\Projects\Basic_Reports\Basic_Reports";
            strPath = @"D:\stefan.steiger\Downloads\AutoModifiedReports";


            //AlterReports(strPath);
            //OldMain();
            // AlterRaiffeisenReports(strPath);
            // AlterStzhReports(strPath);
            CheckProcParameter(strPath);

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine(" --- Press any key to continue --- ");
            Console.ReadKey();
        } // End Sub Main


        [STAThread]
        static void OldMain()
        {
            bool b = false; // Remove "Unreachable Code" warning
            if (b)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            } // End if (false)


            string strPath = @"S:\StefanSteiger\COR_Basic\";

            //string strReport = "GM_Gebaeudestammdaten.rdl";
            string strReport = "GM_Gebaeudestammdaten_ML.rdl";
            strReport = "AL_Anlageninventar_ML.rdl";
            strReport = "FM_NutzungsartenDIN_277_Wincasa_ML.rdl";
            strReport = "FM_NutzungsartenDIN_277_Wincasa_ML.rdl";
            strReport = "KU_Kunstinventar_ML.rdl";
            
            string FILE_NAME = System.IO.Path.Combine(strPath, strReport);

            // AddProc(FILE_NAME);
            // AddMandant(FILE_NAME);

            // ReadUsingReportViewer(FILE_NAME);
            //Select the cd node with the matching title
            System.Xml.XmlDocument doc = XmlTools.File2XmlDocument(FILE_NAME);



            string strXML = @"
<result>
    <relatedProducts>
        <item>
            <id>123foo</id>
            <name>foo</name>
            <text>Foobar</text>
        </item>
        <item>
            <id>hello</id>
            <name>bye</name>
            <text>ciao</text>
        </item>
        <item>
            <id></id>
            <name></name>
            <text></text>
        </item>
    </relatedProducts>
</result>
";
            System.Xml.XmlDocument mydoc = new System.Xml.XmlDocument();
            mydoc.LoadXml(strXML);

            //System.Xml.XmlNodeList x = mydoc.SelectNodes("//*[text()"); // Should be: mydoc.SelectNodes("//*/*[text()");
            //System.Xml.XmlNodeList x = mydoc.SelectNodes("//*[contains(text(), 'foo')]"); 
            System.Xml.XmlNodeList x = mydoc.SelectNodes("//*[contains(translate(text(), 'ABCDEFGHIJKLMNOPQRSTUVWXYZÄÖÜÉÈÊÀÁÂÒÓÔÙÚÛÇÅÏÕÑŒ', 'abcdefghijklmnopqrstuvwxyzäöüéèêàáâòóôùúûçåïõñœ'),'foo')]"); // Should be: mydoc.SelectNodes("//*/*[text()");
            Console.WriteLine(x.Count);
            

            




            //Select the cd node with the matching title
            System.Xml.XmlElement root = doc.DocumentElement;
            System.Xml.XmlNamespaceManager nsmgr = ReportServerTools.GetReportNamespaceManager(doc);


            //System.Xml.XmlNodeList xxx = oldCd.SelectNodes("xpath");

            //System.Xml.XmlNodeList xxx = root.SelectNodes("//*/dft:*[text()=\"String\"]", nsmgr);
            System.Xml.XmlNodeList xxx = root.SelectNodes("//*/dft:*[text()]", nsmgr);
            Console.WriteLine(xxx.Count);
            


            System.Xml.XmlNode oldCd = root.SelectSingleNode("/dft:Report/dft:DataSources/dft:DataSource[@Name=\"COR_Basic\"]", nsmgr);


            // System.Xml.XmlNode RepParams = root.SelectSingleNode("/dft:Report/dft:ReportParameters", nsmgr);
            // System.Xml.XmlNodeList AllParams = RepParams.SelectNodes("//dft:ReportParameter", nsmgr);




            // http://stackoverflow.com/questions/3655549/xpath-containstext-some-string-doesnt-work-when-used-with-node-with-more
            //System.Xml.XmlNodeList AllParams = root.SelectNodes("/dft:Report/dft:ReportParameters/dft:ReportParameter", nsmgr);
            System.Xml.XmlNodeList AllParams = root.SelectNodes("/dft:Report/dft:ReportParameters/dft:ReportParameter/dft:Prompt[contains(text(),\"Liegenschaft\")]", nsmgr);
            Console.WriteLine(AllParams.Count);



            Console.WriteLine(" ----------------------------------------- ");


            foreach (System.Xml.XmlNode ThisParameter in AllParams)
            {
                // XmlAttribute a = doc.SelectSingleNode("/reply/@success");
                Console.WriteLine(ThisParameter.Attributes["Name"].Value);

                System.Xml.XmlNode ParamDataType = ThisParameter.SelectSingleNode("//dft:DataType", nsmgr);
                Console.WriteLine(ParamDataType.FirstChild.Value);
            } // Next ThisParameter

            System.Xml.XmlNodeList stichtage = root.SelectNodes("/dft:Report/dft:ReportParameters/dft:ReportParameter[@Name=\"in_stichtag\"]", nsmgr);
            System.Xml.XmlNode stichtag = root.SelectSingleNode("/dft:Report/dft:ReportParameters/dft:ReportParameter[@Name=\"in_stichtag\"]", nsmgr);

            System.Xml.XmlNode stichtagvalue = root.SelectSingleNode("/dft:Report/dft:ReportParameters/dft:ReportParameter[@Name=\"in_stichtag\"]/dft:DefaultValue/dft:Values/dft:Value", nsmgr);
            
            Console.WriteLine(stichtagvalue.FirstChild.Value);


            // /dft:Report/dft:ReportParameters/dft:ReportParameter[@Name="in_gebaeude"]



            System.Xml.XmlNode datasetname = root.SelectSingleNode("/dft:Report/dft:ReportParameters/dft:ReportParameter[@Name=\"in_gebaeude\"]/dft:DefaultValue/dft:DataSetReference/dft:DataSetName", nsmgr);
            string dataset = datasetname.FirstChild.Value;

            System.Xml.XmlNode dsn = root.SelectSingleNode("/dft:Report/dft:DataSets/dft:DataSet[@Name=\"SEL_Gebaeude\"]", nsmgr);
            
            
            System.Xml.XmlNode dsn3 = root.SelectSingleNode("/dft:Report/dft:DataSets/dft:DataSet[@Name=\"SEL_Gebaeude\"]/dft:Query", nsmgr);

            System.Xml.XmlNode commandtextnode = root.SelectSingleNode("/dft:Report/dft:DataSets/dft:DataSet[@Name=\"SEL_Gebaeude\"]/dft:Query/dft:CommandText", nsmgr);
            string commandText = commandtextnode.FirstChild.Value;
            Console.WriteLine(commandText);


            System.Xml.XmlNodeList AllDatasetParams = root.SelectNodes("/dft:Report/dft:DataSets/dft:DataSet[@Name=\"SEL_Gebaeude\"]/dft:Query/dft:QueryParameters/dft:QueryParameter", nsmgr);
            Console.WriteLine(AllDatasetParams.Count);

            Console.WriteLine(" ----------------------------------------- ");
            foreach (System.Xml.XmlNode DataSetParameter in AllDatasetParams)
            {
                string strName = DataSetParameter.Attributes["Name"].Value;
                string lala = DataSetParameter.FirstChild.FirstChild.Value;
                Console.WriteLine(lala);

                Console.WriteLine(strName);
            } // Next DataSetParameter


            System.Xml.XmlNode dsn5 = root.SelectSingleNode("/dft:Report/dft:DataSets/dft:DataSet[@Name=\"SEL_Gebaeude\"]/dft:Query/dft:QueryParameters", nsmgr);
            System.Xml.XmlNode dsn6 = root.SelectSingleNode("/dft:Report/dft:DataSets/dft:DataSet[@Name=\"SEL_Gebaeude\"]/dft:Query/dft:QueryParameters/dft:QueryParameter", nsmgr);


            System.Xml.XmlNodeList embeddedImages = root.SelectNodes("/dft:Report/dft:EmbeddedImages/dft:EmbeddedImage", nsmgr);


            System.Xml.XmlNode stao = root.SelectSingleNode("/dft:Report/dft:ReportParameters/dft:ReportParameter[@Name=\"in_standort\"]", nsmgr);
            if (stao != null)
                Console.WriteLine("Has stao");


            Console.WriteLine(stichtag);

            // /dft:Report/dft:PageHeader/dft:ReportItems/dft:Image/dft:Value[text()="=Convert.FromBase64String(Parameters!def_logo.Value)"]
            // /dft:Report/dft:PageHeader/dft:ReportItems/dft:Image/dft:Visibility/dft:Hidden[text()="=CBool(Parameters!def_HideLogo.Value)"]



            // System.Xml.XmlElement newCd = doc.CreateElement("cd");
            // newCd.SetAttribute("country", "country.Text");

            // newCd.InnerXml = "<title>" + this.comboBox1.Text + "</title>" + "<artist>" + artist.Text + "</artist>" + "<price>" + price.Text + "</price>";

            // root.ReplaceChild(newCd, oldCd);

            //save the output to a file
            //doc.Save(FILE_NAME);


            Console.WriteLine(Environment.NewLine);
            Console.WriteLine(" --- Press any key to continue --- ");
            Console.ReadKey();
        } // End Sub Main


    } // End Class Program


} // End Namespace ReportModifier
