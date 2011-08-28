using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Net.Mail;
using System.IO;

namespace Anecka.Labs
{
    public class Utility
    {
        public static void WriteErrorLog(Exception ex, string result)
        {
            // Create an EventLog instance and assign its source.   
            using (EventLog myLog = new EventLog())
            {
                myLog.Source = "ConvioConsole";

                // Write an informational entry to the event log.       
                if (ex != null)
                {
                    string msg = ex.Message + "\n" + "\n------\n\n" + result;
                    myLog.WriteEntry(msg, EventLogEntryType.Error);
                    //myLog.WriteEntry("\n\n------\n\n");
                }
                myLog.WriteEntry(result + "\n");  
            }
        }

        public static void CreateTextFile(string filePath, string buffer)
        {
            using (FileStream fsOut = new FileStream(filePath, FileMode.Create))
            {
                byte[] data;
                System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
                data = encoding.GetBytes(buffer);

                fsOut.Write(data, 0, data.Length);
            }
        }
    }
}
