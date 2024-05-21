using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IParticipantEventRepository: IEntityRepository<App.DAL.DTO.ParticipantEvent>, IParticipantEventRepositoryCustom<App.DAL.DTO.Event>
{
    Task<IEnumerable<App.DAL.DTO.ParticipantEvent>> GetAllByEventId(Guid EventId, Guid userId = default, bool noTracking = true);
}

public interface IParticipantEventRepositoryCustom<TEntity>
{
    //Task<IEnumerable<TEntity>> GetAllSortedAsync(Guid userId);
}