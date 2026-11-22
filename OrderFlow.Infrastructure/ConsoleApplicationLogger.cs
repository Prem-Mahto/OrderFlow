using OrderFlow.Application;

namespace OrderFlow.Infrastructure;

public sealed class ConsoleApplicationLogger : IApplicationLogger
{
    public void LogWarning(string message)
    {
        Console.WriteLine(message);
    }
}
