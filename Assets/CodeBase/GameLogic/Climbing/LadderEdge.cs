using CodeBase.GameLogic.Player;
using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.GameLogic.Climbing
{
    [RequireComponent(typeof(Collider))]
    public class LadderEdge : MonoBehaviour
    {
        public event UnityAction<LadderEdge, Character> Reached;

        private void Awake()
        {
            Collider collider = GetComponent<Collider>();
            collider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Character character))
                Reached?.Invoke(this, character);
        }
    }
}