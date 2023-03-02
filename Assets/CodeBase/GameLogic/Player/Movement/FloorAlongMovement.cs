using CodeBase.Constants;
using UnityEngine;

namespace CodeBase.GameLogic.Player.Movement
{
    public class FloorAlongMovement : IMovementStrategy
    {
        private readonly float _movementSpeed;
        private readonly float _fallingSpeed;
        private readonly Camera _camera;

        public FloorAlongMovement(float movementSpeed, float fallingSpeed, Camera camera)
        {
            _movementSpeed = movementSpeed;
            _fallingSpeed = fallingSpeed;
            _camera = camera;
        }

        public Vector3 GetMovementVector(Vector2 axes)
        {
            Vector3 movementVector = Vector3.zero;
            
            if (axes.sqrMagnitude > NumericalConstants.Epsilon)
            {
                movementVector = _camera.transform.TransformDirection(axes);
                movementVector.y = 0;
                movementVector.Normalize();
            }

            return movementVector * _movementSpeed + Physics.gravity * _fallingSpeed;
        }
    }
}