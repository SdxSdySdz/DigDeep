using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.GameLogic.Digging.Fossils;
using CodeBase.GameLogic.Player;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.GameLogic.DNA
{
    public class ReceptionPoint : MonoBehaviour
    {
        private const int MaxCapacity = 25;
        
        [SerializeField] private Transform _tunnel;
        [SerializeField] private TriggerZone _triggerZone;

        private Capacity _capacity;

        private void Awake()
        {
            _capacity = new Capacity(MaxCapacity);
        }

        private void OnEnable()
        {
            _triggerZone.Entered += AcceptFossils;
            _capacity.Overflowed += _capacity.Reset;
        }
        
        private void OnDisable()
        {
            _triggerZone.Entered -= AcceptFossils;
            _capacity.Overflowed -= _capacity.Reset;
        }
        
        private void AcceptFossils(Character character)
        {
            List<Fossil> fossils = character.DropFossils(_capacity.FreeSlotsCount).ToList();
            const float delayBetweenBones = 0.1f;
            for (int i = 0; i < fossils.Count; i++)
            {
                Fossil fossil = fossils[i];
                fossil.transform
                    .DOMove(_tunnel.position, 0.5f)
                    .SetDelay(i * delayBetweenBones);
                
                _capacity.Increase();
            }
            
        }
    }
}