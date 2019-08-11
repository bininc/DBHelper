using System;
using System.Configuration;

namespace DBHelper.Common
{
    public static class ConfigHelperEx
    {

        /// <summary>
        /// 获取配置文件数据库连接字符串
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static string GetConnectionString(DataBaseType dbType)
        {
            return GetConnection(dbType)?.ConnectionString;
            
        }
        /// <summary>
        /// 获取配置文件数据库连接配置
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ConnectionStringSettings GetConnection(DataBaseType dbType)
        {
            try
            {
                string key = null;
                switch (dbType)
                {
                    case DataBaseType.SqlServer:
                        key = "sqlclient";
                        break;
                    case DataBaseType.MySql:
                        key = "mysql";
                        break;
                    case DataBaseType.Oracle:
                        key = "oracle";
                        break;
                    case DataBaseType.Sqlite:
                        key = "sqlite";
                        break;
                }
                if (key == null) return null;

                var conns = ConfigurationManager.ConnectionStrings.GetEnumerator();

                while (conns.MoveNext())
                {
                    var tmp = (ConnectionStringSettings)conns.Current;
                    if (tmp.Name.IndexOf(key, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        return tmp;
                    }
                    if (tmp.ProviderName.IndexOf(key, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        if (tmp.Name == "LocalSqlServer")
                            continue;
                        return tmp;
                    }
                }

                conns = ConfigurationManager.AppSettings.GetEnumerator();
                while (conns.MoveNext())
                {
                    string tmp = (string)conns.Current;
                    if (tmp.IndexOf(key, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        return new ConnectionStringSettings(tmp, ConfigurationManager.AppSettings[tmp]);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex, "ConfigHelper.GetConnection");
                return null;
            }
        }
    }
}
