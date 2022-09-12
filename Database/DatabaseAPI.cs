using Domain;
using FluentResults;
using Npgsql;

namespace Database;

public class DatabaseAPI
{
    private NpgsqlConnection conn;
    private DatabaseHistoryRepository historyRepo;
    private DatabaseUsersRepository usersRepo;
    
    public DatabaseAPI(String databaseUser, String password, String databaseName)
    {
        historyRepo = new DatabaseHistoryRepository(databaseUser, password, databaseName);
        usersRepo = new DatabaseUsersRepository(databaseUser, password, databaseName);
    }
    
    public UpdateHistoryResult UpdateHistory(CalculationData calculationData, UserInfo userInfo)
    {
        try
        {
            HistoryDatabaseRow row = new HistoryDatabaseRow();
            row.id = usersRepo.Get(userInfo).id;
            row.query = calculationData.Query.QueryString;
            row.valid = calculationData.Result.Result.IsSuccess;
            row.result = -1;
            if (row.valid) row.result = calculationData.Result.Result.Value;
            
            historyRepo.Update(row);

            GetHistoryResult history = GetHistory(userInfo);
            
            return new UpdateHistoryResult(history.History);
        }
        catch (Exception e)
        {
            return new UpdateHistoryResult(Result.Fail("Source: " + e.Source + "; Message: " + e.Message));
        }
    }

    public GetHistoryResult GetHistory(UserInfo userInfo)
    {
        try
        {
            Int32 id = usersRepo.Get(userInfo).id;
            List<HistoryDatabaseRow> history = historyRepo.Get(id);
            CalculationData[] data = new CalculationData[history.Count];

            for (int i = 0; i < data.Length; i++)
            {
                CalculationQuery query = new CalculationQuery(history[i].query);
                CalculationResult result;
                if (history[i].valid) result = new CalculationResult(Result.Ok(history[i].result));
                else result = new CalculationResult(Result.Fail("Wrong query"));

                data[i] = new CalculationData(query, result);
            }

            CalculationHistory calculationHistory = new CalculationHistory(data);
            return new GetHistoryResult(Result.Ok(calculationHistory));
        }
        catch (Exception e)
        {
            return new GetHistoryResult(Result.Fail("Source: " + e.Source + "; Message: " + e.Message));
        }
    }
    
    public RegisterUserDatabaseResult RegisterUser(User user)
    {
        try
        {
            usersRepo.Insert(user);
            return new RegisterUserDatabaseResult(Result.Ok());
        }
        catch (Exception e)
        {
            return new RegisterUserDatabaseResult(Result.Fail("Source: " + e.Source + "; Message: " + e.Message));
        }
    }
    
    public GetUserResult GetUser(UserInfo userInfo)
    {
        try
        {
            UserDatabaseRow row = usersRepo.Get(userInfo);

            Login login = new Login(row.login);
            Password password = new Password(row.password);
            User user = new User(login, password);

            return new GetUserResult(Result.Ok(user));
        }
        catch (Exception e)
        {
            return new GetUserResult(Result.Fail("Source: " + e.Source + "; Message: " + e.Message));
        }
    }
}