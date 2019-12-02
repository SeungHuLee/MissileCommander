using UnityEngine;

namespace MissileCommander
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public class Missile : RecyclableObject
    {
        private Rigidbody2D _rigidbody2D;
        private BoxCollider2D _boxCollider2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _boxCollider2D = GetComponent<BoxCollider2D>();

            _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
            _boxCollider2D.isTrigger = true;
        }
    }
}