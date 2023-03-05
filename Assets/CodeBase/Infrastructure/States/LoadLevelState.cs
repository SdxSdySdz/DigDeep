using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Constants;
using CodeBase.GameLogic.Digging;
using CodeBase.GameLogic.Player;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Infrastructure.Services.Random;
using CodeBase.Infrastructure.States.Core;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : State, IIndependentState
    {
        private readonly SceneLoader _sceneLoader;
        private readonly IFactoryService _factoryService;
        private readonly IProgressService _progressService;
        private readonly IInputService _inputService;
        private readonly IRandomService _randomService;

        public LoadLevelState(
            StateMachine stateMachine,
            SceneLoader sceneLoader,
            IFactoryService factoryService,
            IProgressService progressService,
            IInputService inputService,
            IRandomService randomService
        ) : base(stateMachine)
        {
            _sceneLoader = sceneLoader;
            _factoryService = factoryService;
            _progressService = progressService;
            _inputService = inputService;
            _randomService = randomService;
        }

        public async void Enter()
        {
            _factoryService.Cleanup();
            await _factoryService.WarmUp();
            _sceneLoader.Load(Scenes.GameLoop, EnterGameLoop);
        }

        public void Exit()
        {
        }

        private async void EnterGameLoop()
        {
            await InitWorld();
            InitUI();
            InformProgressReaders();
            
            StateMachine.Enter<GameLoopState>();
        }

        private async Task InitWorld()
        {
            Character character = Object.FindObjectOfType<Character>();
            character.Construct(_inputService);

            Quarry quarry = Object.FindObjectOfType<Quarry>();
            quarry.Construct(_factoryService);
        }

        private void InitUI()
        {
        }

        private void InformProgressReaders()
        {
            foreach (IProgressReader progressReader in _factoryService.ProgressReaders)
            {
                progressReader.LoadProgress(_progressService.Progress);
            }
        }
    }
}