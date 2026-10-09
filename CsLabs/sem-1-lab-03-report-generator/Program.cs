using Microsoft.VisualBasic;
using System.ComponentModel;
using System.Diagnostics.Tracing;
using System.Drawing;

namespace sem_1_lab_03_report_generator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = (Path.GetFullPath("event_server.log"));
            string[] lines = File.ReadAllLines(path);
            bool EventRunning = false;
            int EventStartIndex = 0;
            int EventEndIndex = 0;
            int ErrorsCounter = 0;
            int WarningsCounter = 0;
            int EventWinPoints = 0;
            int EventLoosPoints = 0;
            string EventLoot = "";
            string Winner = "";
            while (EventRunning == false)
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    int FisrtStr = line.IndexOf("]");
                    int StrNeed = line.IndexOf("]", FisrtStr + 1) + 2;
                    string message = line.Substring(StrNeed);
                    if (message.IndexOf("Событие началось: Восстание Ледяного Пламени") != -1)
                    {
                        EventRunning = true;
                        EventStartIndex = i;
                    }
                }
            }
            while (EventEndIndex == 0)
            {
                for (int i = EventStartIndex; i < lines.Length; i++)
                {
                    string line = lines[i];
                    int FisrtStr = line.IndexOf("[");
                    string Inf = line.Substring(FisrtStr + 1, 4);
                    if (Inf == "Warn")
                    {
                        WarningsCounter++;
                        continue;
                    }
                    if (Inf == "Erro")
                    {
                        ErrorsCounter++;
                        continue;
                    }
                    if (line.IndexOf("объявлены победителями события") != -1)
                    {
                        int WorkIndex = line.IndexOf("]");
                        int TrueWorkIndex = line.IndexOf("]", WorkIndex + 1) + 2;
                        Winner = line.Substring(TrueWorkIndex, line.IndexOf("объявлены победителями события") - TrueWorkIndex - 1);
                    }
                    if (line.IndexOf("получили ивентовый предмет:") != -1)
                    {
                        int WorkIndex = line.IndexOf("предмет:");
                        EventLoot = line.Substring(WorkIndex + 9);
                    }
                    if (line.IndexOf("очков события") != -1 && line.IndexOf("утешительную награду") == -1)
                    {
                        int WorkIndexStart = line.IndexOf("получили") + 9;
                        int WorkIndexEnd = line.IndexOf(" ", WorkIndexStart + 1);
                        string WinPoints = line.Substring(WorkIndexStart, WorkIndexEnd - WorkIndexStart);
                        EventWinPoints = Convert.ToInt32(WinPoints);
                    }
                    if (line.IndexOf("утешительную награду:") != -1)
                    {
                        int WorkIndexStart = line.IndexOf("награду:") + 9;
                        int WorkIndexEnd = line.IndexOf(" ", WorkIndexStart + 1);
                        string LoosPoints = line.Substring(WorkIndexStart, WorkIndexEnd - WorkIndexStart);
                        EventLoosPoints = Convert.ToInt32(LoosPoints);
                    }
                    if (line.IndexOf("Событие \"Восстание Ледяного Пламени\" закрыто") != -1)
                    {
                        EventEndIndex = 1;
                        break;
                    }
                }
            }
            string StartLine = lines[EventStartIndex];
            DateOnly EventDateOnly = new DateOnly(Convert.ToInt32(StartLine.Substring(0, 4)), Convert.ToInt32(StartLine.Substring(5, 2)), Convert.ToInt32(StartLine.Substring(8, 2)));
            Console.WriteLine("Итоги события: Восстание Ледяного Пламени");
            Console.WriteLine($"Дата: {EventDateOnly}");
            Console.WriteLine($"Победитель: {Winner}");
            Console.WriteLine($"Очки победителя: {EventWinPoints}");
            Console.WriteLine($"Ивентовый предмет: {EventLoot}");
            Console.WriteLine($"Утешительная награда Железных волков: {EventLoosPoints} очков");
            Console.WriteLine($"Предупреждений во время события: {WarningsCounter}");
            Console.WriteLine($"Ошибок во время события: {ErrorsCounter}");
        }
    }
}
