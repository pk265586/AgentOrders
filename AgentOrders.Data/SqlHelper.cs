using System;
using System.IO;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;

namespace AgentOrders.Data
{
    public class SqlHelper
    {
        private readonly string connectionString;

        public SqlHelper(string connectionString)
        {
            this.connectionString = connectionString;
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
        }

        public bool IsTableExist(string tableName)
        {
            return RowExists($"Select Top 1 id From dbo.sysobjects Where id = object_id(N'dbo.{tableName}')");
        }

        public string[] RunScript(string fileName) 
        {
            if (!File.Exists(fileName))
                return new string[0];

            string[] lines = File.ReadAllLines(fileName);
            return RunScript(lines);
        }

        private string[] RunScript(string[] lines)
        {
            var errorList = new List<string>();
            var sqlText = new StringBuilder();

            for (int idxLine = 0; idxLine < lines.Length; idxLine++)
            {
                string line = lines[idxLine];
                string ScriptLine_Trim = line.Trim();
                bool isStatementEnd = ScriptLine_Trim.Equals("GO", StringComparison.OrdinalIgnoreCase);

                if (!isStatementEnd)
                {
                    sqlText.AppendLine(line);
                }

                if (isStatementEnd || idxLine == lines.Length - 1)
                {
                    try
                    {
                        ExecSql(sqlText.ToString());
                    }
                    catch (Exception e)
                    {
                        errorList.Add($"ERROR in line {idxLine + 1}: {e.Message}");
                    }

                    sqlText.Clear();
                }
            }


            return errorList.ToArray();
        }
    }
}
