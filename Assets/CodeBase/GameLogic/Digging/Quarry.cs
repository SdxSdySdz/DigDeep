using CodeBase.GameLogic.Pooling;
using CodeBase.Infrastructure.Services.Factory;
using UnityEngine;

namespace CodeBase.GameLogic.Digging
{
    public class Quarry : MonoBehaviour
    {
        [SerializeField] private Bottom _bottom;

        public void Construct(IFactoryService factoryService)
        {
            _bottom.Construct(factoryService);
            _bottom.CreateBlocks();
        }
    }   
}