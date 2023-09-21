using GraphFlix.Database;
using GraphFlix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphFlix___API___Tests;

public class QueryTests
{
    [Fact]
    public void Query_Create()
    {
        // Arrange
        Movie movie = new Movie()
        {
            Title = "Test",
            ReleaseDate = DateOnly.Parse("21-09-23")
        };
        //Act
        IQuery query = new Query().Create(movie);
        // Assert
        Assert.Equal("CREATE (:Movie { Id: '0', Title: 'Test', ReleaseDate: '21/09/2023'})", query.ToString());
    }
    [Fact]
    public void Query_Match()
    {
        IQuery query = new Query().Match("n").Return("n");
        // Assert
        Assert.Equal("MATCH (n) RETURN n", query.ToString());
    }
}
