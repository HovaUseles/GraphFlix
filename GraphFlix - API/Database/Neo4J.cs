using GraphFlix.Appsettings;
using GraphFlix.Models.Nodes;
using Microsoft.Extensions.Options;
using Neo4j.Driver;
using System.Net.WebSockets;

namespace GraphFlix.Database
{
    public class Neo4J : INeo4J, IDisposable
    {
        private readonly IDriver _driver;

        public Neo4J(IOptions<Neo4JSettings> appSettings)
        {
            _driver = GraphDatabase.Driver(appSettings.Value.Uri, AuthTokens.Basic(appSettings.Value.Username, appSettings.Value.Password));
        }
        public async Task<List<IRecord>> ExecuteReadAsync(string query)
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
        public async Task ExecuteCreateAsync(string query)
        {
            using (var session = _driver.AsyncSession())
            {
                var result = await session.ExecuteWriteAsync(async writer =>
                {
                    var cursor = await writer.RunAsync(query);
                    return await cursor.ToListAsync();
                });
            }
        }
        public void Dispose()
        {
            _driver?.Dispose();
        }

    }
}
