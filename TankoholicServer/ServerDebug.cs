using Riptide.Utils;

namespace TankoholicServer
{
    internal class ServerDebug
    {
        public static bool DebugMessages = true;
        public static bool DebugPosition = false;

        public static void Log(string message)
        {
            if (!DebugMessages) return;

            Console.ForegroundColor = ConsoleColor.White;
            RiptideLogger.Log(LogType.Debug, message);
        }

        public static void Info(string message)
        {
            if (!DebugMessages) return;

            Console.ForegroundColor = ConsoleColor.Green;
            RiptideLogger.Log(LogType.Info, message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Warn(string message)
        {
            if (!DebugMessages) return;

            Console.ForegroundColor = ConsoleColor.Yellow;
            RiptideLogger.Log(LogType.Warning, message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Error(string message)
        {
            if (!DebugMessages) return;

            Console.ForegroundColor = ConsoleColor.Red;
            RiptideLogger.Log(LogType.Error, message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
