using GraphFlix.Database;
using GraphFlix.DTOs;
using GraphFlix.Helper;
using GraphFlix.Models;

namespace GraphFlix.Repositories;

public class MovieRepository : IMovieRepository
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
    public async Task GetNodesAsync()
    {
        IBuilder builder = new QueryBuilder();
        IQuery query = builder.Match("n").Return<string>("n").Build();
        var result = await neo.ExecuteReadAsync(query.ToString());
        //return testdata;
    }
    /// <summary>
    /// Gets all movies and 
    /// </summary>
    /// <returns></returns>
    public async Task<List<Movie>> GetMoviesAsync()
    {
        IBuilder builder = new QueryBuilder();
        IQuery query = builder.Match("movie:Movie").Return<List<Movie>>("movie").Build();
        var result =  await neo.ExecuteReadAsync<Movie>(query);
        return result;
    }

    /// <summary>
    /// Used for creating movie
    /// </summary>
    /// <returns></returns>
    public async Task CreateMovie(Movie movie)
    {
        IBuilder builder = new QueryBuilder();

        IQuery query = builder.Create(movie).Build();
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

    public Task<IEnumerable<MovieDto>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<MovieDto?> GetById(string id)
    {
        throw new NotImplementedException();
    }

    public Task<MovieDto> Create(MovieDto movie)
    {
        throw new NotImplementedException();
    }

    public Task<MovieDto> Update(MovieDto movieChanges)
    {
        throw new NotImplementedException();
    }

    public Task<MovieDto> Delete(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<MovieDto>> GetRecommendedMovies(int userId)
    {
        throw new NotImplementedException();
    }
}
