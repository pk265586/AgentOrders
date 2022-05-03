using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AgentOrders.Data.Utils
{
    static class SqlParameterUtils
    {
        /// <summary>
        /// Creates parameter from list of string values.
        /// Note: type StrList should be created in target DB (see script StrList.type.sql in Ramplus/Scripts).
        /// Example:
        /// <code>DBUtils.GetDataTable(connection, "Select * From Table Where Id in (Select * From @ids)", new[] { SqlParameterUtils.StrListParameter("@ids", ids) });</code>
        /// See also SqlQueryUtils.ValueListSelect
        /// </summary>
        public static SqlParameter CreateStrListParameter(string ParamName, IEnumerable<string> values)
        {
            var table = GetTableForListParameter(values, "Item");
            return ListParameterFromTable(ParamName, table);
        }

        private static DataTable GetTableForListParameter<T>(IEnumerable<T> values, string columnName)
        {
            DataTable table = new DataTable();
            table.Columns.Add(columnName, typeof(T));
            foreach (var item in values)
            {
                table.Rows.Add(item);
            }
            return table;
        }

        /// <summary>
        /// Creates parameter from table of values.
        /// Note: type StringList should be created in target DB (see script IntList.type.sql in Ramplus/Scripts).
        /// </summary>
        private static SqlParameter ListParameterFromTable(string ParamName, DataTable table)
        {
            SqlParameter parameter = new SqlParameter(ParamName, table);
            parameter.SqlDbType = SqlDbType.Structured;
            parameter.TypeName = "StringList";
            return parameter;
        }
    }
}
