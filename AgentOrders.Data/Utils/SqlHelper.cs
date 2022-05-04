using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

namespace AgentOrders.Data.Utils
{
    class SqlHelper
    {
        private readonly string connectionString;
        private bool useStoredProc;

        public SqlHelper(string connectionString, bool useStoredProc = true)
        {
            this.connectionString = connectionString;
            this.useStoredProc = useStoredProc;
        }

        public T GetEntity<T>(string sqlText, Func<IDataReader, T> mapper)
        {
            return GetEntity(sqlText, parameters: null, mapper: mapper);
        }

        public T GetEntity<T>(string sqlText, SqlParameter parameter, Func<IDataReader, T> mapper)
        {
            return GetEntity(sqlText, new[] { parameter }, mapper);
        }

        public T GetEntity<T>(string sqlText, SqlParameter[] parameters, Func<IDataReader, T> mapper)
        {
            var list = GetEntityList(sqlText, parameters, mapper);
            return list.FirstOrDefault();
        }

        public List<T> GetEntityList<T>(string sqlText, Func<IDataReader, T> mapper)
        {
            return GetEntityList(sqlText, parameters: null, mapper: mapper);
        }

        public List<T> GetEntityList<T>(string sqlText, SqlParameter parameter, Func<IDataReader, T> mapper)
        {
            return GetEntityList(sqlText, new[] { parameter }, mapper);
        }

        public List<T> GetEntityList<T>(string sqlText, SqlParameter[] parameters, Func<IDataReader, T> mapper)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sqlText, connection))
            {
                connection.Open();
                InitCommand(cmd, parameters);
                var reader = cmd.ExecuteReader();
                var result = new List<T>();
                while (reader.Read())
                {
                    result.Add(mapper(reader));
                }
                return result;
            }
        }

        public bool RowExists(string sqlText, SqlParameter[] parameters = null)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var statement = new SqlCommand(sqlText, connection))
            {
                connection.Open();
                InitCommand(statement, parameters);
                var reader = statement.ExecuteReader();
                return reader.Read();
            }
        }

        public void ExecSql(string sqlText)
        {
            ExecSql(sqlText, parameters: null);
        }

        public void ExecSql(string sqlText, SqlParameter parameter)
        {
            ExecSql(sqlText, new[] { parameter });
        }

        public void ExecSql(string sqlText, SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sqlText, connection))
            {
                connection.Open();
                InitCommand(cmd, parameters);
                cmd.ExecuteNonQuery();
            }
        }

        private void InitCommand(SqlCommand cmd, SqlParameter[] parameters)
        {
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }

            if (useStoredProc)
            {
                cmd.CommandType = CommandType.StoredProcedure;
            }
        }
    }
}
