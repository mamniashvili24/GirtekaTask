using Xunit;
using NSubstitute;
using Domain.Counter;
using FluentAssertions;
using Domain.Common.Database.Abstractions;
using Application.Queries.GetAllElectricities;

namespace Application.Tests.Unit;

public class GetAllElectricitiesQueryHandlerTests
{
	private readonly GetAllElectricitiesQueryHandler _sut;

    private readonly IRepository<HouseholdMeteringPlant> _householdMeteringPlantRepository = Substitute.For<IRepository<HouseholdMeteringPlant>>();

    public GetAllElectricitiesQueryHandlerTests()
	{
		_sut = new(_householdMeteringPlantRepository);
	}

    [Fact]
    public void Handle_GetAllElectricities_GetData()
    {
        // Arrange
        var mockData = GetHouseholdMeteringPlantValuesDamData();
        mockData.GetAsyncEnumerator();
        _householdMeteringPlantRepository.GetAllAsyncEnumerable().Returns(mockData);

        // Act
        var results = _sut.Handle(new GetAllElectricitiesQuery(), default);

        // Assert
        results.Should().BeEquivalentTo(mockData);
    }

    public static async IAsyncEnumerable<HouseholdMeteringPlant> GetHouseholdMeteringPlantValuesDamData()
	{
		yield return new HouseholdMeteringPlant
		{
			Id = Guid.NewGuid(),
			PL_T = DateTime.Now,
			TINKLAS = "TINKLAS",
			PPlus = Random.Shared.Next(),
			OBJ_GV_TIPAS = "OBJ_GV_TIPAS",
			PMinus = Random.Shared.Next(),
			OBJ_NUMERIS = Random.Shared.Next(),
			OBT_PAVADINIMAS = "OBT_PAVADINIMAS"
		};

		yield return null;

        yield return new HouseholdMeteringPlant
        {
            Id = Guid.NewGuid(),
            PL_T = DateTime.Now,
            TINKLAS = "TINKLAS",
            PPlus = Random.Shared.Next(),
            OBJ_GV_TIPAS = "OBJ_GV_TIPAS",
            PMinus = Random.Shared.Next(),
            OBJ_NUMERIS = Random.Shared.Next(),
            OBT_PAVADINIMAS = "OBT_PAVADINIMAS"
        };
    }
}