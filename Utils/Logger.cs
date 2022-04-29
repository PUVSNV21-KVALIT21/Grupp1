namespace hakims_livs.Utils;

public static class Logger
{

    public static void Log(string input)
    {
        string Name = DateTime.Now.ToString("yyyy-M-d dddd") + ".log";
        File.AppendAllText(Name, input);
    }
}
