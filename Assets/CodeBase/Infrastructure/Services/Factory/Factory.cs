using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Constants;
using CodeBase.GameLogic.Digging;
using CodeBase.GameLogic.Digging.Fossils;
using CodeBase.Infrastructure.Services.Assets;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Infrastructure.Services.Random;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factory
{
    public class Factory : IFactoryService
    {
        private readonly IAssetsService _assets;
        private readonly IRandomService _randomService;

        private GameObject _blockPrefab;
        private GameObject _bonePrefab;
        
        public Factory(IAssetsService assets, IRandomService randomService)
        {
            _assets = assets;
            _randomService = randomService;
        }

        public List<IProgressReader> ProgressReaders { get; } = new();
        public List<IProgressWriter> ProgressWriters { get; } = new();

        public async Task WarmUp()
        {
            _blockPrefab = await _assets.Load<GameObject>(AssetAddress.EarthBlock);
            _bonePrefab = await _assets.Load<GameObject>(AssetAddress.Bone);
        }
        
        public Bone CreateBone(Vector3 position)
        {
            GameObject instance = InstantiateRegistered(_bonePrefab, position);
            Bone bone = instance.GetComponent<Bone>();

            return bone;
        }

        public EarthBlock CreateBlock()
        {
            GameObject instance = InstantiateRegistered(_blockPrefab);
            EarthBlock block = instance.GetComponent<EarthBlock>();
            block.Construct(this, _randomService);

            return block;
        }

        private void Register(IProgressInteractor progressInteractor)
        {
            if (progressInteractor is IProgressWriter progressWriter)
                ProgressWriters.Add(progressWriter);

            if (progressInteractor is IProgressReader progressReader)
                ProgressReaders.Add(progressReader);
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();

            _assets.Cleanup();
        }

        private GameObject InstantiateRegistered(GameObject prefab, Vector3 at)
        {
            GameObject gameObject = Object.Instantiate(prefab, at, Quaternion.identity);
            RegisterProgressInteractors(gameObject);

            return gameObject;
        }

        private GameObject InstantiateRegistered(GameObject prefab)
        {
            GameObject gameObject = Object.Instantiate(prefab);
            RegisterProgressInteractors(gameObject);

            return gameObject;
        }

        private void RegisterProgressInteractors(GameObject gameObject)
        {
            foreach (IProgressInteractor progressReader in gameObject.GetComponentsInChildren<IProgressInteractor>())
            {
                Register(progressReader);
            }
        }
    }
}
