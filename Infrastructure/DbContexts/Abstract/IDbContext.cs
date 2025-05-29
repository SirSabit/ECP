using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DbContexts.Abstract
{
    public interface IDbContext<T> where T: DbContext
    {
        public T Instance { get;}
    }
}
