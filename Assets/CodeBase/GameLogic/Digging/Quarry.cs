using CodeBase.Infrastructure.Services.Factory;
using UnityEngine;

namespace CodeBase.GameLogic.Digging
{
    public class Quarry : MonoBehaviour
    {
        [SerializeField] private GridPlacer _placer;
        [SerializeField] private EarthBlock _earthBlockPrefab;
        
        public void Construct(IFactoryService factoryService)
        {
            for (int i = 0; i < _placer.Capacity; i++)
            {
                EarthBlock block = factoryService.CreateBlock();
                _placer.Place(block.transform);
            }
        }
    }   
}