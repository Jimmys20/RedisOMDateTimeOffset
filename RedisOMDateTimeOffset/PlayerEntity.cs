using Redis.OM.Modeling;

namespace RedisOMDateTimeOffset;

[Document(StorageType = StorageType.Json, Prefixes = ["Player"])]
public class PlayerEntity
{
    [RedisIdField, Indexed] public string UniquePlayerId { get; set; }
    public DateTimeOffset StartTime { get; set; }
}
