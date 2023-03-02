using CodeBase.Infrastructure.Services.Input;
using UnityEngine;

namespace CodeBase.GameLogic.Player
{
    public abstract class Mover : MonoBehaviour
    {
        [SerializeField] private CharacterController _controller;
        [SerializeField] private float _movementSpeed;

        private IInputService _inputService;

        public void Enable()
        {
            
        }
        
        public void Disable()
        {
            
        }

        private void Update()
        {
            if (_inputService == null)
                return;

            OnUpdate(_inputService.Axes);
        }

        protected abstract void OnUpdate(Vector2 inputAxes);
        
        protected void Move(Vector3 movementVector)
        {
            _controller.Move(movementVector * (_movementSpeed * Time.deltaTime));
        }
    }
}