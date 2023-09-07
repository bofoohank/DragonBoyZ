using System;
using DragonBoyZ.Application.IO;
using DragonBoyZ.DatabaseManager;
using Serilog;
using Serilog.Events;
using DragonBoyZ.Application.Main;

namespace DragonBoyZ.Logging
{
    public class ServerLogger : IServerLogger
    {
        private readonly ILogger _logger;

        public ServerLogger()
        {
            _logger = new LoggerConfiguration()
                .WriteTo.Console(outputTemplate: "{Message:lj}{NewLine}{Exception}")
                .WriteTo.File("logging/log-.txt", LogEventLevel.Error, rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public void Debug(string message)
        {
            if(ConfigManager.gI().IsDebug) _logger.Information($"DEBUG ==> {message}");
        }
        public void PrintError(int setId,string message)
        {
            if (DragonBall.findErr) ServerUtils.WriteLog("login/"+setId,$"Find Error ==> {message}");
        }
        public void DebugColor(string message, string color)
        {
            if (ConfigManager.gI().IsDebug)
            {
                if (color == "black") Console.ForegroundColor = ConsoleColor.Black;
                if (color == "red") Console.ForegroundColor = ConsoleColor.Red;
                if (color == "darkred") Console.ForegroundColor = ConsoleColor.DarkRed;
                if (color == "blue") Console.ForegroundColor = ConsoleColor.Blue;
                if (color == "cyan") Console.ForegroundColor = ConsoleColor.Cyan;
                if (color == "yellow") Console.ForegroundColor = ConsoleColor.Yellow;

                if (color == "green") Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine(message);
                Console.ResetColor();
            }
        }
        public void PrintColor(string message, string color)
        {
            if (color == "black") Console.ForegroundColor = ConsoleColor.Black;
            if (color == "red") Console.ForegroundColor = ConsoleColor.Red;
            if (color == "darkred") Console.ForegroundColor = ConsoleColor.DarkRed;
            if (color == "blue") Console.ForegroundColor = ConsoleColor.Blue;
            if (color == "cyan") Console.ForegroundColor = ConsoleColor.Cyan;
            if (color == "yellow") Console.ForegroundColor = ConsoleColor.Yellow;
            if (color == "green") Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(message);
            Console.ResetColor();
        }
        public void Print(string message, string color)
        {
            if (color == "black") Console.ForegroundColor = ConsoleColor.Black;
            if (color == "red") Console.ForegroundColor = ConsoleColor.Red;
            if (color == "darkred") Console.ForegroundColor = ConsoleColor.DarkRed;
            if (color == "blue") Console.ForegroundColor = ConsoleColor.Blue;
            if (color == "cyan") Console.ForegroundColor = ConsoleColor.Cyan;
            if (color == "yellow") Console.ForegroundColor = ConsoleColor.Yellow;
            if (color == "manager") Console.ForegroundColor = ConsoleColor.DarkMagenta;

            if (color == "green") Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(message);
            Console.ResetColor();
        }
		public void SvPrint(string message, string color)
		{
    		if (color == "black") Console.ForegroundColor = ConsoleColor.Black;
    		else if (color == "red") Console.ForegroundColor = ConsoleColor.Red;
    		else if (color == "darkred") Console.ForegroundColor = ConsoleColor.DarkRed;
    		else if (color == "blue") Console.ForegroundColor = ConsoleColor.Blue;
    		else if (color == "cyan") Console.ForegroundColor = ConsoleColor.Cyan;
    		else if (color == "yellow") Console.ForegroundColor = ConsoleColor.Yellow;
    		else if (color == "manager") Console.ForegroundColor = ConsoleColor.DarkMagenta;
    		else if (color == "green") Console.ForegroundColor = ConsoleColor.Green;

    		Console.Write("[Server]  ");
			Console.ResetColor();
			
			Console.ForegroundColor = ConsoleColor.White;
    		Console.WriteLine(message);
    		Console.ResetColor();
		}
		public void PrtColor(string color1, string message1, string color2, string message2)
		{
    		if (color1 == "black") Console.ForegroundColor = ConsoleColor.Black;
			else if (color1 == "white") Console.ForegroundColor = ConsoleColor.White;
    		else if (color1 == "red") Console.ForegroundColor = ConsoleColor.Red;
    		else if (color1 == "darkred") Console.ForegroundColor = ConsoleColor.DarkRed;
    		else if (color1 == "blue") Console.ForegroundColor = ConsoleColor.Blue;
    		else if (color1 == "cyan") Console.ForegroundColor = ConsoleColor.Cyan;
    		else if (color1 == "yellow") Console.ForegroundColor = ConsoleColor.Yellow;
    		else if (color1 == "manager") Console.ForegroundColor = ConsoleColor.DarkMagenta;
			else if (color1 == "magenta") Console.ForegroundColor = ConsoleColor.Magenta;
			else if (color1 == "gray") Console.ForegroundColor = ConsoleColor.Gray;
    		else if (color1 == "green") Console.ForegroundColor = ConsoleColor.Green;

    		Console.Write("[" + message1 + "]   ");
			Console.ResetColor();
			
			if (color2 == "black") Console.ForegroundColor = ConsoleColor.Black;
			else if (color2 == "white") Console.ForegroundColor = ConsoleColor.White;
    		else if (color2 == "red") Console.ForegroundColor = ConsoleColor.Red;
    		else if (color2 == "darkred") Console.ForegroundColor = ConsoleColor.DarkRed;
    		else if (color2 == "blue") Console.ForegroundColor = ConsoleColor.Blue;
			else if (color2 == "darkblue") Console.ForegroundColor = ConsoleColor.DarkBlue;
    		else if (color2 == "cyan") Console.ForegroundColor = ConsoleColor.Cyan;
			else if (color2 == "darkcyan") Console.ForegroundColor = ConsoleColor.DarkCyan;
    		else if (color2 == "yellow") Console.ForegroundColor = ConsoleColor.Yellow;
			else if (color2 == "darkyellow") Console.ForegroundColor = ConsoleColor.DarkYellow;
    		else if (color2 == "manager") Console.ForegroundColor = ConsoleColor.DarkMagenta;
			else if (color2 == "magenta") Console.ForegroundColor = ConsoleColor.Magenta;
			else if (color2 == "gray") Console.ForegroundColor = ConsoleColor.Gray;
    		else if (color2 == "green") Console.ForegroundColor = ConsoleColor.Green;
			else if (color2 == "darkgreen") Console.ForegroundColor = ConsoleColor.DarkGreen;
    		Console.WriteLine(message2);
    		Console.ResetColor();
		}

        public void PrintColorWithBackgroundColor(string message, string color, string bgColor)
        {
            if (color == "black") Console.ForegroundColor = ConsoleColor.Black;
            if (color == "red") Console.ForegroundColor = ConsoleColor.Red;
            if (color == "darkred") Console.ForegroundColor = ConsoleColor.DarkRed;
            if (color == "blue") Console.ForegroundColor = ConsoleColor.Blue;
            if (color == "cyan") Console.ForegroundColor = ConsoleColor.Cyan;
            if (color == "yellow") Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine(message);
            Console.ResetColor();
        }
        //public void Print(string message)
        //{
        //    _logger.Information($"==> " + ServerUtils.TimeNow() + "| "+ message);
        //}
        public void Print(string message)
        {
            _logger.Information(message);
        }
        public void Info(string info)
        {
            _logger.Information(info);
        }

        public void Warning(string message, Exception exception = null)
        {
            _logger.Warning(message, exception);
        }

        public void Error(string message, Exception exception)
        {
            _logger.Error(message, exception);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }
    }
}