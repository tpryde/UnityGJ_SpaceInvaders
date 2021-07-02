using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Custom
{
    [Serializable]
    public struct Rangef
    {
        [SerializeField] private float _min;
        [SerializeField] private float _max;

        private static int Seed = System.DateTime.Now.Millisecond;

        public float Min
        {
            get => _min;
            set
            {
                _min = value;
                OrderValues();
            }
        }
        public float Max
        {
            get => _max;
            set
            {
                _max = value;
                OrderValues();
            }
        }

        public float Length => _max - _min;

        public Rangef(float min, float max)
        {
            _min = min;
            _max = max;
        }
        public Rangef(Rangef range)
        {
            _min = range.Min;
            _max = range.Max;
        }

        private void OrderValues()
        {
            if(_max < _min)
            {
                float temp = _max;
                _max = _min;
                _min = temp;
            }
        }

        public float GetPercentage(float amount)
        {
            float max = _max - _min;
            amount -= _min;
            return amount / max;
        }

        public float GetRandom()
        {
            if (_min == _max) { return _min; }

            Seed += UnityEngine.Random.Range(0, 100000);
            UnityEngine.Random.InitState(Seed);
            return UnityEngine.Random.Range(_min, _max);
        }
    };
}