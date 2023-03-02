using CodeBase.Constants;
using CodeBase.Infrastructure.Services.Input;
using UnityEngine;

namespace CodeBase.GameLogic.Player
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private CharacterController _controller;
        [SerializeField] private float _movementSpeed;

        private Camera _camera;
        private IInputService _inputService;

        private void Start()
        {
            _camera = Camera.main;
        }

        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Update()
        {
            if (_inputService == null)
                return;
            
            Vector3 movementVector = Vector3.zero;
            
            if (_inputService.Axes.sqrMagnitude > NumericalConstants.Epsilon)
            {
                movementVector = _camera.transform.TransformDirection(_inputService.Axes);
                movementVector.y = 0;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;

            _controller.Move(movementVector * (_movementSpeed * Time.deltaTime));
        }
    }
}