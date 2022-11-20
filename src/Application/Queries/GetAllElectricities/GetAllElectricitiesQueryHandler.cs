using MediatR;
using Domain.Counter;
using Domain.Common.Database.Abstractions;

namespace Application.Queries.GetAllElectricities;

public class GetAllElectricitiesQueryHandler : IStreamRequestHandler<GetAllElectricitiesQuery, HouseholdMeteringPlant>
{
    private readonly IRepository<HouseholdMeteringPlant> _householdMeteringPlantRepository;

    public GetAllElectricitiesQueryHandler(IRepository<HouseholdMeteringPlant> householdMeteringPlantRepository)
    {
        _householdMeteringPlantRepository = householdMeteringPlantRepository;
    }

    public IAsyncEnumerable<HouseholdMeteringPlant> Handle(GetAllElectricitiesQuery request, CancellationToken cancellationToken)
    {
        var householdMeteringPlants = _householdMeteringPlantRepository.GetAllAsyncEnumerable();

        return householdMeteringPlants;
    }
}