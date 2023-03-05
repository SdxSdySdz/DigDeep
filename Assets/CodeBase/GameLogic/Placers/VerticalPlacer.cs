﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.GameLogic.Placers
{
    public class VerticalPlacer : MonoBehaviour
    {
        [SerializeField] private int _capacity;
        [SerializeField] private float _margin;
        

        private List<Transform> _children;
        public bool IsFull => _children.Count >= _capacity;

        private void Awake()
        {
            _children = new List<Transform>();
        }

        public void Place(Transform child)
        {
            if (_children.Contains(child)) 
                return;
            
            if (_children.Count >= _capacity)
                throw new OverflowException("Impossible to place child. Overflow");
            
            Align(child);
            Adopt(child);
            UpdatePositions();
        }

        private void Adopt(Transform child)
        {
            child.SetParent(transform);
            _children.Add(child);
        }

        private void Align(Transform child)
        {
            child.up = transform.up;
            child.forward = transform.forward;
            child.localScale /= 1.5f;
        }

        private void UpdatePositions()
        {
            for (int i = 0; i < _children.Count; i++)
            {
                _children[i].transform.position = transform.position + transform.up * i * _margin;
            }
        }
    }
}