using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wkmvc
{
    internal class Platform
    {

    }
    /// <summary>
    /// 数据库
    /// </summary>
    internal class DataBaeProvider
    {
        private ApplicationConfiguration dataBaseProvider = new Service.ConfigServices.AppConfigurtaionServices().GetAppSettings<ApplicationConfiguration>("SiteBaseConfig");
        /// <summary>
        /// 数据库类型--mssql
        /// </summary>
        public bool _isSqlServer
        {
            get
            {
                return dataBaseProvider.DataBase.ToLower() == "mssql";
            }
        }
        /// <summary>
        /// 数据库类型--mysql
        /// </summary>
        public bool _isMysql
        {
            get
            {
                return dataBaseProvider.DataBase.ToLower() == "mysql";
            }
        }
        public bool _isOracle
        {
            get
            {
                return dataBaseProvider.DataBase.ToLower() == "oracle";
            }
        }

    }
    /// <summary>
    /// 缓存相关配置取得
    /// </summary>
    internal class CacheProvider
    {
        private ApplicationConfiguration cacheProvider = new Service.ConfigServices.AppConfigurtaionServices().GetAppSettings<ApplicationConfiguration>("siteconfig");
        /// <summary>
        /// 是否redis
        /// </summary>
        public bool _isUseRedis
        {
            get
            {
                return cacheProvider.UserRedis;
            }
        }
        /// <summary>
        /// radis连接字符串
        /// </summary>
        public string _connectionString
        {
            get
            {
                return cacheProvider.Redis_connectionString;
            }
        }
        /// <summary>
        /// Redis实例名称   
        /// </summary>
        public string _instanceName
        {
            get
            {
                return cacheProvider.Redis_instanceName;
            }
        }
    }
}
