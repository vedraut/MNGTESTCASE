using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
namespace TestCase.RepositoryData.Core
{
    public abstract class RepositoryBase : IDisposable
    {
        private readonly object _mutex = new object();
        protected SqlConnection Connection;
        protected SqlConnection MultipleActiveResultSetConnection;
        protected RepositoryBase()
        {
            // Store the database type
        }
        protected void SetConnection()
        {
            var connectionString = GetConnectionString();
            Connection = new SqlConnection(connectionString.ToString());
        }
        private static ConnectionStringSettings GetConnectionString()
        {
            // If none has been set, use the default hard coded entries.
            ConnectionStringSettings connectionStringSettings = new ConnectionStringSettings();
            connectionStringSettings.ConnectionString = @"Data Source = (LocalDb)\MSSQLLocalDB; Integrated Security = True; Persist Security Info = False; Pooling = False; MultipleActiveResultSets = False; Connect Timeout = 60; Encrypt = False; TrustServerCertificate = False";
            
            return connectionStringSettings;
        }
        protected void OpenConnection()
        {
            if (Connection == null)
            {
                SetConnection();
            }
            if (Connection != null && Connection.State == ConnectionState.Open)
            {
                return;
            }
            Connection.Open();
        }

        protected void CloseConnection()
        {
            if (Connection != null && Connection.State == ConnectionState.Open)
            {
                Connection.Close();
            }
        }
        public void Dispose()
        {
            CloseConnection();
        }

        public IList<T> GetEntities<T>(string storedProcedureName, Func<IDataReader, T> getEntityDelegate, List<SqlParameter> sqlParameters = null)
        {
            IList<T> entities;
            var command = CreateCommand(storedProcedureName, sqlParameters);
            try
            {
                OpenConnection();
                entities = command.GetEntities(getEntityDelegate);
            }
            finally
            {
                CloseConnection();
            }
            return entities;
        }

        public T GetEntity<T>(string storedProcedureName, Func<IDataReader, T> getEntityDelegate, List<SqlParameter> sqlParameters = null) where T : class
        {
            T entity;
            var command = CreateCommand(storedProcedureName, sqlParameters);
            try
            {
                OpenConnection();
                entity = command.GetEntity(getEntityDelegate);
            }
            finally
            {
                CloseConnection();
            }
            return entity;
        }

        public int ExecuteNonQuery(string storedProcedureName, List<SqlParameter> sqlParameters = null)
        {
            var command = CreateCommand(storedProcedureName, sqlParameters);
            try
            {
                OpenConnection();
                return RepositoryExtensions.ExecuteNonQuery(command);
            }
            finally
            {
                CloseConnection();
            }
        }

        private SqlCommand CreateCommand(string storedProcedureName, List<SqlParameter> sqlParameters)
        {
            var command = new SqlCommand
            {
                CommandText = storedProcedureName,
                CommandType = CommandType.StoredProcedure,
                Connection = Connection
            };
            if (sqlParameters != null && sqlParameters.Any())
            {
                command.Parameters.Clear();
                command.Parameters.AddRange(sqlParameters.ToArray());
            }
            return command;
        }

        protected int ExecuteNonQuery(SqlCommand sqlCommand)
        {
            try
            {
                OpenConnection();
                return RepositoryExtensions.ExecuteNonQuery(sqlCommand);
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}

