using CodeBase.Infrastructure.States.Core;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class GameLoopState : State, IIndependentState
    {
        public GameLoopState(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }
}