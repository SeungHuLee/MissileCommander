using UnityEngine;

namespace MissileCommander
{
    public class Bullet : RecyclableObject
    {
        [SerializeField] private float moveSpeed = 5f;

        private Transform _cachedTransform;
        private Transform CachedTransform => _cachedTransform ? _cachedTransform : (_cachedTransform = transform);

        private void Update()
        {
            CachedTransform.position += Time.deltaTime * moveSpeed * transform.up;
        }

        public void Activate(Vector3 startPos, Vector3 targetPos)
        {
            Vector3 direction = (targetPos - startPos).normalized;
            CachedTransform.SetPositionAndRotation(startPos,
                                                   Quaternion.LookRotation(CachedTransform.forward, direction));
        }
    }
}