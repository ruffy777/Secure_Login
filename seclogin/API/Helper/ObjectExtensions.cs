using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Data.SqlClient;

namespace admin.api.Extensions;

public static class ObjectExtensions
{
    public static List<SqlParameter> GetSqlParameterList(this object o, bool addSetParameters)
    {
        var list = new List<SqlParameter>();
        foreach (var prop in o.GetType().GetProperties())
        {
            object propName = prop.Name;
            var propValue = prop.GetValue(o, null);
            var propType = prop.PropertyType;

            var readOnly = false;
            bool isNull;

            foreach (var attrib in prop.CustomAttributes)
                if (attrib.AttributeType == typeof(ReadOnlyAttribute))
                    readOnly = (bool)attrib.ConstructorArguments[0].Value;

            if (propType == typeof(Guid))
                isNull = (Guid)propValue == Guid.Empty;
            else
                isNull = propValue == null;

            if ((!prop.CanWrite || !readOnly) && !isNull)
            {
                list.Add(new SqlParameter("@" + propName, propValue));
                if (addSetParameters &&
                    (propType == typeof(float)
                     || propType == typeof(float?)
                     || propType == typeof(int)
                     || propType == typeof(int?)
                     || propType == typeof(short)
                     || propType == typeof(short?)
                     || propType == typeof(int)
                     || propType == typeof(int?)
                     || propType == typeof(long)
                     || propType == typeof(long?)
                     || propType == typeof(bool)
                     || propType == typeof(bool?)
                     || propType == typeof(decimal)
                     || propType == typeof(decimal?)
                     || propType == typeof(DateTime)
                     || propType == typeof(DateTime?)
                     || propType == typeof(TimeSpan)
                     || propType == typeof(TimeSpan?)))
                    list.Add(new SqlParameter("@Set" + propName, true));
            }
        }

        return list;
    }
}