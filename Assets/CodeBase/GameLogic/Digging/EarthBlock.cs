using CodeBase.GameLogic.Digging.Fossils;
using CodeBase.GameLogic.Player;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.Random;
using UnityEngine;

namespace CodeBase.GameLogic.Digging
{
    [RequireComponent(typeof(BoxCollider))]
    public class EarthBlock : MonoBehaviour
    {
        private const float BoneSpawningProbability = 1f;
        
        private IRandomService _randomService;
        private IFactoryService _factoryService;

        public void Construct(IFactoryService factoryService, IRandomService randomService)
        {
            _factoryService = factoryService;
            _randomService = randomService;
        }
        
        public void Dig(Character character)
        {
            Destroy(gameObject);

            bool isBoneSpawningNeeded = _randomService.GenerateProbability() < BoneSpawningProbability;
            if (isBoneSpawningNeeded)
            {
                Bone bone = _factoryService.CreateBone(transform.position);
                bone.Avoid(character);
            }
        }
    }
}