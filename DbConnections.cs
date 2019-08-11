using CommonLib;
using DBHelper.Common;
using System;
using System.Data.Common;

namespace DBHelper
{
    public class DbConnections
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        public static DataBaseType DbType
        {
            get
            {
                string dbTypeStr = ConfigHelper.GetConfigString("DbType")?.ToLower();
                switch (dbTypeStr)
                {
                    case "sqlserver":
                        return DataBaseType.SqlServer;
                    case "mysql":
                        return DataBaseType.MySql;
                    case "oracle":
                        return DataBaseType.Oracle;
                    case "sqlite":
                        return DataBaseType.Sqlite;
                    default:
                        return DataBaseType.Unknown;
                }
            }
        }

        /// <summary>
        /// 获得数据库类型
        /// </summary>
        /// <param name="conn">数据库链接</param>
        /// <returns></returns>
        public static DataBaseType GetDbTypeByConn(DbConnection conn)
        {
            DataBaseType dbType = DataBaseType.Unknown;
            if (conn == null) return dbType;

            string connStr = conn.ToString();
            if (connStr.IndexOf("sqlite", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                dbType = DataBaseType.Sqlite;
            }

            return dbType;
        }

        public static string GetDbConnectionName(DataBaseType dbtype = DataBaseType.Auto, string defaultName = "")
        {
            if (dbtype == DataBaseType.Auto)
                dbtype = DbType;
            string name = ConfigHelperEx.GetConnection(dbtype)?.Name;
            if (string.IsNullOrWhiteSpace(name))
                name = defaultName;
            return name;
        }

        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <returns></returns>
        public static string GetDbConnectionString(DataBaseType dbtype = DataBaseType.Auto)
        {
            if (dbtype == DataBaseType.Auto)
                dbtype = DbType;
            return ConfigHelperEx.GetConnection(dbtype)?.ConnectionString;
        }
    }
}
