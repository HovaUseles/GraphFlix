using GraphFlix.Models.Edges;
using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GraphFlix.Database;
public interface IQuery
{
    public IQuery AddCreate<T>(T entity);
    public IQuery AddCreate<T>(List<T> listEntity);
    public IQuery AddDelete<T>(T model);
    public IQuery AddDetachDelete<T>(T model);
    public IQuery AddMatch(string match);

    public IQuery AddWhere(LambdaExpression condition);

    public IQuery AddReturn<T>(T model);
    public IQuery CreateRelationship<T1, T2>(IEdge edge, int fromId, int toId);
    public IQuery PlainQuery(string query);
}
public class Query : IQuery
{
    private StringBuilder _queryString = new StringBuilder();
    public IQuery PlainQuery(string query)
    {
        _queryString.Append(query);
        return this;
    }
    private void JsonFromObject<T>(T input)
    {
        Type entityType = typeof(T);

        if (!entityType.IsClass || entityType == typeof(string))
        {
            throw new ArgumentException("Entity must be a non-string class type.");
        }
        PropertyInfo[] properties = entityType.GetProperties();

        for (int i = 0; i < properties.Length; i++)
        {
            PropertyInfo property = properties[i];

            string propertyName = property.Name;
            object propertyValue = property.GetValue(input);

            if (propertyValue != null)
            {
                if (i > 0)
                {
                    _queryString.Append(", ");
                }

                _queryString.Append($"{propertyName}: '{propertyValue}'");
            }
        }

        _queryString.Append("}");

    }
    public IQuery AddCreate<T>(T entity)
    {
        Type entityType = typeof(T);

        if (!entityType.IsClass || entityType == typeof(string))
        {
            throw new ArgumentException("Entity must be a non-string class type.");
        }

        _queryString.Append("CREATE (:");
        _queryString.Append(entityType.Name);
        _queryString.Append(" { ");
        PropertyInfo[] properties = entityType.GetProperties();

        for (int i = 0; i < properties.Length; i++)
        {
            PropertyInfo property = properties[i];

            string propertyName = property.Name;
            object propertyValue = property.GetValue(entity);

            if (propertyValue != null)
            {
                if (i > 0)
                {
                    _queryString.Append(", ");
                }

                _queryString.Append($"{propertyName}: '{propertyValue}'");
            }
        }

        _queryString.Append("})");
        return this;
    }
    public IQuery AddCreate<T>(List<T> listEntity)
    {
        _queryString.Append("CREATE ");
        for (int i = 0; i < listEntity.Count; i++)
        {
            Type entityType = typeof(T);

            if (!entityType.IsClass || entityType == typeof(string))
            {
                throw new ArgumentException("Entity must be a non-string class type.");
            }
            _queryString.Append("(:");
            _queryString.Append(entityType.Name);
            _queryString.Append(" { ");
            PropertyInfo[] properties = entityType.GetProperties();

            for (int j = 0; j < properties.Length; j++)
            {
                PropertyInfo property = properties[j];

                string propertyName = property.Name;
                object propertyValue = property.GetValue(listEntity[i]);

                if (propertyValue != null)
                {
                    if (j > 0)
                    {

                        var test = _queryString.ToString().Split("{")[(i + 1)];
                        var paramsExists = test != string.Empty && test != " ";
                        if (paramsExists)
                        {
                            _queryString.Append(", ");
                        }
                    }

                    _queryString.Append($"{propertyName}: '{propertyValue}'");

                }
            }

            _queryString.Append("})");
            if (listEntity.Count - 1 >= i + 1)
            {
                _queryString.Append(", ");
            }
        }
        return this;

    }
    public IQuery AddDelete<T>(T model)
    {
        _queryString.Append("DELETE ");
        return this;
    }
    public IQuery AddDetachDelete<T>(T model)
    {
        _queryString.Append("DETACH DELETE ");
        return this;
    }
    public IQuery AddMatch(string match)
    {
        _queryString.Append("MATCH (");
        _queryString.Append(match);
        _queryString.Append(")");
        return this;
    }

    public IQuery AddWhere(LambdaExpression condition)
    {
        // Add 'WHERE' logic here
        _queryString.Append("WHERE ");
        _queryString.Append(condition);
        return this;
    }
    public IQuery AddReturn<T>(T model)
    {
        _queryString.Append("RETURN ");
        _queryString.Append(model);
        return this;
    }
    public IQuery CreateRelationship<T1, T2>(IEdge edge, int fromId, int toId)
    {
        //(charlie)-[:ACTED_IN {role: 'bud fox'}]->(wallStreet)
        _queryString.Append($"(fromNode:{typeof(T1).Name} WHERE u.Id = {fromId})");
        _queryString.Append($"-[:{edge} ");
        JsonFromObject(edge);
        _queryString.Append($"]->");
        _queryString.Append($"toNode:{typeof(T2).Name} WHERE toNode.Id = {toId})");
        return this;
    }
    public override string ToString()
    {
        return _queryString.ToString();
    }



}


