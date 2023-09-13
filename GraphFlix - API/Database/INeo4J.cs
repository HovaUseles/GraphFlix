namespace GraphFlix.Database
{
    public interface INeo4J
    {
        public Task<Dictionary<long, IReadOnlyList<string>>> ExecuteReadAsync(string query);
        public Task ExecuteCreateAsync(string query);
    }
}
