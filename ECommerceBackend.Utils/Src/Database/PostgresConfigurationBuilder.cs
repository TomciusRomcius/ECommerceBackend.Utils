namespace ECommerceBackend.Utils.Database;

public class PostgresConfigurationBuilder
{
    public PostgresConfiguration Build(string[] args)
    {
        string? host = args.FirstOrDefault(arg => arg.StartsWith("--host"))?.Split("=")?[1];
        string? database = args.FirstOrDefault(arg => arg.StartsWith("--database"))?.Split("=")?[1];
        string? username = args.FirstOrDefault(arg => arg.StartsWith("--username"))?.Split("=")?[1];
        string? password = args.FirstOrDefault(arg => arg.StartsWith("--password"))?.Split("=")?[1];
        string port = args.FirstOrDefault(arg => arg.StartsWith("--port"))?.Split("=")?[1] ?? "5432";
        
        ArgumentException.ThrowIfNullOrWhiteSpace(host);
        ArgumentException.ThrowIfNullOrWhiteSpace(database);
        ArgumentException.ThrowIfNullOrWhiteSpace(username);
        ArgumentException.ThrowIfNullOrWhiteSpace(password);
        
        int portNum;
        if (!int.TryParse(port, out portNum))
        {
            throw new ArgumentException("Port must be an integer!");
        }

        return new PostgresConfiguration
        {
            Host = host,
            Database = database,
            Username = username,
            Password = password,
            Port = portNum,
        };
    }
}