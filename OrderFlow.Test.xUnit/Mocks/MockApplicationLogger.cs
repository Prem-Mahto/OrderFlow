using OrderFlow.Application;

namespace OrderFlow.Test.xUnit.Mocks;

public sealed class MockApplicationLogger : IApplicationLogger
{
    public List<string> Warnings { get; } = [];

    public void LogWarning(string message)
    {
        Warnings.Add(message);
    }
}
