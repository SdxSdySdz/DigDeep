using CodeBase.Infrastructure.Services.Input;
using UnityEngine;

namespace CodeBase.GameLogic.Player.Movement
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private CharacterController _controller;
        
        private IMovementStrategy _movementStrategy;
        private IInputService _inputService;

        public void Construct(IMovementStrategy movementStrategy, IInputService inputService)
        {
            _movementStrategy = movementStrategy;
            _inputService = inputService;
        }
        
        private void Update()
        {
            if (_inputService == null || _movementStrategy == null)
                return;

            Vector3 movementVector = _movementStrategy.GetMovementVector(_inputService.Axes);
            Direct(movementVector);
            Move(movementVector);
        }

        private void Direct(Vector3 movementVector)
        {
            _controller.transform.forward = new Vector3(movementVector.x, 0, movementVector.z);
        }

        public void SetMovementStrategy(IMovementStrategy movementStrategy)
        {
            _movementStrategy = movementStrategy;
        }
        
        private void Move(Vector3 movementVector)
        {
            _controller.Move(movementVector * Time.deltaTime);
        }
    }
}