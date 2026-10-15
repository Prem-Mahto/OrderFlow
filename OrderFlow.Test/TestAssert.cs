namespace OrderFlow.Test;

public static class TestAssert
{
    public static void Equal<T>(T expected, T actual, string message)
    {
        if (!EqualityComparer<T>.Default.Equals(expected, actual))
        {
            throw new InvalidOperationException($"{message} Expected: {expected}; actual: {actual}.");
        }
    }

    public static void Same(object expected, object? actual, string message)
    {
        if (!ReferenceEquals(expected, actual))
        {
            throw new InvalidOperationException(message);
        }
    }
}
