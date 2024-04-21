using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IParticipantEventRepository: IEntityRepository<App.DAL.DTO.ParticipantEvent>
{
    Task<IEnumerable<App.DAL.DTO.ParticipantEvent>> GetAllByEventId(Guid EventId, bool noTracking = true);
}