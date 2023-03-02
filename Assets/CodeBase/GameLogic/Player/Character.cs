using CodeBase.GameLogic.Player.Movement;
using CodeBase.Infrastructure.Services.Input;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.GameLogic.Player
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private Mover _mover;

        public bool IsClimbing { get; private set; }

        public void Construct(IInputService inputService)
        {
            _mover.Construct(new FloorAlongMovement(5f, 1f, Camera.main), inputService);
        }

        public void StopClimbing(Vector3 floorPosition)
        {
            const float duration = 0.5f;
            transform
                .DOMove(floorPosition + Vector3.up, duration)
                .OnStart(() => _mover.SetMovementStrategy(null))
                .OnComplete(() =>
                {
                    _mover.SetMovementStrategy(new FloorAlongMovement(5f, 1f, Camera.main));
                    IsClimbing = false;
                });
        }

        public void Climb(Vector3 climbingPosition)
        {
            const float duration = 0.25f;
            transform
                .DOMove(climbingPosition, duration)
                .OnStart(() => _mover.SetMovementStrategy(null))
                .OnComplete(() =>
                {
                    _mover.SetMovementStrategy(new LadderAlongMovement(5f));
                    IsClimbing = true;
                });
        }
    }
}