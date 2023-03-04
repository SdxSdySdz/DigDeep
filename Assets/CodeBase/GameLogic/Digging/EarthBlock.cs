using System;
using CodeBase.GameLogic.Player;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.Random;
using UnityEngine;

namespace CodeBase.GameLogic.Digging
{
    [RequireComponent(typeof(BoxCollider))]
    public class EarthBlock : MonoBehaviour
    {
        private const float BoneSpawningProbability = 0.5f;
        
        private IRandomService _randomService;
        private IFactoryService _factoryService;

        public void Construct(IFactoryService factoryService, IRandomService randomService)
        {
            _factoryService = factoryService;
            _randomService = randomService;
        }
        
        public void Dig()
        {
            if (_randomService.GenerateProbability() < BoneSpawningProbability)
                _factoryService.CreateBone(transform.position);
            
            Destroy(gameObject);
        }
    }
}