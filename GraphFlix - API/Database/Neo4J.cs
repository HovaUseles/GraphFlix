using GraphFlix.Appsettings;
using GraphFlix.Helper;
using Microsoft.Extensions.Options;
using Neo4j.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GraphFlix.Database;

/// <summary>
/// Database service for interaction with neo4j
/// </summary>
public class Neo4J : INeo4J, IDisposable
{
    private readonly IDriver _driver;
    /// <summary>
    /// </summary>
    /// <param name="appSettings"></param>
    public Neo4J(IOptions<Neo4JSettings> appSettings)
    {
        _driver = GraphDatabase.Driver(appSettings.Value.Uri, AuthTokens.Basic(appSettings.Value.Username, appSettings.Value.Password));
    }
    public async Task<List<T>> ExecuteReadAsync<T>(IQuery query)
    {
        try
        {
            using (var session = _driver.AsyncSession())
            {
                var result = await session.ExecuteReadAsync(async reader =>
                {
                    var cursor = await reader.RunAsync(query.ToString());
                    return await cursor.ToListAsync();
                });
                var jsonList = GetPropertiesFromIRecord(result);
                var convertedJson = ConvertJsonObjects<T>(jsonList);
                return convertedJson;
            }
        }
        catch (TransactionClosedException e)
        {
            Log.Logger.Error("Error occured while run the query from database | Message {0}", e);
        }
        catch (Exception e)
        {
            Log.Logger.Error("Unknow Error occured while reading from database | Message {0}", e);
        }
        return new List<T>();


    }
    /// <summary>
    /// Gets properties from IRecord
    /// </summary>
    /// <param name="records"></param>
    /// <returns></returns>
    private List<string> GetPropertiesFromIRecord(List<IRecord> records)
    {
        var jsonList = new List<string>();

        try
        {
            for (int i = 0; i < records.Count; i++)
            {
                var json = JsonConvert.SerializeObject(records[i].Values.FirstOrDefault().Value);
                jsonList.Add(json);
            }
        }
        catch (Exception e)
        {
            Log.Logger.Error($"Unknow error while serializing IRecord to json: {e}");
        }

        return jsonList;
    }
    private List<T> ConvertJsonObjects <T>(List<string> jsonList)
    {
        var convertedJsonList = new List<T>();

        try
        {
            foreach (var json in jsonList)
            {
                var newObject = JsonConvert.DeserializeObject<T>(json);
                convertedJsonList.Add(newObject);
            }
        }
        catch (Exception e)
        {
            Log.Logger.Error("Unknow error while Deserializing json to object | Message {0}", e);
        }


        return convertedJsonList;
    }
    /// <summary>
    /// Exectutes read query
    /// </summary>
    /// <param name="query"></param>
    /// <returns>List of IRecord</returns>
    public async Task<List<IRecord>> ExecuteReadAsync(string query)
    {
        try
        {
            using (var session = _driver.AsyncSession())
            {

                // Execute the query
                var result = await session.ExecuteReadAsync(async reader =>
                {
                    var cursor = await reader.RunAsync(query);
                    return await cursor.ToListAsync();
                });
                return result;
            }
        }
        catch (Exception)
        {

            throw;
        }


    }
    /// <summary>
    /// Inserts into database
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public async Task ExecuteWriteAsync(IQuery query)
    {
        try
        {
            using (var session = _driver.AsyncSession())
            {
                var result = await session.ExecuteWriteAsync(async writer =>
                {
                    var cursor = await writer.RunAsync(query.ToString());
                    return await cursor.ToListAsync();
                });
            }
        }
        catch (Exception e)
        {   
            Log.Logger.Error($"Failed to execute {query.ToString()} | Message: {e}");
        }

    }
    public void Dispose()
    {
        _driver?.Dispose();
    }

}
