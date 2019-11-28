using System;
using UnityEngine;

namespace MissileCommander
{
    public class Bullet : RecyclableObject
    {
        [SerializeField] private float moveSpeed = 5f;

        private Transform _cachedTransform;
        private Transform CachedTransform => _cachedTransform ? _cachedTransform : (_cachedTransform = transform);
        private Vector3 _targetPosition;
        [SerializeField] private float sqrApproximateOffset = 0.01f;
        private bool _isActivated = false;
        public event Action<Bullet> OnDestroyed = delegate {  };

        private void Update()
        {
            if (!_isActivated) { return; }
            
            CachedTransform.position += Time.deltaTime * moveSpeed * transform.up;

            if (IsArrivedToTarget())
            {
                _isActivated = false;
                OnDestroyed(this);
            }
        }

        public void Activate(Vector3 startPos, Vector3 targetPos)
        {
            this._targetPosition = targetPos;
            
            Vector3 direction = (targetPos - startPos).normalized;
            CachedTransform.SetPositionAndRotation(startPos,
                                                   Quaternion.LookRotation(CachedTransform.forward, direction));
            _isActivated = true;
        }

        private bool IsArrivedToTarget()
        {
            float sqrDist = (_targetPosition - CachedTransform.position).sqrMagnitude;
            return sqrDist < sqrApproximateOffset;
        }
    }
}