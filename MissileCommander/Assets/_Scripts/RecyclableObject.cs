using System;
using UnityEngine;

namespace MissileCommander
{
    public class RecyclableObject : MonoBehaviour
    {
        protected bool isActivated = false;
        protected Vector3 targetPosition;
        public Action<RecyclableObject> onDestroyed;
        public Action<RecyclableObject> onOutOfScreen;

        private Transform _cachedTransform;
        public Transform CachedTransform => _cachedTransform ? _cachedTransform : (_cachedTransform = transform);

        public virtual void Activate(Vector3 position)
        {
            isActivated = true;
            transform.position = position;
        }

        public virtual void Activate(Vector3 startPos, Vector3 targetPos)
        {
            this.targetPosition = targetPos;
            
            Vector3 direction = (targetPos - startPos).normalized;
            CachedTransform.SetPositionAndRotation
                (startPos,
                 Quaternion.LookRotation(CachedTransform.forward, direction));
            isActivated = true;
        }
    }
}