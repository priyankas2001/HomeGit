using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Utility
{
    public class Logging : ILogger
    {
        private string filePathName;
        private FileStream file;
        private StreamWriter writer;
        public Logging()
        {
            //filePathName = @"C:\Users\sourabhsingh01\Documents\dotnet_3192859_homeloan\Backend\HomeLoanBackend\Utility\LoggerFile.txt";
            //file = new FileStream(filePathName, FileMode.Create);
            //writer = new StreamWriter(file);
            
        }

        public void WriteInFile(string message)
        {            
            //writer.WriteLine(message);
            //writer.Close();
        }
        public void WriteInConsole(string message)
        {
            Console.WriteLine(message);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)    
        {
            throw new NotImplementedException();
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }
    }
}
