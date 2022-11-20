using MediatR;
using Domain.Counter;

namespace Application.Queries.GetAllElectricities;

public class GetAllElectricitiesQuery : IStreamRequest<HouseholdMeteringPlant> { }