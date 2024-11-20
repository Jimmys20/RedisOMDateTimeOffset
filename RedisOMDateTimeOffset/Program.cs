using Redis.OM;

namespace RedisOMDateTimeOffset;

internal class Program
{
    static async Task Main(string[] args)
    {
        var redisConfigurationOptions = new StackExchange.Redis.ConfigurationOptions
        {
            EndPoints = { "192.168.100.4:6379" },
            User = null,
            Password = null,
            AllowAdmin = false,
            Ssl = false,
        };

        var provider = new RedisConnectionProvider(redisConfigurationOptions);

        provider.Connection.DropIndexAndAssociatedRecords(typeof(PlayerEntity));
        await provider.Connection.CreateIndexAsync(typeof(PlayerEntity));

        var players = provider.RedisCollection<PlayerEntity>();

        var uniquePlayerId = Guid.NewGuid().ToString();
        var startTime = new DateTimeOffset(2024, 11, 21, 12, 30, 00, new TimeSpan(1, 0, 0));

        await players.InsertAsync(new PlayerEntity
        {
            UniquePlayerId = uniquePlayerId,
            StartTime = startTime,
        });

        var player = await players.FindByIdAsync(uniquePlayerId);

        await players.SaveAsync();

        var player2 = await players.FindByIdAsync(uniquePlayerId);

        Console.WriteLine(player.StartTime);
        Console.WriteLine(player2.StartTime);
        Console.WriteLine(player.StartTime.Offset == player2.StartTime.Offset);
    }
}
