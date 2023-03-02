using System.Collections.Generic;
using Agava.YandexGames;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services.Update;
using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase
{
    public class Main : MonoBehaviour, ICoroutineRunner, IUpdateService
    {
        private Game _game;
        private List<IUpdatable> _updatables;

        private void Awake()
        {
            YandexGamesSdk.CallbackLogging = true;
        }

        private void Start()
        {
            _game = new Game(this, this);
            _game.StateMachine.Enter<BootstrapState>();

            _updatables = new List<IUpdatable>();

            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            for (int i = 0; i < _updatables.Count; i++)
            {
                _updatables[i].Update(Time.deltaTime);
            }
        }

        public void Register(IUpdatable updatable)
        {
            _updatables.Add(updatable);
        }

        public void Unregister(IUpdatable updatable)
        {
            _updatables.Remove(updatable);
        }
    }
}