
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace ReportModifier
{


    static class OldCrap
    {


        public static void ReadUsingReportViewer(string FILE_NAME)
        {
            Microsoft.Reporting.WebForms.ReportViewer ReportViewer1 = new Microsoft.Reporting.WebForms.ReportViewer();
            // ReportViewer1.LocalReport.ReportPath = "GM_Gebaeudestammdaten_Wincasa.rdl";
            ReportViewer1.LocalReport.ReportPath = FILE_NAME;

            // Console.WriteLine(ReportViewer1.LocalReport.ReportEmbeddedResource);

            Microsoft.Reporting.WebForms.ReportParameterInfoCollection x = ReportViewer1.LocalReport.GetParameters();

            foreach (Microsoft.Reporting.WebForms.ReportParameterInfo pi in x)
            {
                Console.WriteLine(pi.Name);
            } // Next pi

            Microsoft.Reporting.WebForms.ReportDataSourceCollection dsc = ReportViewer1.LocalReport.DataSources;

            foreach (Microsoft.Reporting.WebForms.ReportDataSource rds in dsc)
            {
                Console.WriteLine(rds.Name);
                Console.WriteLine(rds.Value);
            } // Next rds

        } // End Sub ReadUsingReportViewer



        public static void ExtractXMLQuireIcon()
        {
            System.Drawing.Icon ico = System.Drawing.Icon.ExtractAssociatedIcon(@"D:\Stefan.Steiger\Desktop\Quire\XMLQuire.exe");

            using (System.IO.Stream strm = new System.IO.FileStream(@"D:\Stefan.Steiger\Desktop\Quire\myicon.ico", System.IO.FileMode.OpenOrCreate))
            {
                ico.Save(strm);
                strm.Flush();
                strm.Close();
            } // End Using strm

        } // End Sub ExtractXMLQuireIcon


        /*
        public static void ChangeStandort(string strFilename)
        {
            System.Xml.XmlDocument doc = File2XmlDocument(strFilename);
            System.Xml.XmlNamespaceManager nsmgr = GetReportNamespaceManager(doc);

            if (!HasParameter(doc, "in_standort"))
                return;


            System.Xml.XmlNode xnStandortPrompt = GetParameterPrompt(doc, "in_standort");
            string strReportName = System.IO.Path.GetFileNameWithoutExtension(strFilename);

            if (xnStandortPrompt != null)
            {
                xnStandortPrompt.FirstChild.Value = "Liegenschaft / Immeuble / Patrimonio immobiliare / Estate";
                string strStandort = xnStandortPrompt.FirstChild.Value;
                LogMessage("{0}\t{1}", strReportName, strStandort);
            }
            else
                LogMessage("{0}\tKein Parameter in_standort", strReportName);

            SaveDocument(doc, strFilename);
        } // End Sub ChangeStandort


        public static void ChangeBuilding(string strFilename)
        {
            System.Xml.XmlDocument doc = File2XmlDocument(strFilename);
            System.Xml.XmlNamespaceManager nsmgr = GetReportNamespaceManager(doc);

            if (!HasParameter(doc, "in_gebaeude"))
                return;


            System.Xml.XmlNode xnBuilding = GetParameterPrompt(doc, "in_gebaeude");
            string strReportName = System.IO.Path.GetFileNameWithoutExtension(strFilename);

            if (xnBuilding != null)
            {
                xnBuilding.FirstChild.Value = "Gebäude / Bâtiment / Edificio / Building";
                string strStandort = xnBuilding.FirstChild.Value;
                LogMessage("{0}\t{1}", strReportName, strStandort);
            }
            else
                LogMessage("{0}\tKein Parameter in_gebaeude", strReportName);

            SaveDocument(doc, strFilename);
        } // End Sub ChangeBuilding
        */

    } // End Class OldCrap


} // End Namespace ReportModifier
