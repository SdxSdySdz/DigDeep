using System;

namespace CodeBase.GameLogic.DNA
{
    public class Capacity
    {
        private readonly int _capacity;
        
        private int _count;
        
        public Capacity(int capacity)
        {
            _capacity = capacity;
            _count = 0;
        }

        public event Action Overflowed;
        public int FreeSlotsCount => _capacity - _count;

        public void Increase()
        {
            _count++;

            if (_count >= _capacity)
            {
                _count = _capacity;
                Overflowed?.Invoke();
            }
        }

        public void Reset()
        {
            _count = 0;
        }
    }
}