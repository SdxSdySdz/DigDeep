namespace CodeBase.Infrastructure.Services.Random
{
    public interface IRandomService : IService
    {
        int Range(int min, int maxExclusive);
        float GenerateProbability();
        float Range(float min, float max);
    }
}