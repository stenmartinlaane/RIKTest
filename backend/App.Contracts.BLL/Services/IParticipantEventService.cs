using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface IParticipantEventService : IEntityRepository<App.BLL.DTO.ParticipantEvent>, IParticipantEventRepositoryCustom<App.BLL.DTO.ParticipantEvent>
{
    
}

public interface IParticipantEventRepositoryCustom<TEntity>
{
    //Task<IEnumerable<TEntity>> GetAllSortedAsync(Guid userId);
}