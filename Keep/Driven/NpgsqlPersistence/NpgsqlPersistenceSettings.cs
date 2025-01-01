namespace Keep.Driven.NpgsqlPersistence;

public record NpgsqlPersistenceSettings(string ConnectionString)
{
    public required string ConnectionString { get; init; } = ConnectionString;
}