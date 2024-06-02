using System.Reflection;
using AutoMapper;
using Base.Contracts.DAL;
using Base.Contracts.Domain;
using Base.Domain;
using Microsoft.EntityFrameworkCore;

namespace Base.DAL.EF;

public class BaseEntityRepository<TDomainEntity, TDalEntity, TDbContext> :
    BaseEntityRepository<Guid, TDomainEntity, TDalEntity, TDbContext>, IEntityRepository<TDalEntity>
    where TDomainEntity : BaseEntityIdMetadata
    where TDalEntity : BaseEntityId
    where TDbContext : DbContext
{
    public BaseEntityRepository(TDbContext dbContext, IDalMapper<TDomainEntity, TDalEntity> mapper) : base(dbContext,
        mapper)
    {
    }


}

public class BaseEntityRepository<TKey, TDomainEntity, TDalEntity, TDbContext>
    where TKey : IEquatable<TKey>
    where TDomainEntity : BaseEntityIdMetadata
    where TDalEntity : class, IDomainEntityId
    where TDbContext : DbContext

{
    protected readonly TDbContext RepoDbContext;
    protected readonly DbSet<TDomainEntity> RepoDbSet;
    protected readonly IDalMapper<TDomainEntity, TDalEntity> Mapper;
    
    public BaseEntityRepository(TDbContext dbContext, IDalMapper<TDomainEntity, TDalEntity> mapper)
    {
        RepoDbContext = dbContext;
        RepoDbSet = RepoDbContext.Set<TDomainEntity>();
        Mapper = mapper;
    }

    protected virtual IQueryable<TDomainEntity> CreateQuery(TKey? userId = default, bool noTracking = true)
    {
        var query = RepoDbSet.AsQueryable();
        if (userId != null && !userId.Equals(default) &&
            typeof(IDomainAppUserId<TKey>).IsAssignableFrom(typeof(TDomainEntity)))
        {
            query = query
                .Include("AppUser")
                .Where(e => ((IDomainAppUserId<TKey>) e).AppUserId.Equals(userId));
        }

        if (noTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }

    public virtual TDalEntity Add(TDalEntity entity, TKey? userId = default)
    {
        TDomainEntity domainEntity = Mapper.Map(entity)!;
        Type type = typeof(TDomainEntity);
        PropertyInfo? propertyInfo = type.GetProperty("AppUserId");
        if (propertyInfo != null && propertyInfo.PropertyType == typeof(Guid) && !EqualityComparer<TKey>.Default.Equals(userId, default(TKey)))
        {
            propertyInfo.SetValue(domainEntity, userId);
        }
        if (domainEntity.Id.ToString() == "00000000-0000-0000-0000-000000000000")
        {
            domainEntity.Id = Guid.NewGuid();
        }
        DateTime creationTime = DateTime.Now.ToUniversalTime();
        domainEntity.CreatedAt = creationTime;
        domainEntity.CreatedBy = "api";
        domainEntity.UpdatedAt = creationTime;
        domainEntity.UpdatedBy = "api";
        return Mapper.Map(RepoDbSet.Add(domainEntity).Entity)!;
    }

    public virtual TDalEntity Update(TDalEntity entity, TKey? userId = default)
    {
        TDomainEntity domainEntity = Mapper.Map(entity)!;
        domainEntity.UpdatedAt = DateTime.Now.ToUniversalTime();
        domainEntity.UpdatedBy = "api";
        return Mapper.Map(RepoDbSet.Update(domainEntity).Entity)!;
    }

    public virtual int Remove(TDalEntity entity, TKey? userId = default)
    {
        if (userId == null)
        {
            return RepoDbSet.Where(e => e.Id.Equals(entity.Id)).ExecuteDelete();
        }

        return CreateQuery(userId)
            .Where(e => e.Id.Equals(entity.Id))
            .ExecuteDelete();
    }

    public virtual int Remove(TKey id, TKey userId = default)
    {
        if (userId == null)
        {
            return RepoDbSet
                .Where(e => e.Id.Equals(id))
                .ExecuteDelete();
        }

        return CreateQuery(userId)
            .Where(e => e.Id.Equals(id))
            .ExecuteDelete();
    }


    public virtual IEnumerable<TDalEntity> GetAll(TKey userId = default, bool noTracking = true)
    {
        return CreateQuery(userId, noTracking).ToList().Select(de => Mapper.Map(de));
    }

    public virtual bool Exists(TKey id, TKey userId = default)
    {
        return CreateQuery(userId).Any(e => e.Id.Equals(id));
    }


    public virtual async Task<IEnumerable<TDalEntity>> GetAllAsync(TKey userId = default, bool noTracking = true)
    {
        return (await CreateQuery(userId, noTracking).ToListAsync())
            .Select(de => Mapper.Map(de));
    }

    public virtual async Task<bool> ExistsAsync(TKey id, TKey userId = default)
    {
        return await CreateQuery(userId).AnyAsync(e => e.Id.Equals(id));
    }

    public virtual async Task<int> RemoveAsync(TDalEntity entity, TKey userId = default)
    {
        return await CreateQuery(userId)
            .Where(e => e.Id.Equals(entity.Id))
            .ExecuteDeleteAsync();
    }

    public virtual async Task<int> RemoveAsync(TKey id, TKey userId = default)
    {
        return await CreateQuery(userId)
            .Where(e => e.Id.Equals(id))
            .ExecuteDeleteAsync();
    }
    
    public TDalEntity? FirstOrDefault(TKey id, TKey userId = default, bool noTracking = true)
    {
        return Mapper.Map(CreateQuery(userId, noTracking).FirstOrDefault(m => m.Id.Equals(id)));
    }

    public async Task<TDalEntity?> FirstOrDefaultAsync(TKey id, TKey userId = default, bool noTracking = true)
    {
        return Mapper.Map(await CreateQuery(userId, noTracking).FirstOrDefaultAsync(m => m.Id.Equals(id)));
    }
}