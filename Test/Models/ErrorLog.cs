using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class ErrorLog
    {
        public void logError(System.Exception ex) {

            string filePath = @"C:\Error.txt";

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                   "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
            }

        }
    }
}