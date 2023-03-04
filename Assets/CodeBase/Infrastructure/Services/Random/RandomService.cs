namespace CodeBase.Infrastructure.Services.Random
{
    public class RandomService : IRandomService
    {
        public float GenerateProbability()
        {
            return Range(0f, 1f);
        }

        public float Range(float min, float max)
        {
            return UnityEngine.Random.Range(min, max);
        }

        public int Range(int min, int maxExclusive)
        {
            return UnityEngine.Random.Range(min, maxExclusive);
        }
    }
}