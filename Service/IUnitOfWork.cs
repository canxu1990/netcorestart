namespace wkmvc.Core
{
    /// <summary>
    /// Describe：工作单元接口
    /// Author：yuangang
    /// Date：2016/07/16
    /// Blogs:http://www.cnblogs.com/yuangang
    /// </summary>
    public interface IUnitOfWork
    {
        bool Commit();
    }
}