using System;

namespace DotnetCli.Extensions.Utils
{
    public class ConsoleWriter
    {
        public void WriteWarning(string message)
        {
            WriteMessage(message, ConsoleColor.Yellow);
        }
        public void WriteError(string message)
        {
            WriteMessage(message, ConsoleColor.Red);
        }
        public void WriteSuccess(string message)
        {
            WriteMessage(message, ConsoleColor.Green);
        }
        
        private void WriteMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
