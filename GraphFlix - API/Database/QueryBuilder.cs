
using System.Linq.Expressions;

namespace GraphFlix.Database;

public interface IBuilder
{
    public IBuilder Create<T>(T model);
    public IBuilder Delete(string identities);
    public IBuilder DetachDelete(string identities);
    public IBuilder Match(string match);
    public IBuilder Where<T1>(Expression<Func<T1, bool>> expression);

    public IBuilder Return<T>(string model);

    public IQuery Build();
}
public class QueryBuilder : IBuilder
{
    private IQuery Query = new Query();

    public IBuilder Create<T>(List<T> model)
    {
        Query.AddCreate(model);
        return this;
    }
    public IBuilder Create<T>(T model)
    {
        Query.AddCreate(model);
        return this;
    }
    public IBuilder Delete(string identities)
    {
        Query.AddDelete(identities);
        return this;
    }
    public IBuilder DetachDelete(string identities)
    {
        Query.AddDetachDelete(identities);
        return this;
    }
    public IBuilder Match(string match)
    {
        // Perform the 'MATCH' operation
        Query.AddMatch(match);
        return this; // Return the builder instance
    }

    private IBuilder Where(LambdaExpression condition)
    {
        // Perform the 'WHERE' operation
        Query.AddWhere(condition);
        return this; // Return the builder instance
    }

    public IBuilder Where<T1>(Expression<Func<T1, bool>> expression)
    {
        return Where((LambdaExpression)expression);
    }

    public IBuilder Return<T>(string model)
    {
        // Perform the 'RETURN' operation
        Query.AddReturn(model);
        return this; // Return the builder instance
    }

    public IQuery Build()
    {
        return Query;
    }
}
