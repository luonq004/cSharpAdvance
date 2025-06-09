namespace SchoolHRAdministration
{
    class Program
    {
        delegate void LogDel(string mess);

        static void Main(string[] args)
        {
            Log log = new Log();

            //LogDel logDel = new LogDel(log.LogTextToFile
            //logDel("Linog");

            LogDel LogTextToScreenDel, LogTextToFileDel;
            LogTextToScreenDel = new LogDel(log.LogTextToScreen);

            LogText(LogTextToScreenDel, "Luong");

            Console.ReadKey();
        }

        static void LogText(LogDel logDel, string text)
        {
            logDel(text);
        }

       
    }

    public class Log
    {
        public void LogTextToScreen(string mess)
        {
            Console.WriteLine($"{DateTime.Now}: {mess}");
        }

        public void LogTextToFile(string mess)
        {
            using (StreamWriter sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log.txt"), true))
            {
                sw.WriteLine($"{DateTime.Now}: {mess}");
            }
        }
    }

}
