using Neo4j.Driver;

namespace GraphFlix.Database
{
    public interface INeo4J
    {
        public Task<List<IRecord>> ExecuteReadAsync(string query);
        public Task ExecuteCreateAsync(string query);
    }
}
