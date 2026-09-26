namespace Laba_02
{
    internal class Program
    {
        public static string CheckServersConfig(int MaxPlayers, int RamGB, bool Public, bool HasPassword,int CpuCors,bool Anticheat, int port, bool TechnicalWorks)
        {
        if (MaxPlayers <= 0)
            {
                return "Запуск невозможен: количество игроков должно быть больше нуля.";
            }
        if (RamGB < 3)
            {
                return "Запуск невозможен: недостаточно оперативной памяти.";
            }
        if (CpuCors <= 2)
            {
                return "Запуск невозможен: Количество ядер процессора должно быть больше 2";
            }
        if (port < 1024 || port > 65535)
            {
                return "Запуск невозможен: Указан недопустимый порт сервера.";
            }
        if (TechnicalWorks == true)
            {
                return "Запуск невозможен: Сервер находится на техническом обслуживании.";
            }
        if (Public &&  HasPassword)
            {
                return "Сервер можно запустить, но с уведомлением администратора: Публичный сервер защищён паролем.";
            }
        if (Public && (Anticheat == false))
            {
                return "Сервер можно запустить, но с уведомлением администратора: Публичный сервер работает без античита.";
            }
        if ((double)RamGB /  MaxPlayers < 0.10)
            {
                return "Сервер можно запустить, но с уведомлением администратора: Для такого количества игроков рекомендуется большее количество оперативной памяти.";
            }
        if ((double)(CpuCors) /  MaxPlayers < 0.01)
            {
                return "Сервер можно запустить, но с уведомлением администратора: Для такого количества игроков рекомендуется большее количество ядер процессора.";
            }
        else
            {
                return "Сервер готов к работе.";
            }
        }
        static void Main()
        {
            Console.WriteLine("Проверка работоспособности сервера.");

            Console.Write("Введите максимальное число игроков: ");
            string MaxPlayers = Console.ReadLine();
            int MaxPlayersNum = int.Parse(MaxPlayers);

            Console.Write("Введите количество оперативной памяти в гигабайтах: ");
            string RamGB = Console.ReadLine();
            int RamGBNum = int.Parse(RamGB);

            Console.Write("Сервер публичный? (ДА/НЕТ): ");
            string Public = Console.ReadLine();
            while (Public != "ДА" && Public != "НЕТ")
            {
                Console.Write("Ошибка. Введите ДА или НЕТ: ");
                Public = Console.ReadLine();
            }
            bool PublicBool = Public == "ДА";

            Console.Write("Сервер защищён паролем? (ДА/НЕТ): ");
            string HasPassword = Console.ReadLine();
            while (HasPassword != "ДА" && HasPassword != "НЕТ")
            {
                Console.Write("Ошибка. Введите ДА или НЕТ: ");
                HasPassword = Console.ReadLine();
            }
            bool HasPasswordBool = HasPassword == "ДА";

            Console.Write("Введите количество ядер процессора: ");
            string CpuCors = Console.ReadLine();
            int CpuCorsNum = int.Parse(CpuCors);

            Console.Write("Античит включён? (ДА/НЕТ): ");
            string Anticheat = Console.ReadLine();
            while (Anticheat != "ДА" && Anticheat != "НЕТ")
            {
                Console.Write("Ошибка. Введите ДА или НЕТ: ");
                Anticheat = Console.ReadLine();
            }
            bool AnticheatBool = Anticheat == "ДА";

            Console.Write("Введите порт сервера: ");
            string port = Console.ReadLine();
            int portNum = int.Parse(port);

            Console.Write("На сервере проводятся технические работы? (ДА/НЕТ): ");
            string TechnicalWorks = Console.ReadLine();
            while (TechnicalWorks != "ДА" && TechnicalWorks != "НЕТ")
            {
                Console.Write("Ошибка. Введите ДА или НЕТ: ");
                TechnicalWorks = Console.ReadLine();
            }
            bool TechnicalWorksBool = TechnicalWorks == "ДА";

            string result = CheckServersConfig(MaxPlayersNum,RamGBNum,PublicBool,HasPasswordBool,CpuCorsNum,AnticheatBool,portNum,TechnicalWorksBool);
            Console.WriteLine(result);
        }
    }
}
