using Domain;
using Npgsql;

namespace Database;

public class DatabaseAPI
{
    private class UsersTableInfo
    {
        public struct UserDatabaseRow
        {
            public Int32 id;
            public String login;
            public String password;
        }
        
        private String usersTableName = "Users";
        private String usersIdColumn = "id";
        private String usersLoginColumn = "login";
        private String usersPasswordColumn = "password";
    }

    private class HistoryTableInfo
    {
        private struct HistoryDatabaseRow
        {
            public Int32 id;
            public String query;
            public Boolean valid;
            public Double result;
        }
        
        private String historyTableName = "Calculations";
        private String historyIdColumn = "id";
        private String historyQueryColumn = "query";
        private String historyValidColumn = "valid";
        private String historyResultColumn = "result";
    }

    private NpgsqlConnection conn;
    private UsersTableInfo usersTableInfo;
    private HistoryTableInfo historyTableInfo;
    

    public DatabaseAPI(String databaseUser, String password, String databaseName)
    {
        String connectionString = "Server=127.0.0.1;" +
                                  "User Id=" + databaseUser + ";" +
                                  "Password=" + password + ";" +
                                  "Database=" + databaseName + ";";
        conn = new NpgsqlConnection(connectionString);
        conn.Open();
    }
    
    public UpdateHistoryResult UpdateHistory(CalculationResult calculationResult, UserInfo userInfo)
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