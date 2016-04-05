
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;


namespace ReportModifier
{


    public class SSRS_2005_Execution_WithFOA : ReportingService_2005_Execution.ReportExecutionService
    {


        private string m_authCookieName;

        private System.Net.Cookie m_authCookie;


        // Public Shadows Property Url() As String
        public new string Url
        {
            get { return base.Url; }

            set
            {
                base.Url = value;
                try
                {
                    base.LogonUser("admin", ReportModifier.Cryptography.DES.DeCrypt("Crm+pCSGTwZsGKzw3xTa7A=="), null);
                }
                catch (Exception ex)
                {
                    DoNothing(ex);
                    // Console.WriteLine(ex.Message)
                }
            }
        } // End Property Url 


        public static void DoNothing(Exception ex)
        { }


        public SSRS_2005_Execution_WithFOA()
        {
        } // Constructor 


        public SSRS_2005_Execution_WithFOA(string strURL)
        {
            // Set the server URL. You can pull this from a config file or what ever way you want to make it dynamic.
            this.Url = strURL;

            // Calling the LogonUser method defined in the ReportService2005.asmx end point.
            // The LogonUser method authenticates the specified user to the Report Server Web Service when custom authentication has been configured.
            // This is to authenticate against the FBA code and then store the cookie for future reference.
        } // Constructor 


        /// <summary>
        /// Overriding the method defined in the base class.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        protected override System.Net.WebRequest GetWebRequest(Uri uri)
        {
            System.Net.HttpWebRequest request = default(System.Net.HttpWebRequest);
            request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(uri);
            request.Credentials = base.Credentials;
            request.CookieContainer = new System.Net.CookieContainer();

            if (m_authCookie != null)
            {
                request.CookieContainer.Add(m_authCookie);
            }

            return request;
        } // GetWebRequest 


        /// <summary>
        /// Overriding the method defined in the base class.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override System.Net.WebResponse GetWebResponse(System.Net.WebRequest request)
        {
            System.Net.WebResponse response = base.GetWebResponse(request);

            // http://social.msdn.microsoft.com/Forums/sqlserver/en-US/f68c3f2f-c498-4566-8ba4-ffd5070b8f7f/problem-with-ssrs-forms-authentication
            string cookieName = response.Headers["RSAuthenticationHeader"];
            if (cookieName != null)
            {
                m_authCookieName = cookieName;
                System.Net.HttpWebResponse webResponse = (System.Net.HttpWebResponse)response;
                System.Net.Cookie authCookie = webResponse.Cookies[cookieName];
                // save it for future reference and use.
                m_authCookie = authCookie;
            }

            return response;
        } // GetWebResponse 


    } // SSRS_2005_Execution_WithFOA 


}
