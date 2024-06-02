using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IParticipantEventRepository: IEntityRepository<App.DAL.DTO.ParticipantEvent>, IParticipantEventRepositoryCustom<App.DAL.DTO.Event>
{
    Task<IEnumerable<App.DAL.DTO.ParticipantEvent>> GetAllByEventId(Guid EventId, Guid userId = default, bool noTracking = true);
    App.DAL.DTO.ParticipantEvent Add(App.DAL.DTO.ParticipantEvent entity, Guid? userId = default);
}

public interface IParticipantEventRepositoryCustom<TEntity>
{
    //Task<IEnumerable<TEntity>> GetAllSortedAsync(Guid userId);
}