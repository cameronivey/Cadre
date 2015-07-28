using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cadre.DataAccessLayer
{
    public interface IPostDatabase
    {
        IQueryable<T> Get<T>() where T : class;

        T Add<T>(T entity) where T : class;

        T Remove<T>(T entity) where T : class;

        Task<int> CommitAsync<T>(CancellationToken token) where T : class;

        Task<int> CommitAsync<T>() where T : class;
    }
}
