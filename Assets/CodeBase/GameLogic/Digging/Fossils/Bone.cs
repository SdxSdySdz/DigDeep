using CodeBase.GameLogic.Player;
using UnityEngine;

namespace CodeBase.GameLogic.Digging.Fossils
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bone : MonoBehaviour, IFossil
    {
        [SerializeField] private float _avoidingForce;
        
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
    }
}