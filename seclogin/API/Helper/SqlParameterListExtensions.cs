using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace admin.api.Extensions;

public static class SqlParameterListExtensions
{
    public static string GetSqlParameterString(this List<SqlParameter> list)
    {
        var sParams = "";

        foreach (var o in list)
            sParams += (string.IsNullOrEmpty(sParams) ? "" : ",") + o.ParameterName + " = " + o.ParameterName;

        return sParams;
    }
}