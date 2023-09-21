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
    public async Task<IEnumerable<MovieDto>> GetAll()
    {
        IQuery q1 = new Query().PlainQuery("MATCH (m:Movie)-[go:GENRE_OF]->(g:Genre) WITH COLLECT({    Id: split(elementId(g), ':')[2],     Name: g.name }) as genres, m RETURN {Id: split(elementId(m), ':')[2], Title: m.Title, ReleaseDate: m.ReleaseDate, Genres: genres} AS DetailedMovie");
        var result = await neo.ExecuteReadAsync<MovieDto>(q1);
        return result;
    }

    public async Task<MovieDto?> GetById(string id)
    {
        IQuery q1 = new Query().PlainQuery($"MATCH (m:Movie WHERE m.id = {id}) RETURN m LIMIT 1");
        var result = await neo.ExecuteReadAsync<MovieDto>(q1);
        return result.FirstOrDefault();
    }

    public async Task Create(MovieDto movieDto)
    {
        Movie movie = new Movie()
        {
            Title = movieDto.Title,
            ReleaseDate = movieDto.ReleaseDate,
        };
        IQuery query = new Query().AddCreate(movie);
        try
        {
            await neo.ExecuteWriteAsync(query);
        }
        catch (Exception e)
        {
            Log.Logger.Error("Unknown error | Message {0}", e);
            //TODO: Handle exception
        }
    }

    public Task Update(MovieDto movieChanges)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(string id)
    {
        IQuery query = new Query().PlainQuery($"MATCH (m:Movie WHERE m.Id = {id}) DETACH DELETE m");
        await neo.ExecuteWriteAsync(query);
    }

    public async Task<IEnumerable<MovieDto>> GetRecommendedMovies(int userId)
    {
        IQuery q1 = new Query().PlainQuery($"MATCH (u:User)-[:RECOMMENDED]->(recMovie:Movie)   WHERE u.Id = {userId} MATCH (recMovie)<-[:RECOMMENDED]-(otherUser:User)-[:RECOMMENDED]->(otherMovie:Movie)     WHERE NOT (u)-[:RECOMMENDED]->(otherMovie) WITH u, otherUser, otherMovie, COUNT(*) AS commonMovies ORDER BY commonMovies DESC RETURN otherMovie, commonMovies");
        var result = await neo.ExecuteReadAsync<MovieDto>(q1);
        return result;
    }
}
