using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DbContexts.Abstract
{
    /// <summary>
    /// Wraps Db Contexts with abstraction. 
    /// </summary>
    public interface IDbContext<T> where T: DbContext
    {
        public T Instance { get;}
    }
}
