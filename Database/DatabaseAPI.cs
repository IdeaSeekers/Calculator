using Domain;
using Npgsql;

namespace Database;

public class DatabaseAPI
{
    private NpgsqlConnection conn;
    private DatabaseHistoryRepository historyRepo;
    private DatabaseUsersRepository usersRepo;
    
    public DatabaseAPI(String databaseUser, String password, String databaseName)
    {
        String connectionString = "Server=127.0.0.1;" +
                                  "User Id=" + databaseUser + ";" +
                                  "Password=" + password + ";" +
                                  "Database=" + databaseName + ";";
        conn = new NpgsqlConnection(connectionString);
        conn.Open();

        historyRepo = new DatabaseHistoryRepository(conn);
        usersRepo = new DatabaseUsersRepository(conn);
    }
    
    public UpdateHistoryResult UpdateHistory(CalculationData calculationData, UserInfo userInfo)
    {
        throw new NotImplementedException("Not implemented!");
    }

    public GetHistoryResult GetHistory(UserInfo userInfo)
    {
        throw new NotImplementedException("Not implemented!");
    }
    
    public RegisterUserDatabaseResult RegisterUser(User user)
    {
        throw new NotImplementedException("Not implemented!");
    }
    
    public GetUserResult GetUser(UserInfo user)
    {
        throw new NotImplementedException("Not implemented!");
    }

}