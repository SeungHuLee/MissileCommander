using System;
using UnityEngine;

namespace MissileCommander
{
    public class Bullet : RecyclableObject
    {
        [SerializeField] private float moveSpeed = 5f;
        // squApproximateOffset is squared value.
        [SerializeField] private float sqrApproximateOffset = 0.01f;

        private void Update()
        {
            if (!isActivated) { return; }
            
            CachedTransform.position += Time.deltaTime * moveSpeed * transform.up;

            if (IsArrivedToTarget())
            {
                isActivated = false;
                onDestroyed?.Invoke(this);
            }
        }

        private bool IsArrivedToTarget()
        {
            float sqrDist = (targetPosition - CachedTransform.position).sqrMagnitude;
            return sqrDist < sqrApproximateOffset;
        }
    }
}