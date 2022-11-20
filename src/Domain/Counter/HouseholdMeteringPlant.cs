namespace Domain.Counter;

public class HouseholdMeteringPlant
{
    public Guid Id { get; set; }

    public DateTime PL_T { get; set; }

    public decimal? PPlus { get; set; }

    public string TINKLAS { get; set; }

    public decimal? PMinus { get; set; }
    
    public int OBJ_NUMERIS { get; set; }

    public string OBJ_GV_TIPAS { get; set; }

    public string OBT_PAVADINIMAS { get; set; }
}