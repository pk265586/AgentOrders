using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentOrders.Data
{
    public static class SqlUtils
    {
        public static bool IsTableExist(string connectionString, string tableName)
        {
            var helper = new SqlHelper(connectionString, useStoredProc: false);
            return helper.RowExists($"Select Top 1 id From dbo.sysobjects Where id = object_id(N'dbo.{tableName}')");
        }

        public static string[] RunScript(string connectionString, string fileName)
        {
            if (!File.Exists(fileName))
                return new string[0];

            string[] lines = File.ReadAllLines(fileName);
            return RunScript(connectionString, lines);
        }

        private static string[] RunScript(string connectionString, string[] lines)
        {
            var helper = new SqlHelper(connectionString, useStoredProc: false);
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
                        helper.ExecSql(sqlText.ToString());
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
