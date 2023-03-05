using UnityEngine.Events;

namespace CodeBase.GameLogic.Pooling
{
    public interface IPoolObject
    {
        bool IsActive { get; }

        void Activate();
        void Deactivate();
        event UnityAction Deactivated;
    }
}