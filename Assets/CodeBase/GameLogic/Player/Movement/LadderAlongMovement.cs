using UnityEngine;

namespace CodeBase.GameLogic.Player.Movement
{
    public class LadderAlongMovement : IMovementStrategy
    {
        private readonly float _climbingSpeed;

        public LadderAlongMovement(float climbingSpeed)
        {
            _climbingSpeed = climbingSpeed;
        }
        
        public Vector3 GetMovementVector(Vector2 axes)
        {
            Vector3 movementVector = Vector3.zero;
            
            if (axes.y > 0)
                movementVector = Vector3.up;
            else if (axes.y < 0)
                movementVector = -Vector3.up;

            return movementVector * _climbingSpeed;
        }
    }
}