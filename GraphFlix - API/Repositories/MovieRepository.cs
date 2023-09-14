using GraphFlix.Database;
using GraphFlix.Helper;
using GraphFlix.Models;

namespace GraphFlix.Repositories;

public class MovieRepository //Create Interface
{
    private readonly Neo4J neo;
    public MovieRepository(Neo4J neo4J)
    {
        neo = neo4J;
    }
    /// <summary>
    /// Gets all nodes
    /// Used for testing
    /// </summary>
    public async Task<Dictionary<long, IReadOnlyList<string>>> GetNodesAsync()
    {
        var testdata = await neo.ExecuteReadAsync("MATCH (n) RETURN n");
        return testdata;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<Dictionary<long, IReadOnlyList<string>>> GetMoviesAsync()
    {
        var movies = await neo.ExecuteReadAsync("MATCH (movie:Movie) RETURN movie");
        //TODO: Convert to models
        return movies;
    }

    /// <summary>
    /// Used for creating movie
    /// </summary>
    /// <returns></returns>
    public async Task CreateMovie(Movie movie)
    {
        string query = $"CREATE (:Movie {{title: '{movie.Title}'}})";
        try
        {
            await neo.ExecuteCreateAsync(query);
        }
        catch (Exception e)
        {
            Log.Logger.Error("Unknow exception | Message: {0}", e.Message);
            //TODO: Handle exception
        }
    }
}
