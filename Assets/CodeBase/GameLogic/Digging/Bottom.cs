using CodeBase.GameLogic.Pooling;
using CodeBase.Infrastructure.Services.Factory;
using UnityEngine;

namespace CodeBase.GameLogic.Digging
{
    public class Bottom : MonoBehaviour
    {
        [SerializeField] private GridPlacer _placer;
        
        private ObjectPool<EarthBlock> _pool;

        public void Construct(IFactoryService factoryService)
        {
            _pool = new ObjectPool<EarthBlock>(_placer.Capacity, factoryService.CreateBlock);
            _pool.AllDeactivated += OnAllBlocksDestroyed;
        }

        public void CreateBlocks()
        {
            for (int i = 0; i < _placer.Capacity; i++)
            {
                EarthBlock block = _pool.Get();
                _placer.Place(block.transform);
            }
        }

        private void OnAllBlocksDestroyed()
        {
            CreateBlocks();
        }
    }
}