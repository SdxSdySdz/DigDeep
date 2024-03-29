﻿using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.Progress;
using CodeBase.Infrastructure.Services.Random;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.Services.Update;
using UnityEngine;

namespace CodeBase.Infrastructure.States.Core
{
    public class StateMachine
    {
        private readonly Dictionary<Type, IState> _states;

        private IState _currentState;

        public StateMachine(AllServices services, SceneLoader sceneLoader, IUpdateService updateService)
        {
            _states = new Dictionary<Type, IState>()
            {
                { typeof(BootstrapState), new BootstrapState(this, sceneLoader, services, updateService) },
                { typeof(LoadProgressState), new LoadProgressState(
                    this, 
                    services.Get<IProgressService>(),
                    services.Get<ISaveLoadService>()) 
                },
                { typeof(LoadLevelState), new LoadLevelState(
                    this, 
                    sceneLoader, 
                    services.Get<IFactoryService>(),
                    services.Get<IProgressService>(),
                    services.Get<IInputService>(),
                    services.Get<IRandomService>()
                    ) },
                { typeof(GameLoopState), new GameLoopState(
                    this
                    ) },
            };
        }

        public void Enter<TState>()
            where TState : class, IIndependentState
        {
            _currentState?.Exit();
            
            TState newState = GetState<TState>();
            Debug.Log($"=== Enter {newState.GetType().Name} ===");

            newState.Enter();

            _currentState = newState;
        }

        private TState GetState<TState>() 
            where TState : class, IState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}