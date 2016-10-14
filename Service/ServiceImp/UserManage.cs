using System;
using wkmvc.Core;
using wkmvc.Data;
using wkmvc.Data.Models;

namespace Service.ServiceImp
{
    public class UserManage : Repository<SYS_USER>, IService.IUserManage
    {
        public UserManage(ApplicationDbContext context) : base(context)
        {
        }

        public string Test()
        {
            return "我实现了接口方法Test";
        }
        public bool TestOne()
        {
            var users = new SYS_USER() { USERNAME = "张三"};
            return Save(users);
        }
        public bool Login(LoginViewModel user)
        {

            var ss= Get(p => p.USERNAME == user.userName&&p.PASSWORD==user.passWord)!=null;
            return ss;
        }
        

    }

   
}