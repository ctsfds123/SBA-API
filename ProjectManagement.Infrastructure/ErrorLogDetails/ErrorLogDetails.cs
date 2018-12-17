using System;
using System.IO;
using System.Web;

namespace ProjecManagement.ErrorHandler
{
    public class ErrorLogDetails : IErrorLog
    {
        private object locker = new object();
        private enum LogType
        {
            Info = 0,
            Error = 1
        }

        public Guid ErrorLogException(Exception ex)
        {
            return ErrorLogstr(ex.ToString());
        }

        public Guid ErrorLogstr(string message)
        {
            var guid = Guid.NewGuid();
            var msg = string.Format("Reference Id: {0}, Details {1}", guid, message);
            Write(LogType.Error, msg);

            return guid;
        }

        public void Informations(string message)
        {
            Write(LogType.Info, message);
        }

        public void InformationsFormat(string format, params object[] values)
        {
            var message = string.Format(format, values);
            Write(LogType.Error, message);
        }

        private void Write(LogType logType, string message)
        {
            var dir = GetLogDirectory();
            var fileName = GetFileName();
            var fullName = Path.Combine(dir, fileName);

            try
            {
                lock (locker)
                {
                    using (var writer = File.AppendText(fullName))
                    {
                        writer.WriteLine("{0} {1} {2}", DateTime.Now, logType.ToString().ToUpperInvariant(), message);
                        writer.WriteLine();
                    }
                }
            }
            catch (Exception)
            {
                // There was a problem in writing log
                throw;
            }
        }

        private string GetLogDirectory()
        {
            var logDir = Path.Combine(HttpRuntime.AppDomainAppPath, "Logging");

            if (!Directory.Exists(logDir))
                Directory.CreateDirectory(logDir);

            return logDir;
        }

        private string GetFileName()
        {
            return string.Format("Logging-{0}.txt", DateTime.Today.ToString("MM-dd-yyyy"));
        }
    }
}
