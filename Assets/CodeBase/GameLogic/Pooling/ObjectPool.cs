using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.GameLogic.Pooling
{
    public class ObjectPool<TObject>
        where TObject : IPoolObject
    {
        private readonly int _capacity;
        private readonly Func<TObject> _instantiate;

        private List<TObject> _pool;

        public ObjectPool(int capacity, Func<TObject> instantiate)
        {
            _capacity = capacity;
            _instantiate = instantiate;
            _pool = new List<TObject>(capacity);
            
            Fill();
        }

        private IEnumerable<TObject> DeactivatedObjects => _pool
            .Where(obj => obj.IsActive == false);

        public event UnityAction AllDeactivated; 

        public TObject Get()
        {
            TObject obj = DeactivatedObjects.First();
            obj.Activate();

            return obj;
        }
        
        private void Fill()
        {
            _pool = new List<TObject>(_capacity);

            for (int i = 0; i < _capacity; i++)
            {
                TObject instance = _instantiate.Invoke();
                instance.Deactivated += OnObjectDeactivated;
                
                instance.Deactivate();
                _pool.Add(instance);
            }
        }

        private void OnObjectDeactivated()
        {
            if (_pool.Any(obj => obj.IsActive) == false)
                AllDeactivated?.Invoke();
        }
    }
}