using System;
using UnityEngine;

namespace MissileCommander
{
    [RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
    public class Explosion : RecyclableObject
    {
        private Rigidbody2D _rigidbody2D;
        private CircleCollider2D _circleCollider2D;

        [SerializeField] private float timeToRemove = 1f;
        private float _elapsedTime;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _circleCollider2D = GetComponent<CircleCollider2D>();

            _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
            _circleCollider2D.isTrigger = true;
        }

        private void Update()
        {
            if (isActivated)
            {
                _elapsedTime += Time.deltaTime;
                if (_elapsedTime >= timeToRemove)
                {
                    _elapsedTime = 0f;
                    DestroySelf();
                }
            }
        }

        private void DestroySelf()
        {
            isActivated = false;
            onDestroyed?.Invoke(this);
            Debug.Log($"{gameObject.name} is Destroyed!");
        }
    }
}