using System.Collections.Generic;
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
            fossil.ToNotInteractable();
        }
        
        public IEnumerable<Fossil> DropFossils(int count)
        {
            count = Mathf.Min(count, _placer.Count);

            for (int i = 0; i < count; i++)
            {
                yield return _placer.Pop().GetComponent<Fossil>();
            }
        }
    }
}