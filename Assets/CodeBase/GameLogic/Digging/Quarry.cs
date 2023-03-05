using CodeBase.GameLogic.Pooling;
using CodeBase.Infrastructure.Services.Factory;
using UnityEngine;

namespace CodeBase.GameLogic.Digging
{
    public class Quarry : MonoBehaviour
    {
        [SerializeField] private GridPlacer _placer;
        [SerializeField] private EarthBlock _earthBlockPrefab;

        private ObjectPool<EarthBlock> _pool;

        public void Construct(IFactoryService factoryService)
        {
            _pool = new ObjectPool<EarthBlock>(_placer.Capacity, factoryService.CreateBlock);

            OnAllBlocksDestroyed();

            _pool.AllDeactivated += OnAllBlocksDestroyed;
        }

        private void OnAllBlocksDestroyed()
        {
            for (int i = 0; i < _placer.Capacity; i++)
            {
                EarthBlock block = _pool.Get();
                _placer.Place(block.transform);
            }
        }
    }   
}