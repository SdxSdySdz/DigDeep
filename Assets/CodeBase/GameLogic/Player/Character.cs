using CodeBase.Infrastructure.Services.Input;
using UnityEngine;

namespace CodeBase.GameLogic.Player
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private Mover _mover;

        public void Construct(IInputService inputService)
        {
            _mover.Construct(inputService);
        }
    }
}