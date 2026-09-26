using NUnit.Framework;

public class ProgramTests
{
    [Test]
    public void CheckServersConfig_ValidConfiguration_ServerIsReady()
    {
        var result = Program.CheckServersConfig(
            50,      // MaxPlayers
            8,       // RamGB
            false,   // Public
            false,   // HasPassword
            8,       // CpuCors
            true,    // Anticheat
            25565,   // port
            false    // TechnicalWorks
        );

        Assert.That(
            result,
            Is.EqualTo("Сервер готов к работе."));
    }

    [Test]
    public void CheckServersConfig_ZeroPlayers_LaunchIsImpossible()
    {
        var result = Program.CheckServersConfig(
            0,
            8,
            false,
            false,
            8,
            true,
            25565,
            false
        );

        Assert.That(
            result,
            Is.EqualTo(
                "Запуск невозможен: количество игроков должно быть больше нуля."));
    }

    [Test]
    public void CheckServersConfig_NotEnoughRam_LaunchIsImpossible()
    {
        var result = Program.CheckServersConfig(
            50,
            2,
            false,
            false,
            8,
            true,
            25565,
            false
        );

        Assert.That(
            result,
            Is.EqualTo(
                "Запуск невозможен: недостаточно оперативной памяти."));
    }

    [Test]
    public void CheckServersConfig_NotEnoughCpuCores_LaunchIsImpossible()
    {
        var result = Program.CheckServersConfig(
            50,
            8,
            false,
            false,
            2,
            true,
            25565,
            false
        );

        Assert.That(
            result,
            Is.EqualTo(
                "Запуск невозможен: Количество ядер процессора должно быть больше 2"));
    }

    [Test]
    public void CheckServersConfig_PortTooSmall_LaunchIsImpossible()
    {
        var result = Program.CheckServersConfig(
            50,
            8,
            false,
            false,
            8,
            true,
            1023,
            false
        );

        Assert.That(
            result,
            Is.EqualTo(
                "Запуск невозможен: Указан недопустимый порт сервера."));
    }

    [Test]
    public void CheckServersConfig_PortTooLarge_LaunchIsImpossible()
    {
        var result = Program.CheckServersConfig(
            50,
            8,
            false,
            false,
            8,
            true,
            65536,
            false
        );

        Assert.That(
            result,
            Is.EqualTo(
                "Запуск невозможен: Указан недопустимый порт сервера."));
    }

    [Test]
    public void CheckServersConfig_TechnicalWorks_LaunchIsImpossible()
    {
        var result = Program.CheckServersConfig(
            50,
            8,
            false,
            false,
            8,
            true,
            25565,
            true
        );

        Assert.That(
            result,
            Is.EqualTo(
                "Запуск невозможен: Сервер находится на техническом обслуживании."));
    }

    [Test]
    public void CheckServersConfig_PublicServerWithPassword_LaunchWithWarning()
    {
        var result = Program.CheckServersConfig(
            50,
            8,
            true,
            true,
            8,
            true,
            25565,
            false
        );

        Assert.That(
            result,
            Is.EqualTo(
                "Сервер можно запустить, но с уведомлением администратора: Публичный сервер защищён паролем."));
    }

    [Test]
    public void CheckServersConfig_PublicServerWithoutAnticheat_LaunchWithWarning()
    {
        var result = Program.CheckServersConfig(
            50,
            8,
            true,
            false,
            8,
            false,
            25565,
            false
        );

        Assert.That(
            result,
            Is.EqualTo(
                "Сервер можно запустить, но с уведомлением администратора: Публичный сервер работает без античита."));
    }

    [Test]
    public void CheckServersConfig_NotEnoughRamForPlayers_LaunchWithWarning()
    {
        var result = Program.CheckServersConfig(
            100,
            5,
            false,
            false,
            8,
            true,
            25565,
            false
        );

        Assert.That(
            result,
            Is.EqualTo(
                "Сервер можно запустить, но с уведомлением администратора: Для такого количества игроков рекомендуется большее количество оперативной памяти."));
    }

    [Test]
    public void CheckServersConfig_NotEnoughCpuForPlayers_LaunchWithWarning()
    {
        var result = Program.CheckServersConfig(
            301,
            16,
            false,
            false,
            3,
            true,
            25565,
            false
        );

        Assert.That(
            result,
            Is.EqualTo(
                "Сервер можно запустить, но с уведомлением администратора: Для такого количества игроков рекомендуется большее количество ядер процессора."));
    }

    [Test]
    public void CheckServersConfig_MinimumValidPort_ServerIsReady()
    {
        var result = Program.CheckServersConfig(
            50,
            8,
            false,
            false,
            8,
            true,
            1024,
            false
        );

        Assert.That(
            result,
            Is.EqualTo("Сервер готов к работе."));
    }

    [Test]
    public void CheckServersConfig_MaximumValidPort_ServerIsReady()
    {
        var result = Program.CheckServersConfig(
            50,
            8,
            false,
            false,
            8,
            true,
            65535,
            false
        );

        Assert.That(
            result,
            Is.EqualTo("Сервер готов к работе."));
    }
}