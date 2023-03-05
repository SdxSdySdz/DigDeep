using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.GameLogic.Digging;
using CodeBase.GameLogic.Digging.Fossils;
using CodeBase.Infrastructure.Services.Progress;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factory
{
    public interface IFactoryService : IService
    {
        List<IProgressReader> ProgressReaders { get; }
        List<IProgressWriter> ProgressWriters { get; }
        
        void Cleanup();
        Task WarmUp();

        Bone CreateBone(Vector3 position);
        EarthBlock CreateBlock();
    }
}