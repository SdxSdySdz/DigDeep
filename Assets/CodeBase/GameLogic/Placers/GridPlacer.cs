using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.GameLogic.Placers
{
    public class GridPlacer : MonoBehaviour
    {
        [SerializeField] private int _rowsCount;
        [SerializeField] private int _columnsCount;
        
        [Header("Margins")]
        [SerializeField] private float _rowMargin;
        [SerializeField] private float _columnMargin;
        
        private List<Transform> _children;

        public int Capacity => _rowsCount * _columnsCount;
        
        public void Awake()
        {
            _children = new List<Transform>();
        }

        public void Place(Transform child)
        {
            if (_children.Contains(child)) 
                return;
            
            if (_children.Count >= Capacity)
                throw new OverflowException("Impossible to place child. Overflow");

            Adopt(child);
            UpdatePositions();
        }

        private void Adopt(Transform child)
        {
            child.SetParent(transform);
            _children.Add(child);
        }

        private void UpdatePositions()
        {
            for (int i = 0; i < _children.Count; i++)
            {
                int row = i / _columnsCount;
                int column = i % _columnsCount;

                _children[i].transform.localPosition = new Vector3(column * _columnMargin, 0, row * _rowMargin);
            }
        }
    }
}