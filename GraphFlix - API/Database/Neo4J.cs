using GraphFlix.Appsettings;
using Microsoft.Extensions.Options;
using Neo4j.Driver;

namespace GraphFlix.Database
{
    public class Neo4J : INeo4J, IDisposable
    {
        private readonly IDriver _driver;

        public Neo4J(IOptions<Neo4JSettings> appSettings)
        {
            _driver = GraphDatabase.Driver(appSettings.Value.Uri, AuthTokens.Basic(appSettings.Value.Username, appSettings.Value.Password));
        }
        public async Task<Dictionary<long, IReadOnlyList<string>>> ExecuteReadAsync(string query)
        {
            using (var session = _driver.AsyncSession())
            {

                // Execute the query
                var result = await session.ExecuteReadAsync(async reader =>
                {
                    var cursor = await reader.RunAsync(query);
                    return await cursor.ToListAsync();
                });

                Dictionary<long, IReadOnlyList<string>> nodeLabels = new Dictionary<long, IReadOnlyList<string>>();
                foreach (var record in result)
                {
                    var node = record["n"].As<INode>();
                    nodeLabels.Add(node.Id, node.Labels);
                }
                return nodeLabels;
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
