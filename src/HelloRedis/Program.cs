using StackExchange.Redis;
using System.Text.Json;

Console.WriteLine("Run this command to start redis in docker:");
Console.WriteLine("docker run --name my-redis -p 6379:6379 -d redis");

// Connection to Redis server - make sure it's running in Docker!
var redis = ConnectionMultiplexer.Connect("localhost");
var db = redis.GetDatabase();

var user = new User { Id = 1, Name = "John Doe", Email = "johndoe@example.com" };

// Serialize the User object to a JSON string.
string userJson = JsonSerializer.Serialize(user);

// Store the serialized User object in Redis with the key "user:1".
await db.StringSetAsync("user:1", userJson);

Console.WriteLine("User object stored in Redis.");

// Fetch the serialized User object from Redis.
string fetchedUserJson = await db.StringGetAsync("user:1");

// Deserialize the JSON string back to a User object.
var fetchedUser = JsonSerializer.Deserialize<User>(fetchedUserJson);

Console.WriteLine($"Fetched User: {fetchedUser.Name}, Email: {fetchedUser.Email}");

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("If you got this far and you see the user name and email above, it worked!");
Console.ForegroundColor = ConsoleColor.White;
