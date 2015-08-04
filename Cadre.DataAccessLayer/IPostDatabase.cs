using Cadre.Domain;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cadre.DataAccessLayer
{
    public interface IPostDatabase
    {
        IQueryable<T> Get<T>() where T : class, IEntity;

        T GetSingleById<T>(int id) where T : class, IEntity;

        T Add<T>(T entity) where T : class, IEntity;

        T Remove<T>(T entity) where T : class, IEntity;

        Task<int> CommitAsync<T>(CancellationToken token) where T : class, IEntity;

        Task<int> CommitAsync<T>() where T : class, IEntity;
    }
}
