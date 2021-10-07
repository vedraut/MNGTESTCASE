using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace TestCase.RepositoryData.Core
{
    public static class RepositoryExtensions
    {
        public static T GetValue<T>(this IDataReader reader, string fieldName, T defaultValue)
        {
            try
            {
                var index = reader.GetOrdinal(fieldName);
                if (reader.IsDBNull(index))
                {
                    return defaultValue;
                }
                return (T)(reader[fieldName]);
            }
            catch (IndexOutOfRangeException)
            {
                // This exception is acceptable. Let's ignore it.
                return defaultValue;
            }
            catch (Exception exception)
            {
                exception.Message.Contains("");
                throw;
            }
        }

        public static IList<T> GetEntities<T>(this IDbCommand command, Func<IDataReader, T> getEntityDelegate)
        {
            var list = new List<T>();
            try
            {
                {
                    // Fetch data.
                    using (var reader = command.ExecuteReader())
                    {
                        // Enumerate through the data and create model instances.
                        while (reader != null && reader.Read())
                        {
                            var entity = getEntityDelegate(reader);
                            if (entity != null && !entity.Equals(default(T)))
                            {
                                list.Add(entity);
                            }
                        }
                    }
                }
            }
            catch (SqlException exception)
            {
                exception.Message.Contains("");
                throw;
            }
            return list;
        }

        public static T GetEntity<T>(this IDbCommand command, Func<IDataReader, T> getEntityMethod) where T : class
        {
            try
            {
                {
                    using (var reader = command.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        return reader != null && reader.Read() ? getEntityMethod(reader) : default(T);
                    }
                }
            }
            catch (SqlException exception)
            {
                exception.Message.Contains("");
                throw;
            }
        }

        public static IDataReader ExecuteCommand(this IDbCommand command)
        {
            try
            {
                    return command.ExecuteReader();
            }
            catch (SqlException exception)
            {
                exception.Message.Contains("");
                throw;
            }
        }


        public static int ExecuteNonQuery(this IDbCommand command)
        {
            int numberofRowsAffected;
            try
            {
                    numberofRowsAffected = command.ExecuteNonQuery();
            }
            catch (SqlException exception)
            {
                exception.Message.Contains("");
                throw;
            }
            return numberofRowsAffected;
        }


        public static T ExecuteScalar<T>(this IDbCommand command)
        {
            T result;
            try
            {
                {
                    var resultObject = command.ExecuteScalar();
                    if (resultObject != null)
                    {
                        result = (T)resultObject;
                    }
                    else
                    {
                        result = default(T);
                    }
                }
            }
            catch (SqlException exception)
            {
                exception.Message.Contains("");
                throw;
            }
            return result;
        }
    }
}