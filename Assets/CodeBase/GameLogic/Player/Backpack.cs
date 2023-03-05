using CodeBase.GameLogic.Digging.Fossils;
using CodeBase.GameLogic.Placers;
using UnityEngine;

namespace CodeBase.GameLogic.Player
{
    public class Backpack : MonoBehaviour
    {
        [SerializeField] private VerticalPlacer _placer;

        public bool IsFull => _placer.IsFull;
        
        public void Collect(Fossil fossil)
        {
            _placer.Place(fossil.transform);
            DisablePhysics(fossil);
        }

        private void DisablePhysics(Fossil fossil)
        {
            fossil.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}