using GraphFlix.Appsettings;
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
        catch (Exception)
        {

            throw;
        }


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
                JObject jsonObject = JObject.Parse(json);
                var id = jsonObject["Id"];

                var jTokenProperties = jsonObject["Properties"];
                jTokenProperties["Id"] = id;
                var jsonProperties = JsonConvert.SerializeObject(jTokenProperties);
                jsonList.Add(jsonProperties);
            }
        }
        catch (JsonReaderException j)
        {

            throw;
        }

        return jsonList;
    }
    private List<T> ConvertJsonObjects <T>(List<string> jsonList)
    {
        var convertedJsonList = new List<T>();

        foreach (var json in jsonList)
        {
            var newObject = JsonConvert.DeserializeObject<T>(json);
            convertedJsonList.Add(newObject);
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
        catch (Exception)
        {

            throw;
        }

    }
    public void Dispose()
    {
        _driver?.Dispose();
    }

}
