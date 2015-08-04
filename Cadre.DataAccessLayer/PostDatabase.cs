using Cadre.Domain.Models;
using System.Data.Entity;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using Cadre.Domain;

namespace Cadre.DataAccessLayer
{
    public class PostDatabase : DbContext, IPostDatabase
    {
        public PostDatabase() : base()
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
        }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Announcement> Announcements { get; set; }

        public DbSet<Request> Requests { get; set; }

        public DbSet<User> Users { get; set; }

        public IQueryable<T> Get<T>() where T : class, IEntity
        {
            return this.Set<T>();
        }

        public T GetSingleById<T>(int id) where T : class, IEntity
        {
            return Set<T>().SingleOrDefault(x => x.Id == id);
        }

        public T Add<T>(T entity) where T : class, IEntity
        {
            return this.Set<T>().Add(entity);
        }

        public T Remove<T>(T entity) where T : class, IEntity
        {
            return this.Set<T>().Remove(entity);
        }

        public async Task<int> CommitAsync<T>() where T : class, IEntity
        {
            return await this.CommitAsync<T>(CancellationToken.None);
        }

        public async Task<int> CommitAsync<T>(CancellationToken token) where T : class, IEntity
        {
            try
            {
                return await this.SaveChangesAsync(token);
            }
            catch (DbEntityValidationException ex)
            {
                // We would log the exception here
                throw ex;
            }
            catch (DbUpdateException ex)
            {
                // We would log the exception here
                throw ex;
            }
            catch (Exception ex)
            {
                // We would log the exception here
                throw ex;
            }
        }
    }
}
