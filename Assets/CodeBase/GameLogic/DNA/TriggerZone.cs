using CodeBase.GameLogic.Player;
using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.GameLogic.DNA
{
    [RequireComponent(typeof(BoxCollider))]
    public class TriggerZone : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        public event UnityAction<Character> Entered;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Character character))
                Entered?.Invoke(character);
        }
    }
}