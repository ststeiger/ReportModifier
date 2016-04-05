using System;
using System.Collections.Generic;
using System.Text;



//using ReportModifier.ReportingService_2005_Execution;
using ReportModifier.ReportingService_2005_Administration;


namespace ReportModifier.ReportServer
{


    class getparam
    {

        public static string strCredentialsURL = null; // rs.Url
        public static string strReportingServiceURL = "http://localhost/ReportServer_MS_SQL_2008/ReportService2005.asmx";
        public static int iTimeout = 15000;


        protected static System.Net.ICredentials GetMyCredentials(string strURL = null)
        {

            if (ReportServer.Settings.IntegratedSecurity)
            {
                return System.Net.CredentialCache.DefaultCredentials;
            }

            if (string.IsNullOrEmpty(ReportServer.Settings.Domain))
            {
                ReportServer.Settings.Domain = "localhost";
            }

            //Dim ncCustomCredentials As New System.Net.NetworkCredential("username", "password", "domainname")
            //Dim ncCustomCredentials As System.Net.NetworkCredential = New System.Net.NetworkCredential("Administrator", "lolipop", "mach-name")
            System.Net.NetworkCredential ncCustomCredentials = new System.Net.NetworkCredential(ReportServer.Settings.User, ReportServer.Settings.Password, ReportServer.Settings.Domain);
            System.Net.CredentialCache cache = new System.Net.CredentialCache();


            //Add a NetworkCredential instance to CredentialCache.
            //Negotiate for NTLM or Kerberos authentication.


            //if (string.IsNullOrEmpty(strURL))
            //{
            //    return System.Net.CredentialCache.DefaultCredentials;
            //}


            //Add a NetworkCredential instance to CredentialCache.
            //Negotiate for NTLM or Kerberos authentication.
            //cache.Add(New Uri(strURL), "Negotiate", ncCustomCredentials)
            //cache.Add(New Uri("http://hbdm0087/ReportServer/ReportService2005.asmx"), "Negotiate", ncCustomCredentials)
            cache.Add(new Uri(ReportServer.Settings.ReportingServiceURL), "Negotiate", ncCustomCredentials);
            //rs.Credentials = cache
            return cache;
        } // End Function GetMyCredentials


        // http://social.msdn.microsoft.com/Forums/en/sqlreportingservices/thread/361321ea-c227-48f0-b9bf-12869125ae42
        // http://www.codeproject.com/KB/reporting-services/SQLReportDeploy.aspx
        // COR.Reporting.ReportingServiceInterface.ChangeDataSource("Wincasa", "NewSource", "Wincasa", "RM_Bodenbelag")
        public static void GetParameters(string report)
        {
            //strDatasourceLocation = "Wincasa"
            //strDataSource = "NewSource"
            //strFolder = "Wincasa"
            //strReportName = "RM_Bodenbelag.rdl"
            // string strParent = "/";
            //If strFolder.StartsWith("/") Then
            //    strParent = ""
            //Else
            //    strParent = "/"
            //End If


            //If strReportName.EndsWith(".rdl") Then
            //    strReportName = strReportName.Remove(strReportName.Length - 4, 4)
            //End If


            SSRS.SSRS_2005_Administration_WithFOA rs = new SSRS.SSRS_2005_Administration_WithFOA();
            rs.Credentials = GetMyCredentials(strCredentialsURL);
            rs.Url = ReportServer.Settings.ReportingServiceURL;


            //Dim report As String = strParent + strFolder + "/" + strReportName
            bool forRendering = false;
            string historyID = null;
            ParameterValue[] values = null;
            DataSourceCredentials[] credentials = null;
            ReportParameter[] parameters = null;

            try
            {
                parameters = rs.GetReportParameters(report, historyID, forRendering, values, credentials);

                if ((parameters != null))
                {

                    // ReportParameter rp = default(ReportParameter);
                    foreach (ReportParameter rp in parameters)
                    {
                        // http://gallery.technet.microsoft.com/scriptcenter/42440a6b-c5b1-4acc-9632-d608d1c40a5c

                        // rp.Name
                        // rp.Type
                        // rp.Nullable
                        // rp.AllowBlank
                        // rp.MultiValue
                        ///'' used in query, rp.QueryParameter ???
                        //rp.Prompt
                        ///' dynamicprompt
                        // rp.PromptUser
                        // rp.State


                        Console.WriteLine("Name: {0}", rp.Name);
                    }
                }

            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                Console.WriteLine(ex.Detail.InnerXml.ToString());
            }

        } // End Sub GetParameters 


    } // End Class getparam


} // End Namespace ReportModifier.ReportServer
