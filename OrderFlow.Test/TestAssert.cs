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

    public static void IsType<T>(object? actual, string message)
    {
        if (actual is not T)
        {
            throw new InvalidOperationException(
                $"{message} Expected type: {typeof(T).Name}; actual type: {actual?.GetType().Name ?? "null"}.");
        }
    }

    public static void Null(object? actual, string message)
    {
        if (actual is not null)
        {
            throw new InvalidOperationException(message);
        }
    }    
}
