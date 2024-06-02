using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface IParticipantEventService : IEntityRepository<App.BLL.DTO.ParticipantEvent>, IParticipantEventRepositoryCustom<App.BLL.DTO.ParticipantEvent>
{
    Task<IEnumerable<App.BLL.DTO.ParticipantEvent>?> GetAllByEventId(Guid EventId, Guid userId = default,
        bool noTracking = true);

    Task<App.BLL.DTO.ParticipantEvent?> AddParticipantToEventAsync(
        App.BLL.DTO.ParticipantEvent participantEvent, Guid userId);
}

public interface IParticipantEventRepositoryCustom<TEntity>
{
    //Task<IEnumerable<TEntity>> GetAllSortedAsync(Guid userId);
    
}