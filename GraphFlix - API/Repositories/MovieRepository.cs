using GraphFlix.Database;
using GraphFlix.Helper;
using GraphFlix.Models.Nodes;
using Neo4j.Driver;
using System.Reflection.Metadata.Ecma335;

namespace GraphFlix.Repositories;

public class MovieRepository //Create Interface
{
    private readonly INeo4J neo;
    public MovieRepository(INeo4J neo4J)
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
        return ReturnLabels(testdata);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<Dictionary<long, IReadOnlyList<string>>> GetMoviesAsync()
    {
        var movies = await neo.ExecuteReadAsync("MATCH (movie:Movie) RETURN movie");
        //TODO: Convert to models
        return ReturnLabels(movies);
        //return movies;
    }

    /// <summary>
    /// Used for creating movie
    /// </summary>
    /// <returns></returns>
    public async Task CreateMovie(Movie movie)
    {
        string query = $"CREATE (:Movie {{title: '{movie.Name}'}})";
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
    public async Task<Dictionary<long, IReadOnlyDictionary<string, object>>> GetRecommendedMoviesV1(string name)
    {
        string query = $"MATCH (u:User {{name: '{name}'}} )-[r:VIEWED {{recommended: true}}]->(m:Movie)<-[r2:VIEWED {{recommended: true}}]-(user2:User)-[r3:VIEWED {{recommended: true}}]->(movie2:Movie) RETURN DISTINCT movie2";
        try
        {
            var result = await neo.ExecuteReadAsync(query);
            return ReturnProperties(result);
        }
        catch (Exception)
        {

            throw;
        }
    }
    private Dictionary<long, IReadOnlyDictionary<string, object>> ReturnProperties(List<IRecord> result)
    {
        Dictionary<long, IReadOnlyDictionary<string, object>> nodeLabels = new Dictionary<long, IReadOnlyDictionary<string, object>>();
        foreach (var record in result)
        {
            var node = record["movie2"].As<INode>();

            nodeLabels.Add(node.Id, node.Properties);
        }
        return nodeLabels;
    }
    private Dictionary<long, IReadOnlyList<string>> ReturnLabels(List<IRecord> result)
    {
        Dictionary<long, IReadOnlyList<string>> nodeLabels = new Dictionary<long, IReadOnlyList<string>>();
        foreach (var record in result)
        {
            var node = record["movie2"].As<INode>();
            var movie = record["movie2"].As<Movie>();

            nodeLabels.Add(node.Id, node.Labels);
        }
        return nodeLabels;
    }
}
