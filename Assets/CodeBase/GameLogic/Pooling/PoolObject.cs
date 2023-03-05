using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.GameLogic.Pooling
{
    public abstract class PoolObject : MonoBehaviour, IPoolObject
    {
        public bool IsActive
        {
            get => gameObject.activeSelf;
            private set => gameObject.SetActive(value);
        }

        public event UnityAction Deactivated; 

        public void Activate()
        {
            IsActive = true;
            OnActivated();
        }

        public void Deactivate()
        {
            IsActive = false;
            OnDeactivated();
            
            Deactivated?.Invoke();
        }

        protected virtual void OnActivated() { }
        protected virtual void OnDeactivated() { }
    }
}