namespace GraphFlix.Repositories
{
    public interface IEdgeRepository
    {
        public Task CreateEdge<Edge, TFrom, TTo>(TFrom from, TTo to);
    }
}
