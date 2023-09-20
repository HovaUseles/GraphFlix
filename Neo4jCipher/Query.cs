using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neo4jCipher;
public interface IQuery
{
    public void AddCreate<T>(T entity);
    public void AddCreate<T>(List<T> listEntity);
    public void AddDelete<T>(T model);
    public void AddDetachDelete<T>(T model);
    public void AddMatch(string match);

    public void AddWhere(LambdaExpression condition);

    public void AddReturn<T>(T model);
}
public class Query : IQuery
{
    private Type _returnType;
    private StringBuilder _queryString = new StringBuilder();

    public void AddCreate<T>(T entity)
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
    }
    public void AddCreate<T>(List<T> listEntity)
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

    }
    public void AddDelete<T>(T model)
    {
        _queryString.Append("DELETE ");
    }
    public void AddDetachDelete<T>(T model)
    {
        _queryString.Append("DETACH DELETE ");
    }
    public void AddMatch(string match)
    {
        _queryString.Append("MATCH (");
        _queryString.Append(match);
        _queryString.Append(")");
    }

    public void AddWhere(LambdaExpression condition)
    {
        // Add 'WHERE' logic here
        _queryString.Append("WHERE ");
        _queryString.Append(condition);
    }

    public void AddReturn<T>(T model)
    {
        _queryString.Append("RETURN ");
        _returnType = model.GetType();
        _queryString.Append(model);
    }
    public override string ToString()
    {
        return _queryString.ToString();
    }



}


