using Npgsql;

namespace DashBored.Api.Helpers
{
    public static class ConnectionHelper
    {
        public static string GetConnectionString()
        {
            var databaseUrl = Environment.GetEnvironmentVariable("Postgres.DATABASE_URL");

            if (string.IsNullOrEmpty(databaseUrl))
            {
                throw new InvalidOperationException("The Postgres.DATABASE_URL environment variable is not set.");
            }

            var uri = new Uri(databaseUrl);
            var userInfo = uri.UserInfo.Split(':');

            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = uri.Host,
                Port = uri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = uri.LocalPath.TrimStart('/'),
                SslMode = SslMode.Require
            };

            return builder.ConnectionString;
        }
    }
}
