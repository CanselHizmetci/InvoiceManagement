using System;

namespace InvoiceManagement.Service.Services
{
    public class ConsoleLogger:ILoggerService
    {
        public void write(string message)
        {
            Console.WriteLine("[ConsoleLogger] - " + message);
        }
    }
}
