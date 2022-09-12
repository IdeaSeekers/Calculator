using Domain;
using FluentResults;
using Npgsql;

namespace Database;

public struct HistoryDatabaseRow
{
    public Int32 id;
    public String query;
    public Boolean valid;
    public Double result;
}

public class DatabaseHistoryRepository
{
    private DatabaseConnectionProvider connectionProvider;
    
    private String historyTableName = "Calculations";
    private String historyIdColumn = "id";
    private String historyQueryColumn = "query";
    private String historyValidColumn = "valid";
    private String historyResultColumn = "result";

    private String getSelectHistoryString(Int32 id)
    {
        return String.Format("SELECT {1}, {2}, {3}, {4} FROM {0} WHERE {1} = {5}",
            historyTableName, historyIdColumn, historyQueryColumn, historyValidColumn, historyResultColumn,
            id);
    }

    private String getInsertHistoryString(Int32 id, String query, Boolean valid, Double result)
    {
        return String.Format("INSERT INTO {0} ({1}, {2}, {3}, {4}) VALUES ({5}, '{6}', {7}, {8})",
            historyTableName, historyIdColumn, historyQueryColumn, historyValidColumn, historyResultColumn,
            id, query, valid, result);
    }
    
    public DatabaseHistoryRepository(String databaseUser, String password, String databaseName)
    {
        connectionProvider = new DatabaseConnectionProvider(databaseUser, password, databaseName);
    }

    public List<HistoryDatabaseRow> Get(Int32 key)
    {
        String getHistoryString = getSelectHistoryString(key);

        NpgsqlCommand selectIdCommand = new NpgsqlCommand(getHistoryString, connectionProvider.GetConnection());
        NpgsqlDataReader reader = selectIdCommand.ExecuteReader();
        List<HistoryDatabaseRow> history = new List<HistoryDatabaseRow>();
        
        while (reader.HasRows)
        {
            int idColumn = 0, queryColumn = 1, validColumn = 2, resultColumn = 3;
            while (reader.Read())
            {
                HistoryDatabaseRow row = new HistoryDatabaseRow();
                row.id = reader.GetInt32(idColumn);
                row.query = reader.GetString(queryColumn);
                row.valid = reader.GetBoolean(validColumn);
                row.result = reader.GetDouble(resultColumn);
                history.Add(row);
            }

            reader.NextResult();
        }

        return history;
    }

    public void Update(HistoryDatabaseRow value)
    {
        String insertHistoryString = getInsertHistoryString(value.id, value.query, value.valid, value.result);
        
        NpgsqlCommand insertHistoryCommand = new NpgsqlCommand(insertHistoryString, connectionProvider.GetConnection());
        int ret = insertHistoryCommand.ExecuteNonQuery();

        if (ret == -1) throw new NpgsqlException("ExecuteNonQuery() inside insert failed");
    }
}