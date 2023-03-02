using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Infrastructure.Services.Assets;
using CodeBase.Infrastructure.Services.Progress;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factory
{
    public class Factory : IFactoryService
    {
        private readonly IAssetsService _assets;

        public Factory(IAssetsService assets)
        {
            _assets = assets;
        }

        public List<IProgressReader> ProgressReaders { get; } = new();
        public List<IProgressWriter> ProgressWriters { get; } = new();

        public async Task WarmUp()
        {
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

        private async Task<GameObject> InstantiateRegisteredAsync(string prefabPath, Vector3 at)
        {
            GameObject gameObject = await _assets.Instantiate(path: prefabPath, at: at);
            RegisterProgressInteractors(gameObject);

            return gameObject;
        }

        private async Task<GameObject> InstantiateRegisteredAsync(string prefabPath)
        {
            GameObject gameObject = await _assets.Instantiate(path: prefabPath);
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