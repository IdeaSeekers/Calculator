using Domain;

namespace Database;
using Npgsql;

public struct UserDatabaseRow
{
    public Int32 Id;
    public String Login;
    public String Password;
}

public class DatabaseUsersRepository
{
    private DatabaseConnectionProvider _connectionProvider;
    
    private String _usersTableName = "Users";
    private String _usersIdColumn = "id";
    private String _usersLoginColumn = "login";
    private String _usersPasswordColumn = "password";

    private String GetSelectQueryString(String login)
    {
        return String.Format(
            "SELECT {1}, {2}, {3} FROM {0} WHERE {2} = '{4}'",
            _usersTableName, _usersIdColumn, _usersLoginColumn, _usersPasswordColumn, login
        );
    }
    
    private String GetInsertQueryString(String login, String password)
    {
        return String.Format("INSERT INTO {0} ({1}, {2}) VALUES ('{3}', '{4}')",
            _usersTableName, _usersLoginColumn, _usersPasswordColumn,
            login, password);
    }
    
    public DatabaseUsersRepository(String databaseUser, String password, String databaseName)
    {
        _connectionProvider = new DatabaseConnectionProvider(databaseUser, password, databaseName);
    }
    
    public UserDatabaseRow Get(UserInfo key)
    {
        UserDatabaseRow user = new UserDatabaseRow();
        String selectUserQueryString = GetSelectQueryString(key.Login.Data);

        NpgsqlCommand selectIdCommand = new NpgsqlCommand(selectUserQueryString, _connectionProvider.GetConnection());
        NpgsqlDataReader reader = selectIdCommand.ExecuteReader();

        if (reader.HasRows)
        {
            int idColumn = 0, loginColumn = 1, passwordColumn = 2;
            reader.Read();
            user.Id = reader.GetInt32(idColumn);
            user.Login = reader.GetString(loginColumn);
            user.Password = reader.GetString(passwordColumn);
        }
        else
        {
            throw new KeyNotFoundException("No such user");
        }
        
        return user;
    }
    
    public void Insert(User user)
    {
        String registrationQueryString = GetInsertQueryString(user.Login.Data, user.Password.Data);

        NpgsqlCommand registerCommand = new NpgsqlCommand(registrationQueryString, _connectionProvider.GetConnection());
        int executeReturnCode = registerCommand.ExecuteNonQuery();

        if (executeReturnCode == -1) 
            throw new NpgsqlException("ExecuteNonQuery() inside update failed");
    }
}
