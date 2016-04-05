
namespace ReportModifier
{


    class Logging
    {

        public static string strLogPath = GetLogFilePath();


        public static string GetLogFilePath()
        {
            string strLogfileLocation = System.IO.Path.Combine(ReportServerTools.GetSavePath(), "log.txt");

            if (System.IO.File.Exists(strLogfileLocation))
                System.IO.File.Delete(strLogfileLocation);

            return strLogfileLocation;
        } // End Function GetLogFilePath


        public static void LogMessage(string strMessage)
        {
            System.Console.WriteLine(strMessage);
            System.IO.File.AppendAllText(strLogPath, strMessage + System.Environment.NewLine, System.Text.Encoding.UTF8);
        } // End Sub LogMessage


        public static void LogMessage(string str, params object[] args)
        {
            LogMessage(string.Format(str, args));
        } // End Sub LogMessage


    }


}
