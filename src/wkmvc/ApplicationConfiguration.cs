using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wkmvc
{
    public class ApplicationConfiguration
    {
        #region 属性成员
        /// <summary>
        /// 文件上传路径
        /// </summary>
        public string FileUpPath { get; set; }
        /// <summary>
        /// 是否启用单用户登录
        /// </summary>
        public string IsSingleLogin { get; set; }
        /// <summary>
        /// 允许文件上传的格式
        /// </summary>
        public string AttachExtension { get; set; }
        /// <summary>
        /// 图片上传最大值KB
        /// </summary>
        public int AttachImagesize { get; set; }
        /// <summary>
        /// 数据库
        /// </summary>
        public string DataBase { get; set; }
        /// <summary>
        /// 是否使用Redis
        /// </summary>
        public bool UserRedis { get; set; }
        /// <summary>
        /// Redis连接
        /// </summary>
        public string Redis_connectionString { get; set; }
        /// <summary>
        /// Redis实例名称
        /// </summary>
        public string Redis_instanceName { get; set; }
        #endregion
    }
}
