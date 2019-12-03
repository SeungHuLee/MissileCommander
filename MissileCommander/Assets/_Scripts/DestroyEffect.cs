using System;
using UnityEngine;

namespace MissileCommander
{
    public class DestroyEffect : RecyclableObject
    {
        [SerializeField] private float effectTime = 0.5f;
        private float _elapsedTime = 0f;

        private void Update()
        {
            if (!isActivated) { return; }

            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= effectTime)
            {
                _elapsedTime = 0f;
                isActivated = false;
                
                onDestroyed?.Invoke(this);
            }
        }
    }
}