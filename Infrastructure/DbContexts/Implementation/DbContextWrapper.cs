using Infrastructure.DbContexts.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DbContexts.Implementation
{
    public class DbContextWrapper<T> : IDbContext<T> where T : DbContext
    {
        public T Instance { get; }

        public DbContextWrapper(T context) 
        {
            Instance = context;
        }
    }
}
