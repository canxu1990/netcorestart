using wkmvc.Core;
using wkmvc.Data;
using wkmvc.Data.Models;
namespace Service.IService
{
    public interface IUserManage:IRepository<SYS_USER>
    {
        /// <summary>
        /// 测试接口
        /// </summary>
        /// <returns></returns>
        string Test();
        bool TestOne();
        bool Login(LoginViewModel user);
    }
}