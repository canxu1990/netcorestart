using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wkmvc.Data;

namespace wkmvc.Core
{
    /// <summary>
    /// Describe：工作单元实现类
    /// </summary>
    public class UnitOfWork:IUnitOfWork,IDisposable
    {
        #region 数据上下文
        private ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        public bool Commit()
        {
            return _context.SaveChanges()>0;
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
            GC.SuppressFinalize(this);
        }
    }
}
