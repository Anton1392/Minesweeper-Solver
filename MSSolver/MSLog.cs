using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MSSolver
{
    public static class MSLog
    {
        // List of log-messages. This compiles and saves into a .txt file later on.
        public static List<string> Messages = new List<string>();

        /// <summary>
        /// Adds a message to the log.
        /// </summary>
        /// <param name="msg">The message to add</param>
        public static void AddMessage(string msg)
        {
            // Adds a message to the log.
            Messages.Add(msg);
        }

        /// <summary>
        /// Saves the log to C:\\MSSolverLogs\.
        /// </summary>
        public static void SaveLog()
        {
            // Creates a directory if it does not exist.
            if (!Directory.Exists(@"C:\MSSolverLogs\"))
            {
                Directory.CreateDirectory(@"C:\MSSolverLogs\");
            }

            // Writes all lines of the Messages list into a text file.
            File.WriteAllLines(@"C:\MSSolverLogs\MSLog" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".txt", Messages);
        }

        /// <summary>
        /// Clears the log, allowing for multiple separate uses.
        /// </summary>
        public static void ClearLog()
        {
            Messages.Clear();
        }
    }
}
