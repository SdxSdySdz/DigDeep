using System.Numerics;

namespace CodeBase.Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 Axes { get; }
    }
}