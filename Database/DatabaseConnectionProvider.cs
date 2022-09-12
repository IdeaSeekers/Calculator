using Npgsql;

namespace Database;

public class DatabaseConnectionProvider
{
    private String connectionString;

    public DatabaseConnectionProvider(String databaseUser, String password, String databaseName)
    {
        connectionString = "Server=127.0.0.1;" +
                           "User Id=" + databaseUser + ";" +
                           "Password=" + password + ";" +
                           "Database=" + databaseName + ";";
    }

    public NpgsqlConnection GetConnection()
    {
        NpgsqlConnection conn = new NpgsqlConnection(connectionString);
        conn.Open();
        return conn;
    }
}