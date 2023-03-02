using UnityEngine;

namespace CodeBase.GameLogic.Player.Movement
{
    public interface IMovementStrategy
    {
        Vector3 GetMovementVector(Vector2 axes);
    }
}