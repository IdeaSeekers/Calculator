using Domain;

namespace Database;
using Npgsql;

public struct UserDatabaseRow
{
    public Int32 id;
    public String login;
    public String password;
}

public class DatabaseUsersRepository
{
    private DatabaseConnectionProvider connectionProvider;
    
    private String usersTableName = "Users";
    private String usersIdColumn = "id";
    private String usersLoginColumn = "login";
    private String usersPasswordColumn = "password";

    private String getSelectQueryString(String login)
    {
        return String.Format(
            "SELECT {1}, {2}, {3} FROM {0} WHERE {2} = '{4}'",
            usersTableName, usersIdColumn, usersLoginColumn, usersPasswordColumn, login
        );
    }
    
    private String getInsertQueryString(String login, String password)
    {
        return String.Format("INSERT INTO {0} ({1}, {2}) VALUES ('{3}', '{4}')",
            usersTableName, usersLoginColumn, usersPasswordColumn,
            login, password);
    }
    
    public DatabaseUsersRepository(String databaseUser, String password, String databaseName)
    {
        connectionProvider = new DatabaseConnectionProvider(databaseUser, password, databaseName);
    }
    
    public UserDatabaseRow Get(UserInfo key)
    {
        UserDatabaseRow user = new UserDatabaseRow();
        String selectIdQueryString = getSelectQueryString(key.Login.Data);

        NpgsqlCommand selectIdCommand = new NpgsqlCommand(selectIdQueryString, connectionProvider.GetConnection());
        NpgsqlDataReader reader = selectIdCommand.ExecuteReader();

        if (reader.HasRows)
        {
            int idColumn = 0, loginColumn = 1, passwordColumn = 2;
            reader.Read();
            user.id = reader.GetInt32(idColumn);
            user.login = reader.GetString(loginColumn);
            user.password = reader.GetString(passwordColumn);
        }
        else
        {
            throw new KeyNotFoundException("No such user");
        }
        
        return user;
    }
    
    // TODO: do we support password change?
    public void Insert(User user)
    {
        String registrationQueryString = getInsertQueryString(user.Login.Data, user.Password.Data);

        NpgsqlCommand registerCommand = new NpgsqlCommand(registrationQueryString, connectionProvider.GetConnection());
        int ret = registerCommand.ExecuteNonQuery();

        if (ret == -1) throw new NpgsqlException("ExecuteNonQuery() inside update failed");
    }
}