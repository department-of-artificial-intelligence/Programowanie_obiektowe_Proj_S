namespace Project.Abstractions
{
    public interface ISemiTrailer : IVehicle
    {
        float MaxGrossWeightTons { get; }
    }
}