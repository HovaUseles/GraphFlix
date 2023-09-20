

using Neo4j.Driver;
using Neo4jCipher;

namespace GraphFlix.Database
{
    public interface INeo4J
    {
        /// <summary>
        /// Reading from neo4j database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns>list of T (generic)</returns>
        public Task<List<T>> ExecuteReadAsync<T>(Neo4jCipher.IQuery query);
        /// <summary>
        /// Reads from neo4j database
        /// </summary>
        /// <param name="query"></param>
        /// <returns>List of IRecords</returns>
        public Task<List<IRecord>> ExecuteReadAsync(string query);
        public Task ExecuteCreateAsync(IQuery query);
    }
}
