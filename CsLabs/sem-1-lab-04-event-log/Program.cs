using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

public class LogEntry
{
    public DateTime Timestamp;
    public string Level = "";
    public string Category = "";
    public string Message = "";
}

internal class Program
{
    public static void Main()
    {
        string path = Path.GetFullPath("event_server.log");

        if (!File.Exists(path))
        {
            Console.WriteLine("Файла нет");
            return;
        }

        string[] lines = File.ReadAllLines(path);
        LogEntry[] entries = ParseLog(lines);

        Console.WriteLine("Всего записей: " + entries.Length);
        Console.WriteLine(GetServerStatus(entries));
        Console.WriteLine("Info: " + CountByLevel(entries, "Info"));
        Console.WriteLine("Warning: " + CountByLevel(entries, "Warning"));
        Console.WriteLine("Error: " + CountByLevel(entries, "Error"));
        Console.WriteLine("Fatal: " + CountByLevel(entries, "Fatal"));
        Console.WriteLine("\nПредупреждения:");
        PrintLog(FilterByLevel(entries, "Warning"));
        Console.WriteLine("\nКатегория Combat:");
        PrintLog(FilterByCategory(entries, "Combat"));
        Console.WriteLine("\nПоиск слов «база данных»:");
        PrintLog(Search(entries, "база данных"));
    }

    public static LogEntry[] ParseLog(string[] lines)
    {
        LogEntry[] entries = new LogEntry[lines.Length];

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            LogEntry entry = new LogEntry();

            string dateTimeText = line.Substring(0, 23);
            DateTime dateTime = DateTime.Parse(dateTimeText);
            entry.Timestamp = dateTime;

            int firstOpenBracket = line.IndexOf('[');
            int firstCloseBracket = line.IndexOf(']', firstOpenBracket);
            int secondOpenBracket = line.IndexOf('[', firstCloseBracket + 1);
            int secondCloseBracket = line.IndexOf(']', secondOpenBracket);

            entry.Level = line.Substring(
                firstOpenBracket + 1,
                firstCloseBracket - firstOpenBracket - 1);

            entry.Category = line.Substring(
                secondOpenBracket + 1,
                secondCloseBracket - secondOpenBracket - 1);


            entry.Message = line.Substring(secondCloseBracket + 2);

            entries[i] = entry;
        }

        return entries;
    }

    public static LogEntry[] FilterByDate(LogEntry[] entries, DateTime date)
    {
        List<LogEntry> result = new List<LogEntry>();

        for (int i = 0; i < entries.Length; i++)
        {
            if (entries[i].Timestamp.Date == date.Date)
            {
                result.Add(entries[i]);
            }
        }

        return result.ToArray();
    }

    public static LogEntry[] FilterByLevel(LogEntry[] entries, string level)
    {
        List<LogEntry> result = new List<LogEntry>();

        for (int i = 0; i < entries.Length; i++)
        {
            if (entries[i].Level == level)
            {
                result.Add(entries[i]);
            }
        }

        return result.ToArray();
    }

    public static LogEntry[] FilterByCategory(LogEntry[] entries, string category)
    {
        List<LogEntry> result = new List<LogEntry>();

        for (int i = 0; i < entries.Length; i++)
        {
            if (entries[i].Category == category)
            {
                result.Add(entries[i]);
            }
        }

        return result.ToArray();
    }

    public static LogEntry[] Search(LogEntry[] entries, string text)
    {
        List<LogEntry> result = new List<LogEntry>();

        for (int i = 0; i < entries.Length; i++)
        {
            if (entries[i].Message.Contains(text, StringComparison.OrdinalIgnoreCase))
            {
                result.Add(entries[i]);
            }
        }

        return result.ToArray();
    }

    public static int CountByLevel(LogEntry[] entries, string level)
    {
        int count = 0;

        for (int i = 0; i < entries.Length; i++)
        {
            if (entries[i].Level == level)
            {
                count++;
            }
        }

        return count;
    }

    public static string GetServerStatus(LogEntry[] entries)
    {
        bool hasErrors = false;

        for (int i = 0; i < entries.Length; i++)
        {
            LogEntry entry = entries[i];

            if (entry.Level == "Fatal" && entry.Category == "Server")
            {
                return "КРИТИЧЕСКАЯ ОШИБКА: сервер остановлен";
            }

            if (entry.Level == "Error" || entry.Level == "Fatal")
            {
                hasErrors = true;
            }
        }
        if (hasErrors)
        {
            return "Есть ошибки: требуется проверка";
        }
        return "Сервер работает нормально";
    }
    public static void PrintLog(LogEntry[] entries)
    {
        if (entries.Length == 0)
        {
            Console.WriteLine("Записи не найдены.");
            return;
        }

        for (int i = 0; i < entries.Length; i++)
        {
            LogEntry entry = entries[i];

            Console.WriteLine(
                entry.Timestamp.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture)
                + " [" + entry.Level + "][" + entry.Category + "] "
                + entry.Message);
        }
    }
}
