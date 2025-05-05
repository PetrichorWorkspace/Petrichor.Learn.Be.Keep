namespace Keep.Driven.NpgsqlPersistence;

public record NpgsqlPersistenceSettings
{
    public required string ConnectionString { get; init; }
}