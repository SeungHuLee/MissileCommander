using System;
using UnityEngine;

namespace MissileCommander
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public class Missile : RecyclableObject
    {
        [SerializeField] private float moveSpeed = 3f;

        private float _bottomY;
        private Rigidbody2D _rigidbody2D;
        private BoxCollider2D _boxCollider2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _boxCollider2D = GetComponent<BoxCollider2D>();

            Vector3 bottomPos = Camera.main.ViewportToWorldPoint(new Vector2(0f, 0f));
            _bottomY = bottomPos.y - _boxCollider2D.size.y;

            _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
            _boxCollider2D.isTrigger = true;
        }

        private void Update()
        {
            if (!isActivated) { return; }
            
            CachedTransform.position += Time.deltaTime * moveSpeed * transform.up;
            IsOutOfScreen();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Building building))
            {
                DestroySelf();
                return;
            }

            if (other.TryGetComponent(out Explosion explosion))
            {
                Debug.Log("Intercepted by explosion!");
                DestroySelf();
                return;
            }
        }

        private void DestroySelf()
        {
            isActivated = false;
            onDestroyed?.Invoke(this);
        }

        private void IsOutOfScreen()
        {
            if (CachedTransform.position.y < _bottomY)
            {
                isActivated = false;
                onOutOfScreen?.Invoke(this);
            }
        }
    }
}