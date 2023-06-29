using CodeBase.GameLogic.Player;
using UnityEngine;

namespace CodeBase.GameLogic.Digging.Fossils
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Fossil : MonoBehaviour, IFossil
    {
        [SerializeField] private float _avoidingForce;
        [SerializeField] private Collider _collider;
        
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Avoid(Character character)
        {
            Vector3 direction = (transform.position - character.transform.position).normalized;
            direction.y = 1;
            
            _rigidbody.AddForce(direction * _avoidingForce, ForceMode.Impulse);
        }

        public void ToNotInteractable()
        {
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            _collider.enabled = false;
        }
    }
}